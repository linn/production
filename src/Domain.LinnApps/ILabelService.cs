namespace Linn.Production.Domain.LinnApps
{
    public interface ILabelService
    {
        void PrintMACLabel(int serialNumber);

        void PrintAllLabels(int serialNumber, string articleNumber);
    }
}
