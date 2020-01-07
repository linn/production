namespace Linn.Production.Domain.LinnApps
{
    public interface ILabelService
    {
        void PrintMACLabel(int serialNumber);

        void PrintAllLabels(int serialNumber, string articleNumber);

        LabelReprint CreateLabelReprint(
            int requestedByUserNumber,
            string reason,
            string partNumber,
            int? serialNumber,
            int? documentNumber,
            string labelTypeCode,
            int numberOfProducts,
            string reprintType,
            string newPartNumber);
    }
}
