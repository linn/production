namespace Linn.Production.Domain.Tests.BomServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected IBomService Sut { get; private set; }

        protected IQueryRepository<Bom> MockRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.MockRepository = Substitute.For<IQueryRepository<Bom>>();
            this.Sut = new BomService(this.MockRepository);
        }
    }
}
