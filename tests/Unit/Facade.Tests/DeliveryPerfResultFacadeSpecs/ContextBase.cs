namespace Linn.Production.Facade.Tests.DeliveryPerfResultFacadeSpecs
{
    using Linn.Production.Domain.LinnApps.Reports;
    using Linn.Production.Facade.Services;
    using NSubstitute;
    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected DeliveryPerfResultFacadeService Sut { get; set; }

        protected IDeliveryPerformanceReportService DeliveryPerformanceReportService { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.DeliveryPerformanceReportService = Substitute.For<IDeliveryPerformanceReportService>();
            this.Sut = new DeliveryPerfResultFacadeService(this.DeliveryPerformanceReportService);
        }
    }
}
