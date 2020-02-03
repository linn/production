namespace Linn.Production.Domain.Tests.ProductionTriggerSpecs
{
    using Linn.Production.Domain.LinnApps.Triggers;
    using NUnit.Framework;

    public abstract class ContextBase
    {
        public ProductionTrigger Sut { get; set; }

        [SetUp]
        public void SetUpContext()
        {
            this.Sut = new ProductionTrigger();
        }
    }
}
