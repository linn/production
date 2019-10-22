namespace Linn.Production.Domain.Tests.OverdueOrdersReportSpecs
{
    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Reports;
    using Linn.Production.Domain.LinnApps.ViewModels;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected OverdueOrdersReportService Sut;

        protected IQueryRepository<OverdueOrderLine> OverdueOrderRepository;

        protected IRepository<AccountingCompany, string> AccountingCompaniesRepository;


        protected IReportingHelper ReportingHelper { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.OverdueOrderRepository = Substitute.For<IQueryRepository<OverdueOrderLine>>();
            this.AccountingCompaniesRepository = Substitute.For<IRepository<AccountingCompany, string>>();
            this.ReportingHelper = new ReportingHelper();

            this.Sut = new OverdueOrdersReportService(this.OverdueOrderRepository, this.ReportingHelper, this.AccountingCompaniesRepository);
        }
    }
}
