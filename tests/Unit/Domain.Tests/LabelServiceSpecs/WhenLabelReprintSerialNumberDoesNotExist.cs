namespace Linn.Production.Domain.Tests.LabelServiceSpecs
{
    using System;

    using FluentAssertions;

    using Linn.Production.Domain.LinnApps.Exceptions;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenLabelReprintSerialNumberDoesNotExist : ContextBase
    {
        private int noOfSerialNumbers;
        private int noOfBoxes;

        private Action action;

        [SetUp]
        public void SetUp()
        {
            this.SernosPack.When(a => a.GetSerialNumberBoxes("PART 1", out this.noOfSerialNumbers, out this.noOfBoxes))
                .Do(a =>
                    {
                        a[1] = 1;
                        a[2] = 1;
                    });
            this.SernosPack.SerialNumbersRequired("PART 1").Returns(true);
            this.SernosPack.SerialNumberExists(808808, "PART 1").Returns(false);

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
            this.action.Should().Throw<InvalidSerialNumberException>();
        }
    }
}
