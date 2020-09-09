namespace Linn.Production.Service.Modules
{
    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources.RequestResources;
    using Linn.Production.Service.Extensions;
    using Linn.Production.Service.Models;
    using Nancy;
    using Nancy.ModelBinding;
    using Nancy.Security;
    using System.Collections.Generic;
    using System.Linq;

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
            var resource = this.Bind<ManufacturingTimingsRequestResource>();

            var result = this.manufacturingTimingsService.GetManufacturingTimingsReport(resource.StartDate, resource.EndDate, resource.CitCode);

            return this.Negotiate.WithModel(result).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetTimingsExport()
        {
            var resource = this.Bind<ManufacturingTimingsRequestResource>();

            var result = this.manufacturingTimingsService.GetManufacturingTimingsExport(
                                 resource.StartDate,
                                 resource.EndDate,
                                 resource.CitCode);

            var response = this.Negotiate.WithModel(result)
                .WithAllowedMediaRange("text/csv")
                .WithView("Index");

            return response;
        }
    }
}
