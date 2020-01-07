namespace Linn.Production.Facade.Tests.LabelReprintFacadeServiceSpecs
{
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected LabelReprintFacadeService Sut { get; set; }

        protected IRepository<LabelReprint, int> LabelReprintRepository { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        protected ILabelService LabelService { get; private set; }

    [SetUp]
        public void SetUpContext()
        {
            this.LabelReprintRepository = Substitute.For<IRepository<LabelReprint, int>>();
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.LabelService = Substitute.For<ILabelService>();

            this.Sut = new LabelReprintFacadeService(
                this.LabelReprintRepository,
                this.TransactionManager,
                this.LabelService);
        }
    }
}