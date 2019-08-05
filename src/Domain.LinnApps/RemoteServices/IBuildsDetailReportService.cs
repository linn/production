using System;
using System.Collections.Generic;
using System.Data;

namespace Linn.Production.Domain.LinnApps.Reports
{
    public interface IBuildsDetailReportDatabaseService
    {
        DataTable GetBuildsDetail(DateTime from, DateTime to, string quantityOrValue,
            string department, bool monthly = false);
    }
}