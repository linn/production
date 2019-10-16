namespace Linn.Production.Domain.LinnApps.Reports
{
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Models;

    public interface IOrdersReports
    {
        ManufacturingCommitDateResults ManufacturingCommitDate(string date);

        ResultsModel OverdueOrdersReport(
            string fromDate,
            string toDate,
            string accountingCompany,
            string stockPool,
            string reportBy,
            string daysMethod);
    }
}
