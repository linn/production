namespace Linn.Production.Domain.LinnApps
{
    using System.Collections.Generic;

    public interface ILabelPrintService
    {
        void PrintLabel(int serialNumber);


        LabelPrint CreateLabelPrint(
            int requestedByUserNumber,
            string reason,
            string partNumber,
            int? serialNumber,
            int? documentNumber,
            string labelTypeCode,
            int numberOfProducts,
            string reprintType,
            string newPartNumber);

        IEnumerable<IdAndName> GetLabelTypes();

        IEnumerable<IdAndName> GetPrinters();
    }
}