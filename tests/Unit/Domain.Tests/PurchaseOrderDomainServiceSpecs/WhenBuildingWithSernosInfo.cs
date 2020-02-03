namespace Linn.Production.Domain.Tests.PurchaseOrderDomainServiceSpecs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Models;
    using Linn.Production.Domain.LinnApps.ViewModels;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenBuildingWithSernosInfo : ContextBase
    {
        private PurchaseOrder order;
        private PurchaseOrderWithSernosInfo result;

        [SetUp]
        public void SetUp()
        {
            this.order = new PurchaseOrder
                             {
                                 Details = new List<PurchaseOrderDetail>
                                               {
                                                    new PurchaseOrderDetail()
                                               }
                             };
            this.SernosBuiltRepository.FilterBy(Arg.Any<Expression<Func<SernosBuilt, bool>>>()).Returns(
                new List<SernosBuilt>
                    {
                        new SernosBuilt { SernosGroup = "GROUP" },
                        new SernosBuilt { SernosGroup = "GROUP" }
                    }.AsQueryable());

            this.SernosIssuedRepository.FilterBy(Arg.Any<Expression<Func<SernosIssued, bool>>>()).Returns(
                new List<SernosIssued>
                    {
                        new SernosIssued { SernosGroup = "GROUP", SernosNumber = 1 },
                        new SernosIssued { SernosGroup = "GROUP", SernosNumber = 2 }
                    }.AsQueryable());

            this.PurchaseOrdersReceivedRepository.FilterBy(Arg.Any<Expression<Func<PurchaseOrdersReceived, bool>>>())
                .Returns(new List<PurchaseOrdersReceived>
                             {
                                 new PurchaseOrdersReceived { QuantityNetReceived = 1 }
                             }.AsQueryable());

            this.result = this.Sut.BuildPurchaseOrderWithSernosInfo(this.order);
        }

        [Test]
        public void ShouldReturnPurchaseOrderWithSernosInfoOnTheDetails()
        {
            this.result.DetaisWithSernosInfo.Count().Should().Be(1);
            this.result.DetaisWithSernosInfo.FirstOrDefault()?.QuantityReceived.Should().Be(1);
            this.result.DetaisWithSernosInfo.FirstOrDefault()?.FirstSernos.Should().Be(1);
            this.result.DetaisWithSernosInfo.FirstOrDefault()?.LastSernos.Should().Be(2);
            this.result.DetaisWithSernosInfo
                .FirstOrDefault()
                ?.SernosBuilt.Should().Be(2); // Count of Sernos Built returned by SernosBuiltRepository
            this.result.DetaisWithSernosInfo.FirstOrDefault()?.QuantityReceived.Should().Be(1);
        }
    }
}