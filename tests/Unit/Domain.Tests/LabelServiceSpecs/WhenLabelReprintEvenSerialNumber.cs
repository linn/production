namespace Linn.Production.Domain.Tests.LabelServiceSpecs
{
    using System;

    using FluentAssertions;

    using Linn.Production.Domain.LinnApps.Exceptions;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenLabelReprintEvenSerialNumber : ContextBase
    {
        private int noOfSerialNumbers;
        private int noOfBoxes;

        private Action action;

        [SetUp]
        public void SetUp()
        {
            this.SernosPack.When(a => a.GetSerialNumberBoxes("part 1", out this.noOfSerialNumbers, out this.noOfBoxes))
                .Do(a =>
                    {
                        a[1] = 2;
                        a[2] = 2;
                    });

            this.action = () => this.Sut.CreateLabelReprint(
                101202,
                "A good reason",
                "part 1",
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
            this.action.Should().Throw<LabelReprintInvalidException>("You must use the odd number serial number of a pair");
        }
    }
}
