﻿namespace Linn.Production.Facade.Tests.ManufacturingOperationsServiceSpecs
{
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenUpdatingManufacturingOperation : ContextBase
    {
        private ManufacturingOperationResource resource;

        private IResult<ManufacturingOperation> result;

        private ManufacturingOperation manufacturingOperation;

        [SetUp]
        public void SetUp()
        {
            this.manufacturingOperation = new ManufacturingOperation(
                "routecode 1",
                77,
                15,
                "descrip of op",
                "codeOfSkill",
                "res Code",
                27,
                54,
                5,
                "cit code test",
                10);

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
                CITCode = "cit code test",
                ResourcePercentage = 10
            };

            this.ManufacturingOperationRepository
                .FindById(this.manufacturingOperation.ManufacturingId)
                .Returns(this.manufacturingOperation);

            this.result = this.Sut.Update(this.manufacturingOperation.ManufacturingId, this.resource);
        }

        [Test]
        public void ShouldGetManufacturingOperation()
        {
            this.ManufacturingOperationRepository.Received().FindById(this.manufacturingOperation.ManufacturingId);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<ManufacturingOperation>>();
            var dataResult = ((SuccessResult<ManufacturingOperation>)this.result).Data;
            dataResult.RouteCode.Should().Be(this.manufacturingOperation.RouteCode);
            dataResult.ManufacturingId.Should().Be(this.manufacturingOperation.ManufacturingId);
            dataResult.Description.Should().Be(this.manufacturingOperation.Description);
            dataResult.SkillCode.Should().Be(this.manufacturingOperation.SkillCode);
            dataResult.ResourceCode.Should().Be(this.manufacturingOperation.ResourceCode);
            dataResult.SetAndCleanTime.Should().Be(this.manufacturingOperation.SetAndCleanTime);
            dataResult.CycleTime.Should().Be(this.manufacturingOperation.CycleTime);
            dataResult.LabourPercentage.Should().Be(this.manufacturingOperation.LabourPercentage);
            dataResult.CITCode.Should().Be(this.manufacturingOperation.CITCode);
            dataResult.ResourcePercentage.Should().Be(this.manufacturingOperation.ResourcePercentage);
        }
    }
}
