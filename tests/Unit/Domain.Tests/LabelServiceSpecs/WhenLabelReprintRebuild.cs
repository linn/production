namespace Linn.Production.Domain.Tests.LabelServiceSpecs
{
    using FluentAssertions;

    using Linn.Production.Domain.LinnApps;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenLabelReprintRebuild : ContextBase
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
            this.LabelPack.GetLabelData("BOX", 808808, "PART 1").Returns("data to be printed");
            this.LabelTypeRepository.FindById("BOX")
                .Returns(new LabelType { DefaultPrinter = "printer 1", Filename = "file 1" });
            this.SernosPack.GetProductGroup("PART 1").Returns("product group 1");
            this.SernosPack.SerialNumberExists(808808, "PART 1").Returns(true);

            this.result = this.Sut.CreateLabelReprint(
                101202,
                "A good reason",
                "part 1",
                808808,
                45,
                "BOX",
                1,
                "REBUILD",
                null);
        }

        [Test]
        public void ShouldRebuildSerialNumber()
        {
            this.SerialNumberRepository.Received().Add(Arg.Is<SerialNumber>(s => s.SernosNumber == 808808 && s.TransCode == "REBUILD"));
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
            this.result.NewPartNumber.Should().BeNull();
            this.result.LabelTypeCode.Should().Be("BOX");
        }
    }
}
