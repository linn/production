﻿namespace Linn.Production.Facade.Tests.ManufacturingOperationsServiceSpecs
{
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected ManufacturingOperationsService Sut { get; set; }

        protected IRepository<ManufacturingOperation, string> ManufacturingOperationRepository { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.ManufacturingOperationRepository = Substitute.For<IRepository<ManufacturingOperation, string>>();
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.Sut = new ManufacturingOperationsService(this.ManufacturingOperationRepository, this.TransactionManager);
        }
    }
}