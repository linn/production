namespace Linn.Production.Domain.Tests.LabelServiceSpecs
{
    using System;

    using FluentAssertions;

    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Exceptions;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenLabelReprintReissueWithoutProductGroup : ContextBase
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
                        a[1] = 1;
                        a[2] = 1;
                    });
            this.LabelPack.GetLabelData("BOX", 808808, "part 1").Returns("data to be printed");
            this.LabelTypeRepository.FindById("BOX")
                .Returns(new LabelType { DefaultPrinter = "printer 1", Filename = "file 1" });
            this.SernosPack.GetProductGroup("part 1").Returns((string)null);
            this.SernosPack.SerialNumberExists(808808, "PART 1").Returns(true);

            this.action = () => this.Sut.CreateLabelReprint(
                101202,
                "A good reason",
                "part 1",
                808808,
                45,
                "BOX",
                1,
                "REISSUE",
                null);
        }

        [Test]
        public void ShouldThrowException()
        {
            this.action.Should().Throw<ProductGroupNotFoundException>();
        }
    }
}
