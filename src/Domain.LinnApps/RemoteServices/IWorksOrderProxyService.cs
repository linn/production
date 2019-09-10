namespace Linn.Production.Domain.LinnApps.RemoteServices
{
    public interface IWorksOrderProxyService
    {
        string CanRaiseWorksOrder(string partNumber);

        int GetNextBatch(string partNumber);

        string GetDepartment(string partNumber, string raisedByDepartment);

        bool ProductIdOnChip(string partNumber);

        string GetAuditDisclaimer();

        string CanBuildAtWorkStation(string workStationCode);
    }
}
