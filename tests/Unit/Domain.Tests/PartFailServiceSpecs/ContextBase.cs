namespace Linn.Production.Domain.Tests.PartFailServiceSpecs
{
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.Services;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected PartFailService Sut { get; private set; }

        protected PartFail Candidate { get; set; }

        protected IRepository<WorksOrder, int> WorksOrderRepository { get; private set; }

        protected IRepository<PurchaseOrder, int> PurchaseOrderRepository { get; private set; }

        protected IRepository<Part, string> PartRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.WorksOrderRepository = Substitute.For<IRepository<WorksOrder, int>>();
            this.PurchaseOrderRepository = Substitute.For<IRepository<PurchaseOrder, int>>();
            this.PartRepository = Substitute.For<IRepository<Part, string>>();

            this.Sut = new PartFailService(
                this.WorksOrderRepository,
                this.PurchaseOrderRepository,
                this.PartRepository);
        }
    }
}