namespace Linn.Production.Service.Modules
{
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
    using System.Collections.Generic;
    using System.Linq;

    public sealed class MetalWorkTimingsModule : NancyModule
    {
        private readonly IMetalWorkTimingsFacadeService metalWorkTimingsService;

        private readonly IAuthorisationService authorisationService;

        public MetalWorkTimingsModule(
            IMetalWorkTimingsFacadeService metalWorkTimingsService, IAuthorisationService authorisationService)
        {
            this.metalWorkTimingsService = metalWorkTimingsService;
            this.authorisationService = authorisationService;

            this.Get("/production/reports/mw-timings", _ => this.GetTimingsForDates());
            this.Get("/production/reports/mw-timings/export", _ => this.GetTimingsExportForDates());
        }

        private object GetTimingsForDates()
        {
            this.RequiresAuthentication();
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();
            var resource = this.Bind<SearchByDatesRequestResource>();

            var result = this.authorisationService.HasPermissionFor(AuthorisedAction.MetalWorkTimings, privileges)
                             ? this.metalWorkTimingsService.GetMetalWorkTimingsReport(resource.StartDate, resource.EndDate)
                : new UnauthorisedResult<ResultsModel>("You are not authorised to view timings report.");

            return this.Negotiate.WithModel(result).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetTimingsExportForDates()
        {
            this.RequiresAuthentication();
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();
            var resource = this.Bind<SearchByDatesRequestResource>();

            var result = this.authorisationService.HasPermissionFor(AuthorisedAction.MetalWorkTimings, privileges)
                             ? this.metalWorkTimingsService.GetMetalWorkTimingsExport(resource.StartDate, resource.EndDate)
                             : new UnauthorisedResult<IEnumerable<IEnumerable<string>>>("You are not authorised to view timings report.");

            var response = this.Negotiate.WithModel(result)
                .WithAllowedMediaRange("text/csv")
                .WithView("Index");

            return response;
        }
    }
}
