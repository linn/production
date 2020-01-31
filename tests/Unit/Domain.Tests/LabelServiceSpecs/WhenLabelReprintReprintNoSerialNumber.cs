namespace Linn.Production.Domain.Tests.LabelServiceSpecs
{
    using System;

    using FluentAssertions;

    using Linn.Production.Domain.LinnApps;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenLabelReprintReprintNoSerialNumber : ContextBase
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
                        a[1] = 0;
                        a[2] = 0;
                    });
            this.LabelPack.GetLabelData("BOX", null, "PART 1").Returns("data to be printed");
            this.LabelTypeRepository.FindById("BOX")
                .Returns(new LabelType { DefaultPrinter = "printer 1", Filename = "file 1" });
            this.SernosPack.SerialNumbersRequired("PART 1").Returns(false);

            this.result = this.Sut.CreateLabelReprint(
                101202,
                "A good reason",
                "part 1",
                null,
                45,
                "BOX",
                1,
                "REPRINT",
                null);
        }

        [Test]
        public void ShouldNotReIssueSerialNumber()
        {
            this.SernosPack.DidNotReceive().ReIssueSernos(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<int>());
        }

        [Test]
        public void ShouldNotCheckSerialNumberExists()
        {
            this.SernosPack.DidNotReceive().SerialNumberExists(Arg.Any<int>(), "PART 1");
        }

        [Test]
        public void ShouldNotRebuildSerialNumber()
        {
            this.SerialNumberRepository.DidNotReceive().Add(Arg.Any<SerialNumber>());
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
                "LabelReprint",
                "printer 1",
                1,
                "file 1",
                "data to be printed",
                ref value);
        }

        [Test]
        public void ShouldReturnLabelReprint()
        {
            this.result.PartNumber.Should().Be("PART 1");
            this.result.NewPartNumber.Should().BeNull();
            this.result.LabelTypeCode.Should().Be("BOX");
            this.result.SerialNumber.Should().BeNull();
            this.result.ReprintType.Should().Be("REPRINT");
            this.result.RequestedBy.Should().Be(101202);
            this.result.DocumentType.Should().Be("WO");
            this.result.WorksOrderNumber.Should().Be(45);
            this.result.DateIssued.Should().BeCloseTo(DateTime.UtcNow, 10000);
            this.result.NumberOfProducts.Should().Be(1);
        }
    }
}
