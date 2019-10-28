namespace Linn.Production.Facade.Tests.SmtReportsFacadeServiceSpecs
{
    using Linn.Production.Domain.LinnApps.Reports.Smt;
    using Linn.Production.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected SmtReportsFacadeService Sut { get; set; }

        protected ISmtReports SmtReportsService { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.SmtReportsService = Substitute.For<ISmtReports>();
            this.Sut = new SmtReportsFacadeService(this.SmtReportsService);
        }
    }
}