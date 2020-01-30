namespace Linn.Production.Domain.Tests.LabelPrintingServiceSpecs
{
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Products;
    using Linn.Production.Domain.LinnApps.RemoteServices;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected LabelPrintingService Sut { get; set; }

        protected ILabelService LabelService { get; set; }

        protected IBartenderLabelPack BartenderLabelPack { get; private set; }

        protected ILabelPack LabelPack { get; private set; }

        protected IRepository<ProductData, int> ProductDataRepository { get; private set; }

        protected IRepository<SerialNumber, int> SerialNumberRepository { get; private set; }

        protected ISernosPack SernosPack { get; private set; }

        protected IRepository<LabelType, string> LabelTypeRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.BartenderLabelPack = Substitute.For<IBartenderLabelPack>();
            this.LabelPack = Substitute.For<ILabelPack>();
            this.SernosPack = Substitute.For<ISernosPack>();
            this.ProductDataRepository = Substitute.For<IRepository<ProductData, int>>();
            this.SerialNumberRepository = Substitute.For<IRepository<SerialNumber, int>>();
            this.LabelTypeRepository = Substitute.For<IRepository<LabelType, string>>();
            this.LabelService = Substitute.For<ILabelService>();

            this.Sut = new LabelPrintingService(
                this.LabelService);
        }
    }
}