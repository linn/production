namespace Linn.Production.Service.Modules
{
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class WorksOrdersModule : NancyModule
    {
        private readonly IOutstandingWorksOrdersReportFacade outstandingWorksOrdersReportFacade;

        private readonly IWorksOrdersService worksOrdersService;

        public WorksOrdersModule(IOutstandingWorksOrdersReportFacade outstandingWorksOrdersReportFacade, IWorksOrdersService worksOrdersService)
        {
            this.worksOrdersService = worksOrdersService;
            this.outstandingWorksOrdersReportFacade = outstandingWorksOrdersReportFacade;

            this.Get("/production/maintenance/works-orders", _ => this.GetWorksOrders());
            this.Get("/production/maintenance/works-orders/{orderNumber}", parameters => this.GetWorksOrder(parameters.orderNumber));
            this.Post("/production/maintenance/works-orders", _ => this.AddWorksOrder());
            this.Put("/production/maintenance/works-orders/{orderNumber}", _ => this.UpdateWorksOrder());
            this.Get(
                "/production/maintenance/works-orders/details/{partNumber}",
                parameters => this.GetWorksOrderDetails(parameters.partNumber));

            this.Get("/production/maintenance/works-orders/outstanding-works-orders-report", _ => this.GetOutstandingWorksOrdersReport());
            this.Get("/production/maintenance/works-orders/outstanding-works-orders-report/export", _ => this.GetOutstandingWorksOrdersReportExport());
        }

        private object GetWorksOrderDetails(string partNumber)
        {
            return this.Negotiate.WithModel(this.worksOrdersService.GetWorksOrderDetails(partNumber))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get).WithView("Index");
        }

        private object UpdateWorksOrder()
        {
            var resource = this.Bind<WorksOrderResource>();

            return this.Negotiate.WithModel(this.worksOrdersService.UpdateWorksOrder(resource))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get).WithView("Index");
        }

        private object AddWorksOrder()
        {
            // TODO get auth user
            var resource = this.Bind<WorksOrderResource>();

            var test = this.worksOrdersService.AddWorksOrder(resource);

            return this.Negotiate.WithModel(this.worksOrdersService.AddWorksOrder(resource))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get).WithView("Index");
        }

        private object GetWorksOrder(int orderNumber)
        {
            return this.Negotiate.WithModel(this.worksOrdersService.GetWorksOrder(orderNumber))
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

        private object GetWorksOrders()
        {
            var resource = this.Bind<SearchRequestResource>();

            var worksOrders = string.IsNullOrEmpty(resource.SearchTerm)
                              ? this.worksOrdersService.GetAll()
                              : this.worksOrdersService.SearchWorksOrders(resource.SearchTerm);

            return this.Negotiate.WithModel(worksOrders).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
