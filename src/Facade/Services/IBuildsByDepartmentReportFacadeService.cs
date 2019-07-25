namespace Linn.Production.Facade.Services
{
    using System;
    using System.Collections.Generic;

    using Domain.LinnApps;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;

    public interface IBuildsByDepartmentReportFacadeService
    {
        IResult<IEnumerable<ResultsModel>> GetBuildsSummaryReports(DateTime fromWeek, DateTime toWeek, bool monthly = false);
    }
}