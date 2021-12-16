namespace Linn.Production.Service.Modules
{
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources;
    using Linn.Production.Resources.RequestResources;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class ManufacturingTimingsModule : NancyModule
    {
        private readonly IManufacturingTimingsFacadeService manufacturingTimingsService;

        public ManufacturingTimingsModule(
            IManufacturingTimingsFacadeService manufacturingTimingsService)
        {
            this.manufacturingTimingsService = manufacturingTimingsService;

            this.Get("/production/reports/manufacturing-timings", _ => this.GetTimingsReport());
            this.Get("/production/reports/bom-labour-routes", _ => this.GetBomLabourRoutesCsv());
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

        private object GetBomLabourRoutesCsv()
        {
            var resource = this.Bind<SearchRequestResource>();

            var result = this.manufacturingTimingsService.GetTimingsForAssembliesOnABom(
                resource.SearchTerm);

            var response = this.Negotiate.WithModel(result)
                .WithAllowedMediaRange("text/csv")
                .WithView("Index");

            return response;
        }
    }
}
