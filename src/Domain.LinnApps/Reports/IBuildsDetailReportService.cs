namespace Linn.Production.Domain.LinnApps.Services
{
    using System;
    using System.Collections.Generic;

    using Common.Reporting.Models;

    public interface IBuildsDetailReportService
    {
        ResultsModel GetBuildsDetailReport(DateTime from, DateTime to, 
            string department, string quantityOrValue, bool monthly = false);
    }
}