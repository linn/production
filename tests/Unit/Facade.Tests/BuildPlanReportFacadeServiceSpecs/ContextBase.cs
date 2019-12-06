namespace Linn.Production.Facade.Tests.BuildPlanReportFacadeServiceSpecs
{
    using Linn.Production.Domain.LinnApps.Reports;
    using Linn.Production.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected BuildPlansReportFacadeService Sut { get; set; }

        protected IBuildPlansReportService BuildPlansReportService { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.BuildPlansReportService = Substitute.For<IBuildPlansReportService>();
            this.Sut = new BuildPlansReportFacadeService(this.BuildPlansReportService);
        }
    }
}
