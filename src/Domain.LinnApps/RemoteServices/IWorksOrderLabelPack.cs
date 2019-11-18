namespace Linn.Production.Domain.LinnApps.RemoteServices
{
    public interface IWorksOrderLabelPack
    {
        void PrintLabels(int orderNumber, string printerGroup);

        void PrintAioLabels(int orderNumber);
    }
}