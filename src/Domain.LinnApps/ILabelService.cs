namespace Linn.Production.Domain.LinnApps
{
    public interface ILabelService
    {
        void PrintMACLabel(int serialNumber);

        void PrintAllLabels(int serialNumber, string articleNumber);

        LabelReprint CreateLabelReprint(
            string requestedBy,
            string reason,
            string partNumber,
            int serialNumber,
            string documentType,
            int documentNumber,
            string labelTypeCode,
            int numberOfProducts,
            string reprintType,
            string newPartNumber);
    }
}
