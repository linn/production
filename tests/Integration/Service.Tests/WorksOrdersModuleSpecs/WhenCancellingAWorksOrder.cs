namespace Linn.Production.Service.Tests.WorksOrdersModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCancellingAWorksOrder : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var requestResource = new WorksOrderResource
                                      {
                                          PartNumber = "MAJIK",
                                          OrderNumber = 12345,
                                          CancelledBy = 33067,
                                          ReasonCancelled = "REASON"
                                      };

            var worksOrder = new WorksOrder
                                 {
                                     PartNumber = "MAJIK",
                                     OrderNumber = 12345,
                                     CancelledBy = 33067,
                                     ReasonCancelled = "REASON",
                                     Part = new Part { Description = "DESC" }
                                 };

            this.WorksOrdersService.UpdateWorksOrder(Arg.Any<WorksOrderResource>())
                .Returns(new SuccessResult<WorksOrder>(worksOrder));

            this.Response = this.Browser.Put(
                "/production/works-orders/12345",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Header("Content-Type", "application/json");
                        with.JsonBody(requestResource);
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
            this.WorksOrdersService.Received()
                .UpdateWorksOrder(Arg.Is<WorksOrderResource>(r => r.OrderNumber == 12345));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<WorksOrderResource>();
            resource.PartNumber.Should().Be("MAJIK");
            resource.OrderNumber.Should().Be(12345);
            resource.CancelledBy.Should().Be(33067);
            resource.ReasonCancelled.Should().Be("REASON");
        }
    }
}