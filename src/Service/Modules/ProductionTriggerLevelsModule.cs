namespace Linn.Production.Service.Modules
{
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Common;
    using Linn.Production.Domain.LinnApps.Triggers;
    using Linn.Production.Facade.Common;
    using Linn.Production.Resources;
    using Linn.Production.Service.Extensions;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;
    using Nancy.Security;

    public sealed class ProductionTriggerLevelsModule : NancyModule
    {
        private readonly IFacadeService<ProductionTriggerLevel, string, ProductionTriggerLevelResource, ProductionTriggerLevelResource> productionTriggerLevelsService;

        private readonly ISingleRecordFacadeService<PtlSettings, PtlSettingsResource> ptlSettingsFacadeService;

        private readonly IAuthorisationService authorisationService;

        public ProductionTriggerLevelsModule(
            IFacadeService<ProductionTriggerLevel, string, ProductionTriggerLevelResource, ProductionTriggerLevelResource> productionTriggerLevelsService,
            ISingleRecordFacadeService<PtlSettings, PtlSettingsResource> ptlSettingsFacadeService,
            IAuthorisationService authorisationService)
        {
            this.productionTriggerLevelsService = productionTriggerLevelsService;
            this.ptlSettingsFacadeService = ptlSettingsFacadeService;
            this.authorisationService = authorisationService;

            this.Get("production/maintenance/production-trigger-levels/{partNumber*}", parameters => this.GetProductionTriggerLevel(parameters.partNumber));
            this.Get("production/maintenance/production-trigger-levels", _ => this.GetProductionTriggerLevels());
            this.Get("production/maintenance/production-trigger-levels-settings", _ => this.GetProductionTriggerLevelsSettings());
            this.Put("production/maintenance/production-trigger-levels-settings", _ => this.UpdateProductionTriggerLevelsSettings());
        }

        private object UpdateProductionTriggerLevelsSettings()
        {
            var resource = this.Bind<PtlSettingsResource>();
            this.RequiresAuthentication();
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();
            var result = this.authorisationService.HasPermissionFor(AuthorisedAction.PtlSettingsUpdate, privileges)
                                 ? this.ptlSettingsFacadeService.Update(resource, privileges)
                                 : new UnauthorisedResult<ResponseModel<PtlSettings>>("No permissions found for updating ptl settings");

            return this.Negotiate.WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetProductionTriggerLevelsSettings()
        {
            this.RequiresAuthentication();
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            return this.Negotiate.WithModel(this.ptlSettingsFacadeService.Get(privileges))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
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