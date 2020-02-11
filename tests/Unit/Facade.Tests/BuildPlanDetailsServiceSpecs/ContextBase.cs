namespace Linn.Production.Facade.Tests.BuildPlanDetailsServiceSpecs
{
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.BuildPlans;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected BuildPlanDetailService Sut { get; set; }

        protected IRepository<BuildPlanDetail, BuildPlanDetailKey> BuildPlanDetailRepository { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        protected ILinnWeekPack LinnWeekPack { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.BuildPlanDetailRepository = Substitute.For<IRepository<BuildPlanDetail, BuildPlanDetailKey>>();
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.LinnWeekPack = Substitute.For<ILinnWeekPack>();

            this.Sut = new BuildPlanDetailService(this.BuildPlanDetailRepository, this.TransactionManager, this.LinnWeekPack);
        }
    }
}
