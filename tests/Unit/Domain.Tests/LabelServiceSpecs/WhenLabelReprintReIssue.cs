namespace Linn.Production.Domain.Tests.LabelServiceSpecs
{
    using FluentAssertions;

    using Linn.Production.Domain.LinnApps;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenLabelReprintReIssue : ContextBase
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
                        a[1] = 1;
                        a[2] = 1;
                    });
            this.LabelPack.GetLabelData("BOX", 808808, "NEW PART").Returns("data to be printed");
            this.LabelTypeRepository.FindById("BOX")
                .Returns(new LabelType { DefaultPrinter = "printer 1", Filename = "file 1" });
            this.SernosPack.GetProductGroup("PART 1").Returns("product group");
            this.SernosPack.SerialNumberExists(808808, "PART 1").Returns(true);

            this.result = this.Sut.CreateLabelReprint(
                101202,
                "A good reason",
                "part 1",
                808808,
                45,
                "BOX",
                1,
                "REISSUE",
                "new part");
        }

        [Test]
        public void ShouldReIssueSerialNumber()
        {
            this.SernosPack.Received().ReIssueSernos("PART 1", "NEW PART", 808808);
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
                "LabelReprint808808",
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
            this.result.NewPartNumber.Should().Be("NEW PART");
            this.result.LabelTypeCode.Should().Be("BOX");
        }
    }
}
