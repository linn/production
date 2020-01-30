namespace Linn.Production.Domain.Tests.LabelPrintingServiceSpecs
{
    using FluentAssertions;

    using Linn.Production.Domain.LinnApps;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenPrintingGoodsInLabels : ContextBase
    {
        private LabelPrintResponse result;

        [SetUp]
        public void SetUp()
        {
            var labelPrint = new LabelPrint
            {
                LabelType = (int)GeneralPurposeLabelTypes.Labels.GoodsInLabel,
                Printer = (int)LabelPrinters.Printers.ProdLbl2,
                Quantity = 9,
                LinesForPrinting = new LabelPrintContents
                {
                    AddressId = "11",
                    SupplierId = "77",
                    PoNumber = "PCAS",
                    PartNumber = "PCSM",
                    Qty = "99",
                    Initials = "IC",
                    Date = "30 Jan 2020"
                }
            };

            this.result = this.Sut.PrintLabel(labelPrint);
        }

        [Test]
        public void ShouldCallLabelPrintServiceForSmallLabel()
        {
            var data = "\"77\", \"11\", \"PCAS\", \"PCSM\", \"99\", \"IC\", \"30 Jan 2020\"";
            this.LabelService.Received().PrintLabel(Arg.Any<string>(), "ProdLbl2", 9, "c:\\lbl\\goods_in_2004.btw", data);
        }

        [Test]
        public void ShouldReceiveCorrectResult()
        {
            this.result.Message.Should().Be("printed goods in labels");
        }
    }
}
