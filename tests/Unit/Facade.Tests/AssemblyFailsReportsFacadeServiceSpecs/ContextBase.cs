namespace Linn.Production.Facade.Tests.AssemblyFailsReportsFacadeServiceSpecs
{
    using Linn.Production.Domain.LinnApps.Reports;
    using Linn.Production.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected AssemblyFailsReportsFacadeService Sut { get; set; }

        protected IAssemblyFailsReportService ReportService { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.ReportService = Substitute.For<IAssemblyFailsReportService>();
            this.Sut = new AssemblyFailsReportsFacadeService(this.ReportService);
        }
    }
}