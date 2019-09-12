namespace Linn.Production.Domain.Tests.WorksOrderFactorySpecs
{
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.PCAS;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Domain.LinnApps.WorksOrders;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected WorksOrderFactory Sut { get; private set; }

        protected IWorksOrderProxyService WorksOrderService { get; private set; }

        protected IRepository<Part, string> PartsRepository { get; private set; }

        protected IRepository<ProductionTriggerLevel, string> ProductionTriggerLevelsRepository { get; private set; }

        protected IRepository<PcasBoardForAudit, string> PcasBoardsForAuditRepository { get; private set; }

        protected IRepository<PcasRevision, string> PcasRevisionsRepository { get; private set; }

        protected IRepository<Department, string> DepartmentRepository { get; private set; }

        protected IRepository<Cit, string> CitRepository { get; private set; }

        protected ISernosPack SernosPack { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.WorksOrderService = Substitute.For<IWorksOrderProxyService>();
            this.PartsRepository = Substitute.For<IRepository<Part, string>>();
            this.ProductionTriggerLevelsRepository = Substitute.For<IRepository<ProductionTriggerLevel, string>>();
            this.PcasBoardsForAuditRepository = Substitute.For<IRepository<PcasBoardForAudit, string>>();
            this.PcasRevisionsRepository = Substitute.For<IRepository<PcasRevision, string>>();
            this.DepartmentRepository = Substitute.For<IRepository<Department, string>>();
            this.CitRepository = Substitute.For<IRepository<Cit, string>>();
            this.SernosPack = Substitute.For<ISernosPack>();

            this.Sut = new WorksOrderFactory(
                this.WorksOrderService,
                this.PartsRepository,
                this.ProductionTriggerLevelsRepository,
                this.PcasBoardsForAuditRepository,
                this.PcasRevisionsRepository,
                this.DepartmentRepository,
                this.CitRepository,
                this.SernosPack);
        }
    }
}
