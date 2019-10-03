namespace Linn.Production.Service.Modules
{
    using Linn.Common.Resources;
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using Linn.Production.Service.Extensions;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;
    using Nancy.Security;

    public sealed class WorksOrdersModule : NancyModule
    {
        private readonly IOutstandingWorksOrdersReportFacade outstandingWorksOrdersReportFacade;

        private readonly IWorksOrdersService worksOrdersService;

        public WorksOrdersModule(IOutstandingWorksOrdersReportFacade outstandingWorksOrdersReportFacade, IWorksOrdersService worksOrdersService)
        {
            this.worksOrdersService = worksOrdersService;
            this.outstandingWorksOrdersReportFacade = outstandingWorksOrdersReportFacade;

            this.Get("/production/works-orders", _ => this.GetWorksOrders());
            this.Get("/production/works-orders/{orderNumber}", parameters => this.GetWorksOrder(parameters.orderNumber));
            this.Post("/production/works-orders", _ => this.AddWorksOrder());
            this.Put("/production/works-orders/{orderNumber}", _ => this.UpdateWorksOrder());
            this.Get(
                "/production/works-orders/get-part-details/{partNumber*}",
                parameters => this.GetWorksOrderPartDetails(parameters.partNumber));

            this.Get("/production/works-orders/outstanding-works-orders-report", _ => this.GetOutstandingWorksOrdersReport());
            this.Get("/production/works-orders/outstanding-works-orders-report/export", _ => this.GetOutstandingWorksOrdersReportExport());
        }

        private object GetWorksOrder(int orderNumber)
        {
            return this.Negotiate.WithModel(this.worksOrdersService.GetById(orderNumber))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get).WithView("Index");
        }

        private object GetWorksOrders()
        {
            var resource = this.Bind<SearchRequestResource>();

            var worksOrders = string.IsNullOrEmpty(resource.SearchTerm)
                                  ? this.worksOrdersService.GetAll()
                                  : this.worksOrdersService.Search(resource.SearchTerm);

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
    }
}
