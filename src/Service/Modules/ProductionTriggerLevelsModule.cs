namespace Linn.Production.Service.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Dispatchers;
    using Linn.Production.Domain.LinnApps.Exceptions;
    using Linn.Production.Domain.LinnApps.Triggers;
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

        private readonly ITriggerRunDispatcher triggerRunDispatcher;

        public ProductionTriggerLevelsModule(
            IFacadeService<ProductionTriggerLevel, string, ProductionTriggerLevelResource, ProductionTriggerLevelResource> productionTriggerLevelsService,
            ISingleRecordFacadeService<PtlSettings, PtlSettingsResource> ptlSettingsFacadeService,
            IAuthorisationService authorisationService,
            ITriggerRunDispatcher triggerRunDispatcher)
        {
            this.productionTriggerLevelsService = productionTriggerLevelsService;
            this.ptlSettingsFacadeService = ptlSettingsFacadeService;
            this.authorisationService = authorisationService;
            this.triggerRunDispatcher = triggerRunDispatcher;

            this.Get("production/maintenance/production-trigger-levels/{partNumber*}", parameters => this.GetProductionTriggerLevel(parameters.partNumber));
            this.Get("production/maintenance/production-trigger-levels", _ => this.GetProductionTriggerLevels());
            this.Put("production/maintenance/production-trigger-levels", _ => this.UpdateTriggerLevel());
            this.Post("production/maintenance/production-trigger-levels", _ => this.AddTriggerLevel());
            this.Get("production/maintenance/production-trigger-levels-settings", _ => this.GetProductionTriggerLevelsSettings());
            this.Put("production/maintenance/production-trigger-levels-settings", _ => this.UpdateProductionTriggerLevelsSettings());
            this.Post("production/maintenance/production-trigger-levels-settings/start-trigger-run", _ => this.StartTriggerRun());
        }

        private object StartTriggerRun()
        {
            this.RequiresAuthentication();
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();
            if (this.authorisationService.HasPermissionFor(AuthorisedAction.StartTriggerRun, privileges))
            {
                try
                {
                    this.triggerRunDispatcher.StartTriggerRun(this.Context?.CurrentUser?.GetEmployeeUri());
                    return HttpStatusCode.OK;
                }
                catch (Exception e)
                {
                    return this.Negotiate.WithModel(new BadRequestResult<Error>(e.Message));
                }
            }
            else
            {
                    return HttpStatusCode.Forbidden;
            }
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

        private object AddTriggerLevel()
        {
            var resource = this.Bind<ProductionTriggerLevelResource>();
            this.RequiresAuthentication();
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();
            var result = this.authorisationService.HasPermissionFor(AuthorisedAction.ProductionTriggerLevelUpdate, privileges)
                             ? this.productionTriggerLevelsService.Add(resource, privileges)
                             : new UnauthorisedResult<ResponseModel<ProductionTriggerLevel>>("You are not authorised to add trigger levels");

            return this.Negotiate.WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
        
        private object UpdateTriggerLevel()
        {
            var resource = this.Bind<ProductionTriggerLevelResource>();
            this.RequiresAuthentication();
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            var result = this.authorisationService.HasPermissionFor(AuthorisedAction.ProductionTriggerLevelUpdate, privileges)
                             ? this.productionTriggerLevelsService.Update(resource.PartNumber, resource, privileges)
                             : new UnauthorisedResult<ResponseModel<ProductionTriggerLevel>>("You are not authorised to update trigger level");

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
            this.RequiresAuthentication();
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            return this.Negotiate.WithModel(this.productionTriggerLevelsService.GetById(partNumber, privileges))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetProductionTriggerLevels()
        {
            var resource = this.Bind<SearchRequestResource>();

            this.RequiresAuthentication();
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            var parts = string.IsNullOrEmpty(resource.SearchTerm)
                            ? this.productionTriggerLevelsService.GetAll(privileges)
                            : this.productionTriggerLevelsService.Search(resource.SearchTerm, privileges);

            return this.Negotiate.WithModel(parts).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
