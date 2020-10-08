namespace Linn.Production.Service.Modules
{
    using Linn.Production.Facade.Services;
    using Linn.Production.Resources.RequestResources;
    using Linn.Production.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class ProductionMeasuresModule : NancyModule
    {
        private readonly IProductionMeasuresReportFacade productionMeasuresReportFacade;

        public ProductionMeasuresModule(IProductionMeasuresReportFacade productionMeasuresReportFacade)
        {
            this.productionMeasuresReportFacade = productionMeasuresReportFacade;

            this.Get("/production/reports/measures/cits", _ => this.GetProductionMeasuresForCits());
            this.Get("/production/reports/measures/info", _ => this.GetProductionMeasuresInfo());
            this.Get("/production/reports/measures/export", _ => this.GetProductionMeasuresExport());
            this.Get("/production/reports/failed-parts", _ => this.GetFailedPartsReport());
            this.Get("/production/reports/days-required", _ => this.GetDaysRequiredReport());
        }

        private object GetDaysRequiredReport()
        {
            var requestResource = this.Bind<CitCodeRequestResource>();
            return this.Negotiate
                .WithModel(this.productionMeasuresReportFacade.GetDaysRequiredReport(requestResource.CitCode))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetFailedPartsReport()
        {
            var requestResource = this.Bind<CitCodeRequestResource>();
            return this.Negotiate
                .WithModel(this.productionMeasuresReportFacade.GetFailedPartsReport(requestResource.CitCode))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetProductionMeasuresForCits()
        {
            return this.Negotiate.WithModel(this.productionMeasuresReportFacade.GetProductionMeasuresForCits());
        }

        private object GetProductionMeasuresExport()
        {
            return this.Negotiate
                .WithModel(this.productionMeasuresReportFacade.GetProductionMeasuresCsv())
                .WithAllowedMediaRange("text/csv")
                .WithView("Index");
        }

        private object GetProductionMeasuresInfo()
        {
            return this.Negotiate.WithModel(this.productionMeasuresReportFacade.GetOsrInfo());
        }
    }
}