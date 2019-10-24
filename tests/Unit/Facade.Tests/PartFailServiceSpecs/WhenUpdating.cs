namespace Linn.Production.Facade.Tests.PartFailServiceSpecs
{
    using System;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdating : ContextBase
    {
        private PartFailResource resource;

        private IResult<PartFail> result;

        [SetUp]
        public void SetUp()
        {
            this.EmployeeRepository.FindById(1).Returns(new Employee { Id = 1, FullName = "Colin" });

            var partFail = new PartFail
            {
                Id = 1,
                Part = new Part() { PartNumber = "PART" },
                Batch = "BATCH",
                EnteredBy = new Employee { Id = 1, FullName = "Colin" },
                DateCreated = new DateTime(),
                ErrorType = new PartFailErrorType(),
                FaultCode = new PartFailFaultCode()
            }; 

            this.resource = new PartFailResource
            {
                Id = 1,
                WorksOrderNumber = 1,
                PartNumber = "PART",
                Batch = "NEW BATCH",
                EnteredBy = 1,
                EnteredByName = "Colin",
                DateCreated = new DateTime().ToString("o"),
                ErrorType = "Error",
                FaultCode = "Fault"
            };

            this.PartFailService.Create(Arg.Any<PartFail>()).Returns(new PartFail
            {
                Id = 1,
                Part = new Part() { PartNumber = "PART" },
                Batch = "NEW BATCH",
                EnteredBy = new Employee { Id = 1, FullName = "Colin" },
                DateCreated = new DateTime(),
                ErrorType = new PartFailErrorType(),
                FaultCode = new PartFailFaultCode()
            });

            this.PartFailRepository.FindById(1).Returns(partFail);

            this.result = this.Sut.Update(1, this.resource);
        }

        [Test]
        public void ShouldGet()
        {
            this.PartFailRepository.Received().FindById(1);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<PartFail>>();
            var dataResult = ((SuccessResult<PartFail>)this.result).Data;
            dataResult.Batch.Should().Be("NEW BATCH");
        }
    }
}