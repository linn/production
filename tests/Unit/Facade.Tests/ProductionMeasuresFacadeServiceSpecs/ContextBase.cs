namespace Linn.Production.Facade.Tests.ProductionMeasuresFacadeServiceSpecs
{
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.Reports;
    using Linn.Production.Domain.LinnApps.Triggers;
    using Linn.Production.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected ProductionMeasuresReportFacade Sut { get; private set; }

        protected IProductionMeasuresReportService ProductionMeasuresReportService { get; private set; }

        protected IRepository<ProductionMeasures, string> ProductionMeasuresRepository { get; private set; }

        protected ISingleRecordRepository<PtlMaster> PtlMasterRepository { get; private set; }

        protected ISingleRecordRepository<OsrRunMaster> OsrRunMasterRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.ProductionMeasuresReportService = Substitute.For<IProductionMeasuresReportService>();
            this.ProductionMeasuresRepository = Substitute.For<IRepository<ProductionMeasures, string>>();
            this.PtlMasterRepository = Substitute.For<ISingleRecordRepository<PtlMaster>>();
            this.OsrRunMasterRepository = Substitute.For<ISingleRecordRepository<OsrRunMaster>>();
            this.Sut = new ProductionMeasuresReportFacade(
                this.ProductionMeasuresRepository,
                this.PtlMasterRepository,
                this.OsrRunMasterRepository,
                this.ProductionMeasuresReportService);
        }
    }
}
