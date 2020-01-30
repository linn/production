namespace Linn.Production.Domain.Tests.LabelPrintingServiceSpecs
{
    using FluentAssertions;

    using Linn.Production.Domain.LinnApps;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenPrintingSmallWeeBoldTextLabels : ContextBase
    {
        private LabelPrintResponse result;

        [SetUp]
        public void SetUp()
        {
            var labelPrint = new LabelPrint
            {
                LabelType = (int)GeneralPurposeLabelTypes.Labels.SmallBoldText,
                Printer = (int)LabelPrinters.Printers.ProdLbl1,
                Quantity = 7,
                LinesForPrinting = new LabelPrintContents { Line1 = "bold yin" }
            };

            this.result = this.Sut.PrintLabel(labelPrint);
        }

        [Test]
        public void ShouldCallLabelPrintServiceForSmallLabel()
        {
            this.LabelService.Received().PrintLabel(Arg.Any<string>(), "ProdLbl1", 7, "c:\\lbl\\genSmallLabel3b.btw", "bold yin");
        }

        [Test]
        public void ShouldReceiveCorrectResult()
        {
            this.result.Message.Should().Be("printed small (wee bold text) labels");
        }
    }
}
