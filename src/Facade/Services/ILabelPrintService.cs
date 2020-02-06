namespace Linn.Production.Facade
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public interface ILabelPrintService
    {
        IResult<LabelPrintResponse> PrintLabel(LabelPrintResource resource);

        IResult<IEnumerable<IdAndName>> GetLabelTypes();

        IResult<IEnumerable<IdAndName>> GetPrinters();
    }
}