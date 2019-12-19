namespace Linn.Production.Facade
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;

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

        IResult<IEnumerable<IdAndName>> GetLabelTypes();

        IResult<IEnumerable<IdAndName>> GetPrinters();
    }
}