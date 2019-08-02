namespace Linn.Production.Domain.LinnApps.Reports
{
    using System.Collections.Generic;

    using Linn.Common.Reporting.Models;

    public interface IWhoBuiltWhatReport
    {
        IEnumerable<ResultsModel> WhoBuiltWhat(string fromDate, string toDate, string citCode);
    }
}
