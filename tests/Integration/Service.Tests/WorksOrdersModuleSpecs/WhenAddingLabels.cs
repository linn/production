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

    public class WhenAddingLabels : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var requestResource = new WorksOrderLabelResource { PartNumber = "PART", Sequence = 1, };

            var label = new WorksOrderLabel
                                 {
                                     PartNumber = "PART",
                                     Sequence = 1,
                                 };

            this.LabelService.Add(Arg.Any<WorksOrderLabelResource>())
                .Returns(new CreatedResult<WorksOrderLabel>(label));

            this.Response = this.Browser.Post(
                "/production/works-orders/labels",
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
            this.LabelService.Received().Add(Arg.Is<WorksOrderLabelResource>(r => r.PartNumber == "PART"));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<WorksOrderLabelResource>();
            resource.PartNumber.Should().Be("PART");
        }
    }
}