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
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using Linn.Production.Service.Extensions;
    using Linn.Production.Service.Models;
    using Nancy;
    using Nancy.ModelBinding;
    using Nancy.Security;

    public sealed class ProductionTriggerLevelsModule : NancyModule
    {
        private readonly IProductionTriggerLevelsService productionTriggerLevelsService;

        private readonly ISingleRecordFacadeService<PtlSettings, PtlSettingsResource> ptlSettingsFacadeService;

        private readonly IAuthorisationService authorisationService;

        private readonly ITriggerRunDispatcher triggerRunDispatcher;

        public ProductionTriggerLevelsModule(
           IProductionTriggerLevelsService productionTriggerLevelsService,
           ISingleRecordFacadeService<PtlSettings, PtlSettingsResource> ptlSettingsFacadeService,
            IAuthorisationService authorisationService,
            ITriggerRunDispatcher triggerRunDispatcher)
        {
            this.productionTriggerLevelsService = productionTriggerLevelsService;
            this.ptlSettingsFacadeService = ptlSettingsFacadeService;
            this.authorisationService = authorisationService;
            this.triggerRunDispatcher = triggerRunDispatcher;

            this.Get("production/maintenance/production-trigger-levels/create", _ => this.GetApp());
            this.Get("production/maintenance/production-trigger-levels/application-state", _ => this.GetApp());
            this.Get("production/maintenance/production-trigger-levels/{partNumber*}", parameters => this.GetProductionTriggerLevel(parameters.partNumber));
            this.Get("production/maintenance/production-trigger-levels", _ => this.GetProductionTriggerLevels());
            this.Put("production/maintenance/production-trigger-levels/{partNumber*}", parameters => this.UpdateTriggerLevel(parameters.partNumber));
            this.Post("production/maintenance/production-trigger-levels", _ => this.AddTriggerLevel());
            this.Get("production/maintenance/production-trigger-levels-settings", _ => this.GetProductionTriggerLevelsSettings());
            this.Put("production/maintenance/production-trigger-levels-settings", _ => this.UpdateProductionTriggerLevelsSettings());
            this.Post("production/maintenance/production-trigger-levels-settings/start-trigger-run", _ => this.StartTriggerRun());
            this.Delete("production/maintenance/production-trigger-levels/{partNumber*}", parameters => this.DeleteTriggerLevel(parameters.partNumber));
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

        private object UpdateTriggerLevel(string partNumber)
        {
            var resource = this.Bind<ProductionTriggerLevelResource>();
            this.RequiresAuthentication();
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            var result = this.authorisationService.HasPermissionFor(AuthorisedAction.ProductionTriggerLevelUpdate, privileges)
                             ? this.productionTriggerLevelsService.Update(partNumber, resource, privileges)
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
            var resource = this.Bind<ProductionTriggerLevelsSearchRequestResource>();

            this.RequiresAuthentication();
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            IResult<ResponseModel<IEnumerable<ProductionTriggerLevel>>> triggers;

            if (!string.IsNullOrWhiteSpace(resource.SearchTerm)
                || !string.IsNullOrWhiteSpace(resource.CitSearchTerm)
                || (!string.IsNullOrWhiteSpace(resource.OverrideSearchTerm) && resource.OverrideSearchTerm != "null")
                || (!string.IsNullOrWhiteSpace(resource.AutoSearchTerm) && resource.AutoSearchTerm != "null"))
            {
                triggers = this.productionTriggerLevelsService.Search(resource, privileges);
            }
            else
            {
                triggers = this.productionTriggerLevelsService.GetAll(privileges);
            }

            return this.Negotiate.WithModel(triggers).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object DeleteTriggerLevel(string partNumber)
        {
            this.RequiresAuthentication();
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            var result = this.authorisationService.HasPermissionFor(AuthorisedAction.ProductionTriggerLevelUpdate, privileges)
                             ? this.productionTriggerLevelsService.Remove(partNumber, privileges)
                             : new UnauthorisedResult<ResponseModel<ProductionTriggerLevel>>("You are not authorised to delete trigger level");

            return this.Negotiate.WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetApp()
        {
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            return this.Negotiate
                .WithModel(new SuccessResult<ResponseModel<ProductionTriggerLevel>>(new ResponseModel<ProductionTriggerLevel>(new ProductionTriggerLevel(), privileges)))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
