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

    public class WhenGettingWorksOrderDetails : ContextBase
    {
        private string partNumber;

        [SetUp]
        public void SetUp()
        {
            this.partNumber = "pcas";

            var worksOrderDetails = new WorksOrderPartDetails { PartNumber = this.partNumber };

            this.WorksOrdersService.GetWorksOrderDetails(this.partNumber)
                .Returns(new SuccessResult<WorksOrderPartDetails>(worksOrderDetails));

            this.Response = this.Browser.Get(
                "/production/works-orders/get-part-details/pcas",
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
            this.WorksOrdersService.Received().GetWorksOrderDetails(this.partNumber);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<WorksOrderPartsDetailsResource>();
            resource.PartNumber.Should().Be(this.partNumber);
        }
    }
}
