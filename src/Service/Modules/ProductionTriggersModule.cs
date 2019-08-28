namespace Linn.Production.Service.Modules
{
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;
    using Nancy;
    using Nancy.ModelBinding;

    public sealed class ProductionTriggersModule : NancyModule
    {
        private readonly IProductionTriggersFacadeService productionTriggersFacadeService;

        public ProductionTriggersModule(IProductionTriggersFacadeService productionTriggersFacadeService)
        {
            this.productionTriggersFacadeService = productionTriggersFacadeService;
            this.Get("/production/reports/triggers", _ => this.GetProductionTriggers());
        }

        private object GetProductionTriggers()
        {
            var resource = this.Bind<ProductionTriggersRequestResource>();
            var results =
                this.productionTriggersFacadeService.GetProductionTriggerReport(resource.Jobref, resource.CitCode,
                    resource.ReportType);
            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}