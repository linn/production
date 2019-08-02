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
        }

        private object GetProductionMeasuresForCits()
        {
            return this.Negotiate.WithModel(productionMeasuresReportFacade.GetProductionMeasuresForCits());
        }
    }
}