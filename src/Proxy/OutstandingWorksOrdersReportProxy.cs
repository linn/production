namespace Linn.Production.Proxy
{
    using System.Data;

    using Domain.LinnApps.RemoteServices;

    public class OutstandingWorksOrdersReportProxy : IOutstandindWorksOrdersReportDatabaseService
    {
        private readonly IDatabaseService databaseService;

        public OutstandingWorksOrdersReportProxy(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public DataTable GetReport()
        {
            var sql = $@"select order_number,wo.part_number,qty_outstanding,
                        date_raised,cit_code,p.description,work_station_code
                        from v_outstanding_works_orders wo,production_trigger_levels ptl,
                        parts p
                        where wo.part_number = ptl.part_number
                        and wo.part_number = p.part_number
                        order by order_number";

            return this.databaseService.ExecuteQuery(sql).Tables[0];
        }
    }
}
