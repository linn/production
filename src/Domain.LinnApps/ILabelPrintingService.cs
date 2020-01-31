namespace Linn.Production.Domain.LinnApps
{
    using System.Collections.Generic;

    public interface ILabelPrintingService
    {
        LabelPrintResponse PrintLabel(LabelPrint printDetails);
    }
}
