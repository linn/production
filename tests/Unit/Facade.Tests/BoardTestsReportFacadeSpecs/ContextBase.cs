namespace Linn.Production.Facade.Tests.BoardTestsReportFacadeSpecs
{
    using Linn.Production.Domain.LinnApps.BoardTests;
    using Linn.Production.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected BoardTestReportFacadeService Sut { get; set; }

        protected IBoardTestReports BoardTestReports { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.BoardTestReports = Substitute.For<IBoardTestReports>();
            this.Sut = new BoardTestReportFacadeService(this.BoardTestReports);
        }
    }
}