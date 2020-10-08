namespace Linn.Production.Domain.Tests.PartFailSpecs
{
    using Linn.Production.Domain.LinnApps.Measures;

    using NUnit.Framework;

    public class ContextBase
    {
        protected PartFail Sut { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.Sut = new PartFail { Id = 246 };
        }
    }
}