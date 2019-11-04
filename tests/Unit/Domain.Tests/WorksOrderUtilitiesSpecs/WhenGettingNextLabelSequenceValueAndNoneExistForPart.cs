namespace Linn.Production.Domain.Tests.WorksOrderUtilitiesSpecs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Production.Domain.LinnApps.WorksOrders;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingNextLabelSequenceValueAndNoneExistForPart : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.LabelRepository.FilterBy(Arg.Any<Expression<Func<WorksOrderLabel, bool>>>()).Returns(
                new List<WorksOrderLabel>().AsQueryable());
        }

        [Test]
        public void ShouldReturnOneGreaterThanCurrentMax()
        {
            this.Sut.GetNextLabelSeqForPart("PART").Should().Be(1);
        }
    }
}
