namespace Linn.Production.Facade.Tests.ManufacturingOperationsServiceSpecs
{
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenAddingManufacturingOperation : ContextBase
    {
        private ManufacturingOperationResource resource;

        private ManufacturingOperation manufacturingOperation;

        private IResult<ManufacturingOperation> result;

        [SetUp]
        public void SetUp()
        {
            this.manufacturingOperation = new ManufacturingOperation(
                "routecode 1",
                77,
                15,
                "descrip of op",
                "codeOfOperation",
                "res Code",
                27,
                54,
                5,
                "cit code test");

            this.resource = new ManufacturingOperationResource
            {
                RouteCode = "routecode 1",
                ManufacturingId = 77,
                OperationNumber = 15,
                Description = "descrip of op",
                SkillCode = "codeOfOperation",
                ResourceCode = "res Code",
                SetAndCleanTime = 27,
                CycleTime = 54,
                LabourPercentage = 5,
                CITCode = "cit code test"
            };

            this.result = this.Sut.Add(this.resource);
        }

        [Test]
        public void ShouldAddManufacturingOperation()
        {
            this.ManufacturingOperationRepository.Received().Add(Arg.Any<ManufacturingOperation>());
        }

        [Test]
        public void ShouldReturnCreated()
        {
            this.result.Should().BeOfType<CreatedResult<ManufacturingOperation>>();
            var dataResult = ((CreatedResult<ManufacturingOperation>)this.result).Data;
            dataResult.RouteCode.Should().Be(this.manufacturingOperation.RouteCode);
            dataResult.ManufacturingId.Should().Be(this.manufacturingOperation.ManufacturingId);
            dataResult.Description.Should().Be(this.manufacturingOperation.Description);
            dataResult.SkillCode.Should().Be(this.manufacturingOperation.SkillCode);
            dataResult.ResourceCode.Should().Be(this.manufacturingOperation.ResourceCode);
            dataResult.SetAndCleanTime.Should().Be(this.manufacturingOperation.SetAndCleanTime);
            dataResult.CycleTime.Should().Be(this.manufacturingOperation.CycleTime);
            dataResult.LabourPercentage.Should().Be(this.manufacturingOperation.LabourPercentage);
            dataResult.CITCode.Should().Be(this.manufacturingOperation.CITCode);
        }
    }
}
