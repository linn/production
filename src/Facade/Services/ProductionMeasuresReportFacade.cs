namespace Linn.Production.Facade.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
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
    }
}