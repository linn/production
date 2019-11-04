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

    public class WhenGettingLabelById : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var l = new WorksOrderLabel { Sequence = 1, PartNumber = "PART", LabelText = "hello" };

            this.LabelService.GetById(Arg.Any<WorksOrderLabelKey>()).Returns(new SuccessResult<WorksOrderLabel>(l));

            this.Response = this.Browser.Get(
                "/production/works-orders/labels/1/PART",
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
            this.LabelService.Received().GetById(Arg.Any<WorksOrderLabelKey>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<WorksOrderLabelResource>();
            resource.Sequence.Should().Be(1);
        }
    }
}