namespace Linn.Production.Service.Tests.PurchaseOrderModuleSpecs
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdating : ContextBase
    {
        private PurchaseOrderResource requestResource;

        [SetUp]
        public void SetUp()
        {
            var a = new PurchaseOrder
                        {
                            OrderNumber = 1,
                            OrderAddress = new Address { Country = new Country() },
                            Details = new List<PurchaseOrderDetail>()
                        };

            this.requestResource = new PurchaseOrderResource()
                                       {
                                           OrderNumber = 1,
                                       };

            this.FacadeService.Update(1, Arg.Any<PurchaseOrderResource>()).Returns(new SuccessResult<PurchaseOrder>(a));

            this.Response = this.Browser.Put(
                "/production/resources/purchase-orders/1",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.JsonBody(this.requestResource);
                    }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.FacadeService
                .Received()
                .Update(1, Arg.Is<PurchaseOrderResource>(r => r.OrderNumber == this.requestResource.OrderNumber));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<PurchaseOrderResource>();
            resource.OrderNumber.Should().Be(1);
        }
    }
}