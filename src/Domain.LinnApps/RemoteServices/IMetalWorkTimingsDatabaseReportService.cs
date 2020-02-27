namespace Linn.Production.Domain.LinnApps.RemoteServices
{
    using System;
    using System.Data;

    public interface IMetalWorkTimingsDatabaseReportService
    {
        DataTable GetCondensedMWBuildsDetail(
            DateTime from,
            DateTime to);

        DataTable GetAllOpsDetail(
            DateTime from,
            DateTime to);
    }
}
