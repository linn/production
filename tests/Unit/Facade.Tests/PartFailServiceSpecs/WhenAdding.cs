using FluentAssertions.Extensions;

namespace Linn.Production.Facade.Tests.PartFailServiceSpecs
{
    using System;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenAdding : ContextBase
    {
        private PartFailResource resource;

        private IResult<PartFail> result;

        [SetUp]
        public void SetUp()
        {
            this.DbService.GetNextVal(Arg.Any<string>()).Returns(1);

            this.resource = new PartFailResource
                                {
                                    Id = 1,
                                    WorksOrderNumber = 1,
                                    PartNumber = "PART",
                                    Batch = "BATCH",
                                    EnteredBy = 1,
                                    EnteredByName = "Colin",
                                    DateCreated = new DateTime().ToString("o"),
                                    ErrorType = "Error",
                                    FaultCode = "Fault",
                                    SerialNumber = 101,
                                    DateSentenced = 1.June(2024).ToString("o"),
                                    SentenceDecision = "SCRAP",
                                    SentenceReason = "Some"
                                };

            this.PartFailService.Create(Arg.Is<PartFail>(a =>
                    a.SentenceDecision == resource.SentenceDecision &&
                    a.DateSentenced == 1.June(2024) &&
                    a.SentenceReason == resource.SentenceReason))
                .Returns(new PartFail { Id = 1 });

            this.result = this.Sut.Add(this.resource);
        }

        [Test]
        public void ShouldCallService()
        {
            this.PartFailService.Received().Create(
                Arg.Is<PartFail>(r => r.Id == this.resource.Id && 
                                      r.SerialNumber == this.resource.SerialNumber &&
                                      r.SentenceDecision == resource.SentenceDecision &&
                                      r.SentenceReason == resource.SentenceReason));
        }

        [Test]
        public void ShouldAdd()
        {
            this.PartFailRepository.Received().Add(Arg.Any<PartFail>());
        }

        [Test]
        public void ShouldReturnCreated()
        {
            this.result.Should().BeOfType<CreatedResult<PartFail>>();
            var data = ((CreatedResult<PartFail>)this.result).Data;
            data.Id.Should().Be(1);
        }
    }
}
