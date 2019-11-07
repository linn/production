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

    public class WhenUpdatingLabel : ContextBase
    {
        private WorksOrderLabelResource requestResource;

        [SetUp]
        public void SetUp()
        {
            var l = new WorksOrderLabel { Sequence = 1, PartNumber = "PART", LabelText = "text" };

            this.requestResource = new WorksOrderLabelResource { Sequence = 1, PartNumber = "PART", LabelText = "new text" };

            this.LabelService.Update(Arg.Any<WorksOrderLabelKey>(), Arg.Any<WorksOrderLabelResource>()).Returns(new SuccessResult<WorksOrderLabel>(l));

            this.Response = this.Browser.Put(
                "/production/works-orders/labels/1/PART",
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
            this.LabelService.Received().Update(Arg.Any<WorksOrderLabelKey>(), Arg.Any<WorksOrderLabelResource>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<WorksOrderLabelResource>();
            resource.Sequence.Should().Be(1);
        }
    }
}