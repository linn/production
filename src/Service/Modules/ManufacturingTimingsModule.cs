namespace Linn.Production.Service.Modules
{
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using Linn.Production.Service.Extensions;
    using Linn.Production.Service.Models;
    using Nancy;
    using Nancy.ModelBinding;
    using Nancy.Security;

    public sealed class ManufacturingTimingsModule : NancyModule
    {
        private readonly IManufacturingTimingsFacadeService manufacturingTimingsService;

        private readonly IAuthorisationService authorisationService;

        public ManufacturingTimingsModule(
            IManufacturingTimingsFacadeService manufacturingTimingsService, IAuthorisationService authorisationService)
        {
            this.manufacturingTimingsService = manufacturingTimingsService;
            this.authorisationService = authorisationService;

            this.Get("/production/reports/manufacturing-timings", _ => this.GetTimingsReport());
            this.Get("/production/reports/manufacturing-timings/export", _ => this.GetTimingsExport());
        }

        private object GetTimingsReport()
        {
            this.RequiresAuthentication();
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();
            var resource = this.Bind<ManufacturingTimingsRequestResource>();

            var result = this.authorisationService.HasPermissionFor(AuthorisedAction.MetalWorkTimings, privileges)
                             ? this.manufacturingTimingsService.GetManufacturingTimingsReport(resource.StartDate, resource.EndDate, resource.CitCode)
                : new UnauthorisedResult<ResultsModel>("You are not authorised to view timings report.");

            return this.Negotiate.WithModel(result).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetTimingsExport()
        {
            this.RequiresAuthentication();
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();
            var resource = this.Bind<ManufacturingTimingsRequestResource>();

            var result = this.authorisationService.HasPermissionFor(AuthorisedAction.MetalWorkTimings, privileges)
                             ? this.manufacturingTimingsService.GetManufacturingTimingsExport(resource.StartDate, resource.EndDate, resource.CitCode)
                             : new UnauthorisedResult<IEnumerable<IEnumerable<string>>>("You are not authorised to view timings report.");

            var response = this.Negotiate.WithModel(result)
                .WithAllowedMediaRange("text/csv")
                .WithView("Index");

            return response;
        }
    }
}
