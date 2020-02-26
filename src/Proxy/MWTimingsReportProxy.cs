namespace Linn.Production.Proxy
{
    using System;
    using System.Data;
    using System.Globalization;
    using Linn.Production.Domain.LinnApps.RemoteServices;


    public class MWTimingsReportProxy : IMWTimingsDatabaseReportService
    {
        private readonly IDatabaseService db;

        public MWTimingsReportProxy(IDatabaseService db)
        {
            this.db = db;
        }

        public DataTable GetAllOpsDetail(DateTime from, DateTime to)
        {
            var sql = $@"select wo.order_number, wo.part_number, wo.qty, wt.operation_type, wt.resource_code, 
            to_char(wt.start_time, 'HH24:MI DD-MON-RRRR') start_time, to_char(wt.end_time, 'HH24:MI DD-MON-RRRR') end_time,
            au.user_name built_by, round(wt.time_taken / 60) minutes_taken
            from works_orders wo, works_order_timings wt, auth_user_name_view au
            where wo.order_number = wt.order_number and operation_type = 'OPERATION' and wt.built_by = au.user_number (+)
            and ((wt.start_time between to_date('{from.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}', 'dd/mm/yyyy')
            and to_date('{to.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}', 'dd/mm/yyyy') + 0.9999)
            or wt.start_time between to_date('{from.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}', 'dd/mm/yyyy')
            and to_date('{to.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}', 'dd/mm/yyyy') + 0.9999)
            order by wo.part_number, wo.order_number, wt.operation_number, wt.start_time";

            return this.db.ExecuteQuery(sql).Tables[0];
        }

        public DataTable GetCondensedMWBuildsDetail(DateTime from, DateTime to)
        {
            var sql = $@"select a.part_number, decode(ptl.kanban_size, 0, 1, null, 1, kanban_size) kanban_size,
            decode('WEEK', 'MONTH', last_day(TRUNC(bu_date)), 'WEEK', linn_week_pack.linn_week_end_date(bu_date))
            month_end, sum(decode('Days', 'Quantity', QUANTITY, 'Days', QUANTITY, null)) t_adj,                 
            ROUND(lrp_pack.days_to_setup_kanban(a.part_number)
                  * ceil(sum(QUANTITY) / decode(ptl.kanban_size, 0, 1, null, 1, kanban_size))
                  + lrp_pack.days_to_build_kanban(a.part_number)
                  * (sum(QUANTITY) / decode(ptl.kanban_size, 0, 1, null, 1, kanban_size)), 2) sum
                from v_builds a, linn_departments d, production_trigger_levels ptl
            where a.cr_dept = d.department_code
            and bu_date between trunc(to_date('{from.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}', 'dd/mm/yyyy') ) 
            and trunc(to_date('{to.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}', 'dd/mm/yyyy')) +1
            and cr_dept = '0000016502' and a.part_number = ptl.part_number(+)
            group by a.part_number , ptl.kanban_size , 
            decode('WEEK', 'MONTH', last_day(TRUNC(bu_date)), 'WEEK', linn_week_pack.linn_week_end_date(bu_date))";

            return this.db.ExecuteQuery(sql).Tables[0];
        }
    }
}
