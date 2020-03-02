namespace Linn.Production.Domain.Tests.LabelPrintingServiceSpecs
{
    using FluentAssertions;

    using Linn.Production.Domain.LinnApps;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenPrintingSmallLabels1Line: ContextBase
    {
        private LabelPrintResponse result;

        [SetUp]
        public void SetUp()
        {
            var labelPrint = new LabelPrint
            {
                LabelType = (int)GeneralPurposeLabelTypes.Labels.Small,
                Printer = (int)LabelPrinters.Printers.ProdLbl1,
                Quantity = 2,
                LinesForPrinting = new LabelPrintContents { Line1 = "this line 1", Line2 = " " }
            };

            this.result = this.Sut.PrintLabel(labelPrint);
        }

        [Test]
        public void ShouldCallLabelPrintServiceForSmallLabel()
        {
            this.LabelService.Received().PrintLabel(Arg.Any<string>(), "ProdLbl1", 2, "c:\\lbl\\genSmallLabel.btw", "this line 1");
        }

        [Test]
        public void ShouldReceiveCorrectResult()
        {
            this.result.Message.Should().Be("printed small labels");
        }
    }
}
