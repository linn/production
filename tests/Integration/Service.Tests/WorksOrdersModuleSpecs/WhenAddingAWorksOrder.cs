namespace Linn.Production.Service.Tests.WorksOrdersModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenAddingAWorksOrder : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var requestResource = new WorksOrderResource
                                      {
                                          PartNumber = "MAJIK",
                                          OrderNumber = 12345
                                      };
            var worksOrder = new WorksOrder
                                 {
                                     PartNumber = "MAJIK",
                                     OrderNumber = 12345
                                 };

            this.WorksOrdersService.AddWorksOrder(Arg.Any<WorksOrderResource>())
                .Returns(new CreatedResult<WorksOrder>(worksOrder));

            this.Response = this.Browser.Post(
                "/production/maintenance/works-orders",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Header("Content-Type", "application/json");
                        with.JsonBody(requestResource);
                    }).Result;
        }

        [Test]
        public void ShouldReturnCreated()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public void ShouldCallService()
        {
            this.WorksOrdersService.Received().AddWorksOrder(Arg.Is<WorksOrderResource>(r => r.PartNumber == "MAJIK"));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<WorksOrderResource>();
            resource.PartNumber.Should().Be("MAJIK");
            resource.OrderNumber.Should().Be(12345);
        }
    }
}
