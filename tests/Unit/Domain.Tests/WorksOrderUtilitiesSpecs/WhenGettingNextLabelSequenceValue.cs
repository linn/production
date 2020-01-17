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

    public class WhenGettingNextLabelSequenceValue : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var labels = new List<WorksOrderLabel>
                             {
                                 new WorksOrderLabel { PartNumber = "PART", Sequence = 1 },
                                 new WorksOrderLabel { PartNumber = "PART", Sequence = 2 },
                                 new WorksOrderLabel { PartNumber = "PART", Sequence = 3 }
                             };

            this.LabelRepository.FilterBy(Arg.Any<Expression<Func<WorksOrderLabel, bool>>>()).Returns(labels.AsQueryable());
        }

        [Test]
        public void ShouldReturnOneGreaterThanCurrentMax()
        {
            this.Sut.GetNextLabelSeqForPart("PART").Should().Be(4);
        }
    }
}