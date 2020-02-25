namespace Linn.Production.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Linn.Production.Resources;

    public interface IMetalWorkTimingsFacadeService
    {
        IResult<ResultsModel> GetMetalWorkTimingsReport(
            DateTime startDate,
            DateTime endDate);

        IResult<IEnumerable<IEnumerable<string>>> GetMetalWorkTimingsExport(
            DateTime startDate,
            DateTime endDate);
    }
}
