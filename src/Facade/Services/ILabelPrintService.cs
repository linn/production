namespace Linn.Production.Facade
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;

    public interface ILabelPrintService
    {
        void PrintLabel();

        IResult<IEnumerable<IdAndName>> GetLabelTypes();

        IResult<IEnumerable<IdAndName>> GetPrinters();
    }
}