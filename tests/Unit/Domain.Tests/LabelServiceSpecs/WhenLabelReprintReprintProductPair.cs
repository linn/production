namespace Linn.Production.Domain.Tests.LabelServiceSpecs
{
    using System;

    using FluentAssertions;

    using Linn.Production.Domain.LinnApps;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenLabelReprintReprintProductPair : ContextBase
    {
        private int noOfSerialNumbers;
        private int noOfBoxes;

        private LabelReprint result;

        [SetUp]
        public void SetUp()
        {
            this.SernosPack.When(a => a.GetSerialNumberBoxes("PART 1", out this.noOfSerialNumbers, out this.noOfBoxes))
                .Do(a =>
                    {
                        a[1] = 2;
                        a[2] = 2;
                    });
            this.LabelPack.GetLabelData("BOX", 808807, "PART 1").Returns("data to be printed 1");
            this.LabelPack.GetLabelData("BOX", 808808, "PART 1").Returns("data to be printed 2");
            this.LabelTypeRepository.FindById("BOX")
                .Returns(new LabelType { DefaultPrinter = "printer 1", Filename = "file 1" });
            this.SernosPack.SerialNumberExists(808807, "PART 1").Returns(true);

            this.result = this.Sut.CreateLabelReprint(
                101202,
                "A good reason",
                "part 1",
                808807,
                45,
                "BOX",
                1,
                "REPRINT",
                null);
        }

        [Test]
        public void ShouldGetSerialNumberDetails()
        {
            this.SernosPack.Received().GetSerialNumberBoxes("PART 1", out this.noOfSerialNumbers, out this.noOfBoxes);
        }

        [Test]
        public void ShouldCallLabelPrintService()
        {
            var value = Arg.Any<string>();
            this.BartenderLabelPack.Received().PrintLabels(
                "LabelReprint808807",
                "printer 1",
                1,
                "file 1",
                "data to be printed 1",
                ref value);

            this.BartenderLabelPack.Received().PrintLabels(
                "LabelReprint808807B",
                "printer 1",
                1,
                "file 1",
                "data to be printed 2",
                ref value);
        }

        [Test]
        public void ShouldReturnLabelReprint()
        {
            this.result.PartNumber.Should().Be("PART 1");
            this.result.NewPartNumber.Should().BeNull();
            this.result.LabelTypeCode.Should().Be("BOX");
            this.result.SerialNumber.Should().Be(808807);
            this.result.ReprintType.Should().Be("REPRINT");
            this.result.RequestedBy.Should().Be(101202);
            this.result.DocumentType.Should().Be("WO");
            this.result.WorksOrderNumber.Should().Be(45);
            this.result.DateIssued.Should().BeCloseTo(DateTime.UtcNow, 10000);
            this.result.NumberOfProducts.Should().Be(1);
        }
    }
}
