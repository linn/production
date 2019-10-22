namespace Linn.Production.Domain.LinnApps.RemoteServices
{
    public interface IBartenderLabelPack
    {
        string ReturnPackageMessage();

        bool PrintLabels(string fileName, string printer, int qty, string template, string data, ref string message);

        void WorksOrderLabels(int orderNumber);
    }
}
