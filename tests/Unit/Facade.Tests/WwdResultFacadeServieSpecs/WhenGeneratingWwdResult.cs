namespace Linn.Production.Facade.Tests.WwdResultFacadeServieSpecs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenGeneratingWwdResult : ContextBase
    {
        private IResult<WwdResult> result;

        [SetUp]
        public void SetUp()
        {
            var trigger = new ProductionTriggerLevel
            {
                PartNumber = "AKUB PARTY",
                WsName = "AKUBSTATION"
            };
            this.ProductionTriggerLevelRepository.FindById(Arg.Any<string>()).Returns(trigger);

            var details = new List<WwdDetail>()
            {
                new WwdDetail
                    {
                        PartNumber = "AKUB SPKR",
                        Description = "SOUND MAKING BIT",
                        QtyAtLocation = 1,
                        QtyKitted = 0,
                        QtyReserved = 0,
                        StoragePlace = "P666",
                        Remarks = string.Empty
                    },
                new WwdDetail
                    {
                        PartNumber = "AKUB PYRO",
                        Description = "NO PYRO NO PARTY",
                        QtyAtLocation = 0,
                        QtyKitted = 0,
                        QtyReserved = 0,
                        StoragePlace = "E-FA-STANDS",
                        Remarks = "Remember kids fireworks are dangerous"
                    }
            };
            this.WwdDetailRepository.FilterBy(Arg.Any<Expression<Func<WwdDetail, bool>>>()).Returns(details.AsQueryable());

            this.WwdTrigFunction.WwdTriggerRun(Arg.Any<string>(), Arg.Any<int>()).Returns(1);
            this.result = this.Sut.GenerateWwdResultForTrigger("AKUB PARTY", 1, string.Empty);
        }

        [Test]
        public void ShouldReturnSuccessResult()
        {
            this.result.Should().BeOfType<SuccessResult<WwdResult>>();
        }

        [Test]
        public void ShouldCallWwdTrigFunction()
        {
            this.WwdTrigFunction.Received().WwdTriggerRun(Arg.Any<string>(), Arg.Any<int>());
            this.result.As<SuccessResult<WwdResult>>().Data.WwdJobId.Should().Be(1);
        }

        [Test]
        public void ShouldGetWwdDetails()
        {
            this.WwdDetailRepository.Received().FilterBy(Arg.Any<Expression<Func<WwdDetail, bool>>>());
            this.result.As<SuccessResult<WwdResult>>().Data.WwdDetails.Count().Should().Be(2);
        }
    }
}