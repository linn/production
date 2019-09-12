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
            this.outstandingWorksOrdersReportFacade = outstandingWorksOrdersReportFacade;
            this.worksOrdersService = worksOrdersService;

            this.Get("/production/maintenance/works-orders/outstanding-works-orders-report", _ => this.GetOutstandingWorksOrdersReport());
            this.Get("/production/maintenance/works-orders/outstanding-works-orders-report/export", _ => this.GetOutstandingWorksOrdersReportExport());

            this.Get("/production/maintenance/works-orders/{orderNumber}", parameters => this.GetWorksOrder(parameters.orderNumber));
            this.Post("/production/maintenance/works-orders", _ => this.AddWorksOrder());
            this.Put("/production/maintenance/works-orders/{orderNumber}", _ => this.UpdateWorksOrder());
            this.Get(
                "/production/maintenance/works-orders/details/{partNumber}",
                parameters => this.GetWorksOrderDetails(parameters.partNumber));
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
            var resource = this.Bind<WorksOrderResource>();

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
            return this.Negotiate.WithModel(this.outstandingWorksOrdersReportFacade.GetOutstandingWorksOrdersReport())
                .WithMediaRangeModel("text/html", ApplicationSettings.Get).WithView("Index");
        }

        private object GetOutstandingWorksOrdersReportExport()
        {
            return this.Negotiate
                .WithModel(this.outstandingWorksOrdersReportFacade.GetOutstandingWorksOrdersReportCsv())
                .WithAllowedMediaRange("text/csv")
                .WithView("Index");
        }
    }
}
