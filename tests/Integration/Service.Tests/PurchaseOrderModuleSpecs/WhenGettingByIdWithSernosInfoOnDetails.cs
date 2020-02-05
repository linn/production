namespace Linn.Production.Service.Tests.PurchaseOrderModuleSpecs
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Models;
    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingByIdWithSernosInfoOnDetails : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var detail = new PurchaseOrderDetail
                             {
                                 Part = new Part()
                             };

            var l = new PurchaseOrderWithSernosInfo
                        {
                            OrderNumber = 1,
                            OrderAddress = new Address(),
                            DetaisWithSernosInfo = new List<PurchaseOrderDetailWithSernosInfo>
                                                       {
                                                           new PurchaseOrderDetailWithSernosInfo(detail)
                                                       }
                        };

            this.FacadeService.GetPurchaseOrderWithSernosInfo(Arg.Any<int>()).Returns(new SuccessResult<PurchaseOrderWithSernosInfo>(l));

            this.Response = this.Browser.Get(
                "/production/resources/purchase-orders/1",
                with => { with.Header("Accept", "application/json"); }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.FacadeService.Received().GetPurchaseOrderWithSernosInfo(Arg.Any<int>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<PurchaseOrderWithSernosInfoResource>();
            resource.OrderNumber.Should().Be(1);
        }
    }
}