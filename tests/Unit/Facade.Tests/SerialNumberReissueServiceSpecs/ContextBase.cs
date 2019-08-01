using Linn.Common.Persistence;
using Linn.Production.Domain.LinnApps.RemoteServices;
using Linn.Production.Domain.LinnApps.SerialNumberReissue;

namespace Linn.Production.Facade.Tests.SerialNumberReissueServiceSpecs
{
    using Services;
    using NSubstitute;
    using NUnit.Framework;

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
