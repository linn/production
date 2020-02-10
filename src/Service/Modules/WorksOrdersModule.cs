namespace Linn.Production.Service.Modules
{
    using System;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps.Exceptions;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using Linn.Production.Resources.RequestResources;
    using Linn.Production.Service.Extensions;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;
    using Nancy.Security;

    public sealed class WorksOrdersModule : NancyModule
    {
        private readonly IOutstandingWorksOrdersReportFacade outstandingWorksOrdersReportFacade;

        private readonly IWorksOrdersService worksOrdersService;

        private readonly
            IFacadeService<WorksOrderLabel, WorksOrderLabelKey, WorksOrderLabelResource, WorksOrderLabelResource>
            labelService;

        private readonly IWorksOrderLabelPack worksOrderLabelPack;

        public WorksOrdersModule(
            IOutstandingWorksOrdersReportFacade outstandingWorksOrdersReportFacade,
            IFacadeService<WorksOrderLabel, WorksOrderLabelKey, WorksOrderLabelResource, WorksOrderLabelResource> labelService, 
            IWorksOrdersService worksOrdersService,
            IWorksOrderLabelPack worksOrderLabelPack)
        {
            this.worksOrdersService = worksOrdersService;
            this.worksOrderLabelPack = worksOrderLabelPack;
            this.labelService = labelService;
            this.outstandingWorksOrdersReportFacade = outstandingWorksOrdersReportFacade;

            this.Get("/production/works-orders", _ => this.GetWorksOrders());
            this.Put("/production/works-orders/labels/{seq}/{part*}", _ => this.UpdateWorksOrderLabel());
            this.Post("production/works-orders/labels", _ => this.AddWorksOrderLabel());
            this.Get("/production/works-orders/labels", _ => this.GetWorksOrderLabelsForPart());
            this.Get("/production/works-orders/batch-notes", _ => this.GetWorksOrderBatchNotes());
            this.Get("/production/works-orders/labels/{seq}/{part*}", parameters => this.GetWorksOrderLabel(parameters.part, parameters.seq));
            this.Get("/production/works-orders/{orderNumber}", parameters => this.GetWorksOrder(parameters.orderNumber));
            this.Post("/production/works-orders", _ => this.AddWorksOrder());
            this.Put("/production/works-orders/{orderNumber}", _ => this.UpdateWorksOrder());

            this.Post("/production/works-orders/print-labels", _ => this.PrintWorksOrderLabels());
            this.Post("/production/works-orders/print-aio-labels", _ => this.PrintWorksOrderAioLabels());

            this.Get(
                "/production/works-orders/get-part-details/{partNumber*}",
                parameters => this.GetWorksOrderPartDetails(parameters.partNumber));

            this.Get("/production/works-orders/outstanding-works-orders-report", _ => this.GetOutstandingWorksOrdersReport());
            this.Get("/production/works-orders/outstanding-works-orders-report/export", _ => this.GetOutstandingWorksOrdersReportExport());
            this.Get("/production/works-orders-for-part", _ => this.GetWorksOrdersForPart());
        }

        private object GetWorksOrderBatchNotes()
        {
            return this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index");
        }

        private object GetWorksOrder(int orderNumber)
        {
            return this.Negotiate.WithModel(this.worksOrdersService.GetById(orderNumber))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get).WithView("Index");
        }

        private object GetWorksOrders()
        {
            var resource = this.Bind<SearchRequestResource>();

            var worksOrders = this.worksOrdersService.Search(resource.SearchTerm);

            return this.Negotiate.WithModel(worksOrders).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object UpdateWorksOrder()
        {
            this.RequiresAuthentication();

            var resource = this.Bind<WorksOrderResource>();

            resource.Links = new[] { new LinkResource("updated-by", this.Context.CurrentUser.GetEmployeeUri()) };

            return this.Negotiate.WithModel(this.worksOrdersService.UpdateWorksOrder(resource))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get).WithView("Index");
        }

        private object AddWorksOrder()
        {
            this.RequiresAuthentication();

            var resource = this.Bind<WorksOrderResource>();

            resource.Links = new[] { new LinkResource("raised-by", this.Context.CurrentUser.GetEmployeeUri()) };

            return this.Negotiate.WithModel(this.worksOrdersService.AddWorksOrder(resource))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get).WithView("Index");
        }

        private object PrintWorksOrderLabels()
        {
            var resource = this.Bind<WorksOrderLabelPrintRequestResource>();

            try
            {
                this.worksOrderLabelPack.PrintLabels(resource.OrderNumber, resource.PrinterGroup);
            }
            catch (Exception exception)
            {
                return this.Negotiate.WithModel(new BadRequestResult<Error>(exception.Message));
            }

            return HttpStatusCode.OK;
        }

        private object PrintWorksOrderAioLabels()
        {
            var resource = this.Bind<WorksOrderLabelPrintRequestResource>();

            try
            {
                this.worksOrderLabelPack.PrintAioLabels(resource.OrderNumber);
            }
            catch (Exception exception)
            {
                return this.Negotiate.WithModel(new BadRequestResult<Error>(exception.Message));
            }

            return HttpStatusCode.OK;
        }

        private object GetWorksOrderPartDetails(string partNumber)
        {
            return this.Negotiate.WithModel(this.worksOrdersService.GetWorksOrderPartDetails(partNumber))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get).WithView("Index");
        }

        private object GetOutstandingWorksOrdersReport()
        {
            var resource = this.Bind<OutstandingWorksOrdersRequestResource>();

            var result = this.outstandingWorksOrdersReportFacade.GetOutstandingWorksOrdersReport(resource.ReportType, resource.SearchParameter);

            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetOutstandingWorksOrdersReportExport()
        {
            var resource = this.Bind<OutstandingWorksOrdersRequestResource>();

            var result = this.outstandingWorksOrdersReportFacade.GetOutstandingWorksOrdersReportCsv(resource.ReportType, resource.SearchParameter);

            return this.Negotiate
                .WithModel(result)
                .WithAllowedMediaRange("text/csv")
                .WithView("Index");
        }

        private object GetWorksOrdersForPart()
        {
            var resource = this.Bind<SearchRequestResource>();
            
            var worksOrders = this.worksOrdersService.SearchByBoardNumber(
                resource.SearchTerm, 
                resource.Limit,
                resource.OrderByDesc);

            return this.Negotiate.WithModel(worksOrders).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetWorksOrderLabelsForPart()
        {
            var resource = this.Bind<SearchRequestResource>();
            var labels = this.labelService.Search(resource.SearchTerm);
            return this.Negotiate.WithModel(labels).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetWorksOrderLabel(string part, int seq)
        {
            var labels = this.labelService.GetById(new WorksOrderLabelKey { PartNumber = part, Sequence = seq });
            return this.Negotiate.WithModel(labels).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object UpdateWorksOrderLabel()
        {
            this.RequiresAuthentication();

            var resource = this.Bind<WorksOrderLabelResource>();
            var result =
                this.labelService.Update(
                    new WorksOrderLabelKey
                        {
                            Sequence = resource.Sequence, PartNumber = resource.PartNumber
                        }, 
                    resource);
            return this.Negotiate.WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get).WithView("Index");
        }

        private object AddWorksOrderLabel()
        {
            this.RequiresAuthentication();

            var resource = this.Bind<WorksOrderLabelResource>();

            return this.Negotiate.WithModel(this.labelService.Add(resource))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get).WithView("Index");
        }
    }
}
