﻿namespace Linn.Production.Domain.Tests.LabelServiceSpecs
{
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Products;
    using Linn.Production.Domain.LinnApps.RemoteServices;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected LabelService Sut { get; set; }

        protected IBartenderLabelPack BartenderLabelPack { get; private set; }

        protected ILabelPack LabelPack { get; private set; }

        protected IRepository<ProductData, int> ProductDataRepository { get; private set; }

        protected IRepository<SerialNumber, int> SerialNumberRepository { get; private set; }

        protected ISernosPack SernosPack { get; private set; }

        protected ISalesArticleService SalesArticleService { get; private set; }

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
            this.SalesArticleService = Substitute.For<ISalesArticleService>();

            this.Sut = new LabelService(
                this.BartenderLabelPack,
                this.LabelPack,
                this.ProductDataRepository,
                this.SerialNumberRepository,
                this.SernosPack,
                this.LabelTypeRepository,
                this.SalesArticleService);
        }
    }
}
