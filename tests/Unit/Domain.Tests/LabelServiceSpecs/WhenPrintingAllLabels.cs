namespace Linn.Production.Domain.Tests.LabelServiceSpecs
{
    using Linn.Production.Domain.LinnApps.Products;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenPrintingAllLabels : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.ProductDataRepository.FindById(808)
                .Returns(new ProductData { MACAddress = "99:99", ProductId = 808 });
            this.Sut.PrintAllLabels(808, "article");
        }

        [Test]
        public void ShouldGetProductDetails()
        {
            this.ProductDataRepository.Received().FindById(808);
        }

        [Test]
        public void ShouldGetLabelData()
        {
            this.LabelPack.Received().GetLabelData("BOX", 808, "article");
        }

        [Test]
        public void ShouldCallLabelPrintServiceForMACLabel()
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

        [Test]
        public void ShouldCallLabelPrintServiceForProductLabel()
        {
            var value = Arg.Any<string>();
            this.BartenderLabelPack.Received().PrintLabels(
                $"ProductReprint{808}",
                "ProdLbl1",
                3,
                "c:\\lbl\\prodlbl.btw",
                Arg.Any<string>(),
                ref value);
        }

        [Test]
        public void ShouldCallLabelPrintServiceForBoxLabel()
        {
            var value = Arg.Any<string>();
            this.BartenderLabelPack.Received().PrintLabels(
                $"BoxReprint{808}",
                "ProdLbl2",
                1,
                "c:\\lbl\\boxlbl_08ean.btw",
                Arg.Any<string>(),
                ref value);
        }
    }
}
