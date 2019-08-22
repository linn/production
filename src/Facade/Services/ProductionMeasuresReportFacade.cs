namespace Linn.Production.Facade.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using CsvExtensions;
    using Domain.LinnApps.Triggers;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.Measures;
    using Persistence.LinnApps.Repositories;

    public class ProductionMeasuresReportFacade : IProductionMeasuresReportFacade
    {
        private readonly IRepository<ProductionMeasures, string> productionMeasuresRepository;

        private readonly IMasterRepository<PtlMaster> ptlMasterRepository;

        private readonly IMasterRepository<OsrRunMaster> osrRunMasterRepository;

        public ProductionMeasuresReportFacade(IRepository<ProductionMeasures, string> productionMeasuresRepository, IMasterRepository<PtlMaster> ptlMasterRepository, IMasterRepository<OsrRunMaster> osrRunMasterRepository)
        {
            this.productionMeasuresRepository = productionMeasuresRepository;
            this.ptlMasterRepository = ptlMasterRepository;
            this.osrRunMasterRepository = osrRunMasterRepository;
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

        public IResult<OsrInfo> GetOsrInfo()
        {
            var info = new OsrInfo
            {
               RunMaster = osrRunMasterRepository.GetMasterRecord(),
               PtlMaster = ptlMasterRepository.GetMasterRecord()
            };

            return new SuccessResult<OsrInfo>(info);
        }
    }
}