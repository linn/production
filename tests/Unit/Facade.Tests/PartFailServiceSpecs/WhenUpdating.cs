using FluentAssertions.Extensions;

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
            this.PartFailService.Create(Arg.Any<PartFail>())
                .Returns(new PartFail
                {
                    Id = 1,
                    Part = new Part { PartNumber = "PART" },
                    Batch = "NEW BATCH",
                    EnteredBy = new Employee { Id = 1, FullName = "Colin" },
                    DateCreated = new DateTime(),
                    ErrorType = new PartFailErrorType(),
                    FaultCode = new PartFailFaultCode(),
                    SentenceDecision = "DESTROY",
                    SentenceReason = "Purge",
                    DateSentenced = 1.January(2012)
                });

            this.resource = new PartFailResource
            {
                Id = 1324,
                WorksOrderNumber = 1,
                PartNumber = "PART",
                Batch = "NEW BATCH",
                EnteredBy = 1,
                EnteredByName = "Colin",
                DateCreated = new DateTime().ToString("o"),
                ErrorType = "Error",
                FaultCode = "Fault",
                SerialNumber = 202,
                SentenceDecision = "DESTROY",
                SentenceReason = "Purge",
                DateSentenced = 1.January(2012).ToString("o")
            };

            this.PartFailRepository.FindById(1).Returns(new PartFail());

            this.result = this.Sut.Update(1, this.resource);
        }

        [Test]
        public void ShouldPassCorrectInfoForUpdate()
        {
            this.PartFailService.Received().Create(Arg.Is<PartFail>(p =>
                p.SerialNumber == this.resource.SerialNumber && 
                p.SentenceDecision == resource.SentenceDecision &&
                p.SentenceReason == resource.SentenceReason));

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
            dataResult.SentenceDecision.Should().Be(resource.SentenceDecision);
            dataResult.SentenceReason.Should().Be(resource.SentenceReason);
            dataResult.DateSentenced.Should().Be(1.January(2012));
        }
    }
}