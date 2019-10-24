namespace Linn.Production.Domain.Tests.LabelServiceSpecs
{
    using Linn.Production.Domain.LinnApps.Products;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenPrintingMACLabel : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.ProductDataRepository.FindById(808)
                .Returns(new ProductData { MACAddress = "99:99", ProductId = 808 });
            this.Sut.PrintMACLabel(808);
        }

        [Test]
        public void ShouldGetProductDetails()
        {
            this.ProductDataRepository.Received().FindById(808);
        }

        [Test]
        public void ShouldCallLabelPrintService()
        {
            var value = Arg.Any<string>();
            this.BartenderLabelPack.Received().PrintLabels(
                $"MACReprintm{808}",
                "ProdLbl1",
                2,
                "c:\\lbl\\prodlblctr.btw",
                "99:99",
                ref value);
        }
    }
}
