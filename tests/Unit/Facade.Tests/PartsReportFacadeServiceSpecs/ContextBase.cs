namespace Linn.Production.Facade.Tests.PartsReportFacadeServiceSpecs
{
    using Linn.Production.Domain.LinnApps.Reports;
    using Linn.Production.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected PartsReportFacadeService Sut { get; private set; }

        protected IPartsReportService PartsReportService { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.PartsReportService = Substitute.For<IPartsReportService>();
            this.Sut = new PartsReportFacadeService(this.PartsReportService);
        }
    }
}
