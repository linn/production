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

    using Linn.Production.Persistence.LinnApps.Repositories;

    public sealed class ManufacturingTimingsModule : NancyModule
    {
        private readonly IManufacturingTimingsFacadeService manufacturingTimingsService;

        public ManufacturingTimingsModule(
            IManufacturingTimingsFacadeService manufacturingTimingsService)
        {
            this.manufacturingTimingsService = manufacturingTimingsService;

            this.Get("/production/reports/manufacturing-timings", _ => this.GetTimingsReport());
            this.Get("/production/reports/manufacturing-timings/export", _ => this.GetTimingsExport());
        }

        private object GetTimingsReport()
        
        {
            var x = this.manufacturingTimingsService.GetTimingsForAssembliesOnABom("KLI DSM/A/3");
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
