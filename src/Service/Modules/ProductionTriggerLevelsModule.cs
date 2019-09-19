namespace Linn.Production.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class ProductionTriggerLevelsModule : NancyModule
    {
        private readonly IFacadeService<ProductionTriggerLevel, string, ProductionTriggerLevelResource, ProductionTriggerLevelResource> productionTriggerLevelsService;

        public ProductionTriggerLevelsModule(
            IFacadeService<ProductionTriggerLevel, string, ProductionTriggerLevelResource, ProductionTriggerLevelResource> productionTriggerLevelsService)
        {
            this.productionTriggerLevelsService = productionTriggerLevelsService;

            this.Get("production/maintenance/production-trigger-levels/{partNumber*}", parameters => this.GetProductionTriggerLevel(parameters.partNumber));
            this.Get("production/maintenance/production-trigger-levels", _ => this.GetProductionTriggerLevels());
        }

        private object GetProductionTriggerLevel(string partNumber)
        {
            return this.Negotiate.WithModel(this.productionTriggerLevelsService.GetById(partNumber))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetProductionTriggerLevels()
        {
            var resource = this.Bind<SearchRequestResource>();

            var parts = string.IsNullOrEmpty(resource.SearchTerm)
                            ? this.productionTriggerLevelsService.GetAll()
                            : this.productionTriggerLevelsService.Search(resource.SearchTerm);

            return this.Negotiate.WithModel(parts).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}