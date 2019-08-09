namespace Linn.Production.Domain.LinnApps.RemoteServices
{
    using System.Data;

    using Linn.Production.Resources;

    public interface IOutstandingWorksOrdersReportDatabaseService
    {
        DataTable GetReport(OutstandingWorksOrdersRequestResource options);
    }
}
