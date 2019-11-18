namespace Linn.Production.Domain.LinnApps.Reports
{
    using System.Collections.Generic;

    using Linn.Common.Reporting.Models;

    public interface IProductionBackOrdersReportService
    {
        IEnumerable<ResultsModel> ProductionBackOrders(string citCode);
    }
}
