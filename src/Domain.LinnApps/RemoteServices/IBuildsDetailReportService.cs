namespace Linn.Production.Domain.LinnApps.RemoteServices
{
    using System;
    using System.Data;

    public interface IBuildsDetailReportDatabaseService
    {
        DataTable GetBuildsDetail(
            DateTime from,
            DateTime to,
            string quantityOrValue,
            string department,
            bool monthly = false,
            string partNumbers = null);
    }
}