namespace Linn.Production.Domain.LinnApps.RemoteServices
{
    using System;
    using System.Data;

    public interface IManufacturingTimingsDatabaseReportService
    {
        DataTable GetCondensedBuildsDetail(
            DateTime from,
            DateTime to,
            string citCode);

        DataTable GetAllOpsDetail(
            DateTime from,
            DateTime to,
            string citCode);
    }
}
