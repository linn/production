﻿namespace Linn.Production.Domain.LinnApps.Reports
{
    using System.Collections.Generic;

    using Linn.Common.Reporting.Models;

    public interface IProductionMeasuresReportService
    {
        IEnumerable<ResultsModel> FailedPartsReport(string citCode, string partNumber, string orderByDate);

        IEnumerable<ResultsModel> DayRequiredReport(string citCode);
    }
}