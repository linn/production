namespace Linn.Production.Domain.Tests.PurchaseOrderSpecs
{
    using System.Collections.Generic;

    using Linn.Production.Domain.LinnApps;

    using NUnit.Framework;

    public class ContextBase
    {
        protected PurchaseOrder Sut { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.Sut = new PurchaseOrder
                           {
                               OrderNumber = 1,
                           };
        }
    }
}