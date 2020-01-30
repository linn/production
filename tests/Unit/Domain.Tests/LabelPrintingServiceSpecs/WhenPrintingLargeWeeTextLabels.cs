namespace Linn.Production.Domain.Tests.LabelPrintingServiceSpecs
{
    using Linn.Production.Domain.LinnApps;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenPrintingLargeWeeTextLabels : ContextBase
    {
        private LabelPrintResponse result;

        [SetUp]
        public void SetUp()
        {
            var labelPrint = new LabelPrint
            {
                LabelType = (int)GeneralPurposeLabelTypes.Labels.LargeWeeText,
                Printer = (int)LabelPrinters.Printers.ProdLbl2,
                Quantity = 3,
                LinesForPrinting = new LabelPrintContents
                {
                    Line1 = "Large and small",
                    Line2 = "line2",
                    Line3 = "three",
                    Line4 = "four line",
                    Line5 = "funf",
                    Line6 = "siiiixz",
                    Line7 = "siben"
                }
            };

            this.result = this.Sut.PrintLabel(labelPrint);
        }

        [Test]
        public void ShouldCallLabelPrintServiceForSmallLabel()
        {
            var data = "\"Large and small\", \"line2\", \"three\", \"four line\", \"funf\", \"siiiixz\", \"siben\"";
            this.LabelService.Received().PrintLabel(Arg.Any<string>(), "ProdLbl2", 3, "c:\\lbl\\genLargeLabel.btw", data);
        }

        [Test]
        public void ShouldReceiveCorrectResult()
        {
            Assert.AreEqual(this.result.Message, "printed large (wee text) labels");
        }
    }
}
