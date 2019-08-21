namespace Linn.Production.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Facade.Services;
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

            this.Get("production/maintenance/production-trigger-levels", _ => this.GetProductionTriggerLevels());
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