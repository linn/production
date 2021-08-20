namespace Linn.Production.Domain.Tests.BoardTestReportsSpecs
{
    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Domain.LinnApps.BoardTests;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected BoardTestReports Sut { get; set; }

        protected IRepository<BoardTest, BoardTestKey> BoardTestRepository { get; private set; }

        protected IReportingHelper ReportingHelper { get; private set; }

        protected IRepository<AteTestDetail, AteTestDetailKey> DetailRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.BoardTestRepository = Substitute.For<IRepository<BoardTest, BoardTestKey>>();
            this.ReportingHelper = new ReportingHelper();
            this.DetailRepository = Substitute.For<IRepository<AteTestDetail, AteTestDetailKey>>();
            this.Sut = new BoardTestReports(this.BoardTestRepository, this.ReportingHelper, this.DetailRepository);
        }
    }
}