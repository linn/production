namespace Linn.Production.Domain.LinnApps
{
    public interface ILabelService
    {
        void PrintMACLabel(int serialNumber);

        void PrintAllLabels(int serialNumber, string articleNumber);

        void PrintLabel(string name, string printer, int qty, string template, string data);

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
