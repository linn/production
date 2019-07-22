namespace Linn.Production.Facade.Services
{
    using System;
    using System.Collections.Generic;

    using Domain.LinnApps;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;

    public interface IBuildsByDepartmentReportFacadeService
    {
        IResult<ResultsModel> GetBuildsSummary(DateTime fromWeek, DateTime toWeek);
    }
}