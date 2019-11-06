namespace Linn.Production.Domain.Tests.ProductionTriggerAssemblySpecs
{
    using Linn.Production.Domain.LinnApps.Triggers;
    using NUnit.Framework;

    public class ContextBase
    {
        protected ProductionTriggerAssembly Sut { get; set; }

        [SetUp]
        public void SetUpContext()
        {
            this.Sut = new ProductionTriggerAssembly();
        }
    }
}
