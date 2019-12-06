namespace Linn.Production.Domain.Tests.LabelServiceSpecs
{
    using System;

    using FluentAssertions;

    using Linn.Production.Domain.LinnApps.Exceptions;

    using NUnit.Framework;

    public class WhenLabelReprintWithoutPart : ContextBase
    {
        private Action action;

        [SetUp]
        public void SetUp()
        {
            this.action = () => this.Sut.CreateLabelReprint(
                101202,
                "A good reason",
                null,
                808808,
                45,
                "BOX",
                1,
                "REPRINT",
                null);
        }

        [Test]
        public void ShouldThrowException()
        {
            this.action.Should().Throw<LabelReprintInvalidException>("No part number specified for label reprint");
        }
    }
}
