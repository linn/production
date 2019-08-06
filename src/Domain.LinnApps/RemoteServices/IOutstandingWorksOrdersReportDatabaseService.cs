namespace Linn.Production.Domain.LinnApps.RemoteServices
{
    using System.Data;

    public interface IOutstandingWorksOrdersReportDatabaseService
    {
        DataTable GetReport();
    }
}
