namespace Linn.Production.Domain.LinnApps
{
    public interface ILabelPrintingService
    {
        LabelPrintResponse PrintLabel(LabelPrint printDetails);
    }
}
