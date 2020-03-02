namespace Linn.Production.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;

    public interface IManufacturingTimingsFacadeService
    {
        IResult<ResultsModel> GetManufacturingTimingsReport(
            DateTime startDate,
            DateTime endDate,
            string citCode);

        IResult<IEnumerable<IEnumerable<string>>> GetManufacturingTimingsExport(
            DateTime startDate,
            DateTime endDate,
            string citCode);
    }
}
