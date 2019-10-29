namespace Linn.Production.Domain.LinnApps
{
    using Linn.Common.Domain.Exceptions;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.Products;
    using Linn.Production.Domain.LinnApps.RemoteServices;

    public class LabelService : ILabelService
    {
        private readonly IBartenderLabelPack bartenderLabelPack;

        private readonly ILabelPack labelPack;

        private readonly IRepository<ProductData, int> productDataRepository;

        private string message;

        public LabelService(
            IBartenderLabelPack bartenderLabelPack,
            ILabelPack labelPack,
            IRepository<ProductData, int> productDataRepository)
        {
            this.labelPack = labelPack;
            this.bartenderLabelPack = bartenderLabelPack;
            this.productDataRepository = productDataRepository;
        }

        public void PrintMACLabel(int serialNumber)
        {
            this.PrintMACAddressLabel(serialNumber, this.GetMACAddress(serialNumber));
        }

        public void PrintAllLabels(int serialNumber, string articleNumber)
        {
            var labelData = this.labelPack.GetLabelData("BOX", serialNumber, articleNumber, "B");
            string macAddress = null;
            try
            {
                macAddress = this.GetMACAddress(serialNumber);
            }
            catch (DomainException) {
                // Still print the other labels if the product does not have a MAC address
            }

            if (!string.IsNullOrEmpty(macAddress))
            {
                this.PrintMACAddressLabel(serialNumber, this.GetMACAddress(serialNumber));
            }

            this.PrintBoxLabel(serialNumber, labelData);
            this.PrintProductLabel(serialNumber, labelData);
        }

        private void PrintProductLabel(int serialNumber, string labelData)
        {
            this.bartenderLabelPack.PrintLabels(
                $"ProductReprint{serialNumber}",
                "ProdLbl1",
                3,
                "c:\\lbl\\prodlbl.btw",
                labelData,
                ref this.message);
        }

        private void PrintBoxLabel(int serialNumber, string labelData)
        {
            this.bartenderLabelPack.PrintLabels(
                $"BoxReprint{serialNumber}",
                "ProdLbl2",
                1,
                "c:\\lbl\\boxlbl_ean.btw",
                labelData,
                ref this.message);
        }

        private string GetMACAddress(int serialNumber)
        {
            var productData = this.productDataRepository.FindById(serialNumber);
            if (productData == null)
            {
                throw new DomainException($"Could not find product data for serial number {serialNumber}");
            }

            return productData.MACAddress;
        }

        private void PrintMACAddressLabel(int serialNumber, string macAddress)
        {
            this.bartenderLabelPack.PrintLabels(
                $"MACReprintm{serialNumber}",
                "ProdLbl1",
                2,
                "c:\\lbl\\prodlblctr.btw",
                macAddress,
                ref this.message);
        }
    }
}