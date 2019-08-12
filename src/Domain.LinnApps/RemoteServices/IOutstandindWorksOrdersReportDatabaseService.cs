namespace Linn.Production.Domain.LinnApps.RemoteServices
{
    using System.Data;

    public interface IOutstandindWorksOrdersReportDatabaseService
    {
        DataTable GetReport();
    }
}
