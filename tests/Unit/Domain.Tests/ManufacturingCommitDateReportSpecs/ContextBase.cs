namespace Linn.Production.Domain.Tests.ManufacturingCommitDateReportSpecs
{
    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Reports;
    using Linn.Production.Domain.LinnApps.ViewModels;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected ManufacturingCommitDateReport Sut { get; set; }

        protected IQueryRepository<MCDLine> MCDLinesRepository { get; private set; }

        protected IReportingHelper ReportingHelper { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.MCDLinesRepository = Substitute.For<IQueryRepository<MCDLine>>();
            this.ReportingHelper = new ReportingHelper();
            this.Sut = new ManufacturingCommitDateReport(this.MCDLinesRepository, this.ReportingHelper);
        }
    }
}