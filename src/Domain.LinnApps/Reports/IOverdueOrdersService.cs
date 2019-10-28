namespace Linn.Production.Domain.LinnApps.Reports
{
    using Linn.Common.Reporting.Models;

    public interface IOverdueOrdersService
    {
        ResultsModel OverdueOrdersReport(
            int jobId,
            string fromDate,
            string toDate,
            string accountingCompany,
            string stockPool,
            string reportBy,
            string daysMethod);
    }
}
