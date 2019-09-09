namespace Linn.Production.Service.Tests.ManufacturingOperationsModuleSpecs
{
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using Nancy;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenGettingManufacturingOperation : ContextBase
    {
        private ManufacturingOperation manufacturingOperation;

        [SetUp]
        public void SetUp()
        {

            this.manufacturingOperation = new ManufacturingOperation("routecode1", 77, 15, "descrip of op", "codeOfOperation", "res Code", 27, 54, 5, "cit code test");

            this.ManufacturingOperationService.GetById(77)
                .Returns(new SuccessResult<ManufacturingOperation>(this.manufacturingOperation));

            this.Response = this.Browser.Get(
                "/production/resources/manufacturing-operations/77",
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
            this.ManufacturingOperationService.Received().GetById(this.manufacturingOperation.ManufacturingId);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ManufacturingOperationResource>();
            resource.RouteCode.Should().Be(this.manufacturingOperation.RouteCode);
            resource.ManufacturingId.Should().Be(this.manufacturingOperation.ManufacturingId);
            resource.Description.Should().Be(this.manufacturingOperation.Description);
            resource.SkillCode.Should().Be(this.manufacturingOperation.SkillCode);
            resource.ResourceCode.Should().Be(this.manufacturingOperation.ResourceCode);
            resource.SetAndCleanTime.Should().Be(this.manufacturingOperation.SetAndCleanTime);
            resource.CycleTime.Should().Be(this.manufacturingOperation.CycleTime);
            resource.LabourPercentage.Should().Be(this.manufacturingOperation.LabourPercentage);
            resource.CITCode.Should().Be(this.manufacturingOperation.CITCode);
        }
    }
}
