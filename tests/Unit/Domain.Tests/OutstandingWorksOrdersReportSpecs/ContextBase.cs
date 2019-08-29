namespace Linn.Production.Domain.Tests.OutstandingWorksOrdersReportSpecs
{
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Domain.LinnApps.Reports;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected OutstandingWorksOrdersReportService Sut { get; set; }

        protected IOutstandingWorksOrdersReportDatabaseService DatabaseService { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.DatabaseService = Substitute.For<IOutstandingWorksOrdersReportDatabaseService>();
            this.Sut = new OutstandingWorksOrdersReportService(this.DatabaseService);
        }
    }
}
