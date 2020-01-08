namespace Linn.Production.Facade.Tests.AteReportsServiceSpecs
{
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected AteReportsFacadeService Sut { get; set; }

        protected IAteReportsService AteReportsService { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.AteReportsService = Substitute.For<IAteReportsService>();

            this.Sut = new AteReportsFacadeService(this.AteReportsService);
        }
    }
}