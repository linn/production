namespace Linn.Production.Domain.LinnApps.Reports
{
    using Linn.Common.Reporting.Models;

    public interface IOverdueOrdersService
    {
        ResultsModel OverdueOrdersReport(
            string reportBy,
            string daysMethod);
    }
}
