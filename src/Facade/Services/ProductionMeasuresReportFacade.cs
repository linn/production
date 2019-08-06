namespace Linn.Production.Facade.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using CsvExtensions;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain;
    using Linn.Production.Domain.LinnApps.Measures;

    public class ProductionMeasuresReportFacade : IProductionMeasuresReportFacade
    {
        private readonly IRepository<ProductionMeasures, string> productionMeasuresRepository;

        public ProductionMeasuresReportFacade(IRepository<ProductionMeasures, string> productionMeasuresRepository)
        {
            this.productionMeasuresRepository = productionMeasuresRepository;
        }

        public IResult<IEnumerable<ProductionMeasures>> GetProductionMeasuresForCits()
        {
            var measures = this.productionMeasuresRepository.FindAll().ToList();
            return new SuccessResult<IEnumerable<ProductionMeasures>>(measures.Where(m => m.HasMeasures()));
        }

        public IResult<IEnumerable<IEnumerable<string>>> GetProductionMeasuresCsv()
        {
            var citMeasures = this.productionMeasuresRepository.FindAll().ToList();
            var results = new List<List<string>>() { ProductionMeasuresCsvExtensions.CsvHeaderLine().ToList()};
            results.AddRange(citMeasures.Where(m => m.HasMeasures()).Select(m => m.ToCsvLine().ToList()));
            return new SuccessResult<IEnumerable<IEnumerable<string>>>(results);
        }
    }
}