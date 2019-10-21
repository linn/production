namespace Linn.Production.Facade.Tests.OrdersReportsFacadeSpecs
{
    using Linn.Production.Domain.LinnApps.Reports;
    using Linn.Production.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected OrdersReportsFacadeService Sut { get; set; }

        protected IManufacturingCommitDateReport ManufacturingCommitDateReport { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.ManufacturingCommitDateReport = Substitute.For<IManufacturingCommitDateReport>();
            this.Sut = new OrdersReportsFacadeService(this.ManufacturingCommitDateReport);
        }
    }
}