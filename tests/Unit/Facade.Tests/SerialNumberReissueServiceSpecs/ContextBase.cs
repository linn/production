namespace Linn.Production.Facade.Tests.SerialNumberReissueServiceSpecs
{
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Domain.LinnApps.SerialNumberReissue;

    using NSubstitute;

    using NUnit.Framework;

    using Services;

    public abstract class ContextBase
    {
        protected SerialNumberReissueService Sut { get; set; }

        protected ISernosRenumPack SernosRenumPack { get; private set; }

        protected IRepository<SerialNumberReissue, int> SerialNumberResissueRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.SernosRenumPack = Substitute.For<ISernosRenumPack>();
            this.SerialNumberResissueRepository = Substitute.For<IRepository<SerialNumberReissue, int>>();

            this.Sut = new SerialNumberReissueService(this.SernosRenumPack, this.SerialNumberResissueRepository);
        }
    }
}
