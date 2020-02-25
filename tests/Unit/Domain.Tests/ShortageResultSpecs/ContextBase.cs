namespace Linn.Production.Domain.Tests.ShortageResultSpecs
{
    using Linn.Production.Domain.LinnApps.Models;
    using NUnit.Framework;

    public abstract class ContextBase
    {
        public ShortageResult Sut { get; set; }

        [SetUp]
        public void SetUpContext()
        {
            this.Sut = new ShortageResult();
        }
    }
}
