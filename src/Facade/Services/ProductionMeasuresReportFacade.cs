﻿namespace Linn.Production.Facade.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.Reports;
    using Linn.Production.Domain.LinnApps.Triggers;
    using Linn.Production.Facade.CsvExtensions;

    public class ProductionMeasuresReportFacade : IProductionMeasuresReportFacade
    {
        private readonly IRepository<ProductionMeasures, string> productionMeasuresRepository;

        private readonly ISingleRecordRepository<PtlMaster> ptlMasterRepository;

        private readonly ISingleRecordRepository<OsrRunMaster> osrRunMasterRepository;

        private readonly IProductionMeasuresReportService productionMeasuresReportService;

        public ProductionMeasuresReportFacade(
            IRepository<ProductionMeasures, string> productionMeasuresRepository,
            ISingleRecordRepository<PtlMaster> ptlMasterRepository,
            ISingleRecordRepository<OsrRunMaster> osrRunMasterRepository,
            IProductionMeasuresReportService productionMeasuresReportService)
        {
            this.productionMeasuresRepository = productionMeasuresRepository;
            this.ptlMasterRepository = ptlMasterRepository;
            this.osrRunMasterRepository = osrRunMasterRepository;
            this.productionMeasuresReportService = productionMeasuresReportService;
        }

        public IResult<IEnumerable<ProductionMeasures>> GetProductionMeasuresForCits()
        {
            var measures = this.productionMeasuresRepository.FindAll().ToList();
            return new SuccessResult<IEnumerable<ProductionMeasures>>(measures.Where(m => m.HasMeasures()));
        }

        public IResult<IEnumerable<IEnumerable<string>>> GetProductionMeasuresCsv()
        {
            var citMeasures = this.productionMeasuresRepository.FindAll().ToList();
            var results = new List<List<string>> { ProductionMeasuresCsvExtensions.CsvHeaderLine().ToList() };
            results.AddRange(citMeasures.Where(m => m.HasMeasures()).Select(m => m.ToCsvLine().ToList()));
            return new SuccessResult<IEnumerable<IEnumerable<string>>>(results);
        }

        public IResult<OsrInfo> GetOsrInfo()
        {
            var info = new OsrInfo
                           {
                              RunMaster = this.osrRunMasterRepository.GetRecord(),
                              PtlMaster = this.ptlMasterRepository.GetRecord()
                           };

            return new SuccessResult<OsrInfo>(info);
        }

        public IResult<IEnumerable<ResultsModel>> GetFailedPartsReport(
            string citCode,
            string partNumber,
            string orderByDate,
            bool excludeLinnProduced,
            string vendorManager,
            string stockPoolCode)
        {
            return new SuccessResult<IEnumerable<ResultsModel>>(this.productionMeasuresReportService.FailedPartsReport(
                citCode, 
                partNumber, 
                orderByDate,
                excludeLinnProduced,
                vendorManager,
                stockPoolCode));
        }

        public IResult<IEnumerable<ResultsModel>> GetDaysRequiredReport(string citCode)
        {
            return new SuccessResult<IEnumerable<ResultsModel>>(this.productionMeasuresReportService.DayRequiredReport(citCode));
        }
    }
}