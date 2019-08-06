namespace Linn.Production.Service.Modules
{
    using Linn.Production.Facade.Services;
    using Nancy;

    public sealed class ProductionMeasuresModule : NancyModule
    {
        private readonly IProductionMeasuresReportFacade productionMeasuresReportFacade;

        public ProductionMeasuresModule(IProductionMeasuresReportFacade productionMeasuresReportFacade)
        {
            this.productionMeasuresReportFacade = productionMeasuresReportFacade;

            this.Get("/production/reports/measures/cits", _ => this.GetProductionMeasuresForCits());
            this.Get("/production/reports/measures/export", _ => this.GetProductionMeasuresExport());
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
    }
}