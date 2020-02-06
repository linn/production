namespace Linn.Production.Domain.Tests.PurchaseOrderDomainServiceSpecs
{
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Domain.LinnApps.Services;
    using Linn.Production.Domain.LinnApps.ViewModels;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected PurchaseOrderDomainService Sut { get; set; }

        protected IRepository<PurchaseOrder, int> PurchaseOrderRepository { get; set; }

        protected IQueryRepository<SernosIssued> SernosIssuedRepository { get; set; }

        protected IQueryRepository<SernosBuilt> SernosBuiltRepository { get; set; }

        protected IQueryRepository<PurchaseOrdersReceived> PurchaseOrdersReceivedRepository { get; set; }

        protected ISernosPack SernosPack { get; set; }

        [SetUp]
        public void SetUpContext()
        {
            this.SernosPack = Substitute.For<ISernosPack>();
            this.PurchaseOrderRepository = Substitute.For<IRepository<PurchaseOrder, int>>();
            this.SernosIssuedRepository = Substitute.For<IQueryRepository<SernosIssued>>();
            this.SernosBuiltRepository = Substitute.For<IQueryRepository<SernosBuilt>>();
            this.PurchaseOrdersReceivedRepository = Substitute.For<IQueryRepository<PurchaseOrdersReceived>>();
            this.Sut = new PurchaseOrderDomainService(
                this.SernosIssuedRepository, 
                this.SernosBuiltRepository, 
                this.SernosPack,
                this.PurchaseOrdersReceivedRepository);
        }
    }
}