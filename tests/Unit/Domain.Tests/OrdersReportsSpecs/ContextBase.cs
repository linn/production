namespace Linn.Production.Domain.Tests.OrdersReportsSpecs
{
    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Reports;
    using Linn.Production.Domain.LinnApps.ViewModels;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected OrdersReports Sut { get; set; }

        protected IQueryRepository<MCDLine> MCDLinesRepository { get; private set; }

        protected IReportingHelper ReportingHelper { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.MCDLinesRepository = Substitute.For<IQueryRepository<MCDLine>>();
            this.ReportingHelper = new ReportingHelper();
            this.Sut = new OrdersReports(this.MCDLinesRepository, this.ReportingHelper);
        }
    }
}