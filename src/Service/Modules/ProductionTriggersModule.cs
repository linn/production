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
            this.Get("/production/reports/triggers/facts", _ => this.GetProductionTriggerFacts());
        }

        private object GetProductionTriggers()
        {
            var resource = this.Bind<ProductionTriggersRequestResource>();

            var results =
                this.productionTriggersFacadeService.GetProductionTriggerReport(resource.Jobref, resource.CitCode);
            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetProductionTriggerFacts()
        {
            var resource = this.Bind<ProductionTriggersRequestResource>();

            var results =
                this.productionTriggersFacadeService.GetProductionTriggerFacts(resource.Jobref, resource.PartNumber);
            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}