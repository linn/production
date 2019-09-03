namespace Linn.Production.Facade.Tests.AssemblyFailsServiceSpecs
{
    using System;
    using System.Reflection;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenAdding : ContextBase
    {
        private AssemblyFailResource resource;

        private IResult<AssemblyFail> result;

        [SetUp]
        public void SetUp()
        {
            this.EmployeeRepository.FindById(12345678).Returns(new Employee { Id = 12345678, FullName = "Colin" });
            this.DbService.GetNextVal("ASSEMBLY_FAULTS_SEQ").Returns(1);
            this.WorksOrderRepository.FindById(99999999).Returns(new WorksOrder
                                                                     {
                                                                         OrderNumber = 99999999, Part = new Part { PartNumber = "PART", Description = "desc" }
                                                                     });
            this.resource = new AssemblyFailResource
                                {
                                    WorksOrderNumber = 99999999,
                                    EnteredBy = 12345678,
                                    EnteredByName = "Colin",
                                    PartNumber = "PART",
                                    PartDescription = "Something",
                                    NumberOfFails = 1,
                                    DateTimeFound = DateTime.Now.ToString("o")
                                };

            this.result = this.Sut.Add(this.resource);
        }

        [Test]
        public void ShouldAdd()
        {
            this.AssemblyFailRepository.Received().Add(Arg.Any<AssemblyFail>());
        }

        [Test]
        public void ShouldReturnCreated()
        {
            this.result.Should().BeOfType<CreatedResult<AssemblyFail>>();
            var data = ((CreatedResult<AssemblyFail>)this.result).Data;
            data.Id.Should().Be(1);
        }
    }
}