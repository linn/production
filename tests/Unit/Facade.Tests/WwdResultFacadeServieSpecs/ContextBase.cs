namespace Linn.Production.Facade.Tests.WwdResultFacadeServieSpecs
{
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Facade.Services;
    using NSubstitute;
    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected WwdResultFacadeService Sut { get; set; }

        protected IWwdTrigFunction WwdTrigFunction { get; set; }

        protected IRepository<ProductionTriggerLevel, string> ProductionTriggerLevelRepository { get; set; }

        protected IQueryRepository<WwdDetail> WwdDetailRepository { get; set; }

        [SetUp]
        public void SetUpContext()
        {
            this.WwdTrigFunction = Substitute.For<IWwdTrigFunction>();
            this.ProductionTriggerLevelRepository = Substitute.For<IRepository<ProductionTriggerLevel, string>>();
            this.WwdDetailRepository = Substitute.For<IQueryRepository<WwdDetail>>();

            this.Sut = new WwdResultFacadeService(this.WwdTrigFunction, this.ProductionTriggerLevelRepository, this.WwdDetailRepository);
        }
    }
}
