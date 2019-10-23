﻿namespace Linn.Production.Facade.Services
{
    using System;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Models;

    public interface IOrdersReportsFacadeService
    {
        IResult<ManufacturingCommitDateResults> ManufacturingCommitDateReport(string date);

        IResult<ResultsModel> GetOverdueOrdersReport(
            int jobId,
            string fromDate,
            string toDate,
            string accountingCompany,
            string stockPool,
            string reportBy,
            string daysMethod);
    }
}