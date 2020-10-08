namespace Linn.Production.Domain.Tests.LabelPrintingServiceSpecs
{
    using FluentAssertions;

    using Linn.Production.Domain.LinnApps;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenPrintingAddressLabels : ContextBase
    {
        private LabelPrintResponse result;

        [SetUp]
        public void SetUp()
        {
            var labelPrint = new LabelPrint
            {
                LabelType = (int)GeneralPurposeLabelTypes.Labels.AddressLabel,
                Printer = (int)LabelPrinters.Printers.ProdLbl2,
                Quantity = 6,
                LinesForPrinting = new LabelPrintContents
                {
                    SupplierId = "77",
                    AddressId = "11",
                    Addressee = "la addressee",
                    Addressee2 = "duexieme addressee",
                    Line1 = "Large and small",
                    Line2 = "line2",
                    Line3 = "three",
                    Line4 = "four line",
                    PostalCode = "G1",
                    Country = "schottland"
                }
            };

            this.result = this.Sut.PrintLabel(labelPrint);
        }

        [Test]
        public void ShouldCallLabelPrintService()
        {
            var data = "\"la addressee\", \"duexieme addressee\", \"Large and small\", "
                       + "\"line2\", \"three\", \"four line\", \"G1\", \"schottland\"";
            this.LabelService.Received().PrintLabel(Arg.Any<string>(), "ProdLbl2", 6, "c:\\lbl\\genAddressLabel.btw", data);
        }

        [Test]
        public void ShouldReceiveCorrectResult()
        {
            this.result.Message.Should().Be("printed address labels");
        }
    }
}
