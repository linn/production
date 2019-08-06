﻿namespace Linn.Production.Proxy
{
    using System;
    using System.Data;
    using System.Globalization;

    using Linn.Production.Domain.LinnApps.RemoteServices;

    public class BuildsDetailReportProxy : IBuildsDetailReportDatabaseService
    {
        private readonly IDatabaseService db;

        public BuildsDetailReportProxy(IDatabaseService db)
        {
            this.db = db;
        }

        public DataTable GetBuildsDetail(
            DateTime from,
            DateTime to,
            string quantityOrValue,
            string department,
            bool monthly = false)
        {
            var totalBy = monthly ? "MONTH" : "WEEK";
            var sql =
                $@"select a.cr_dept dept, d.description, a.part_number, decode(ptl.kanban_size, 0, 1, null, 1, kanban_size) kanban_size, 
            decode('{totalBy}', 'MONTH', last_day(TRUNC(bu_date)), 'WEEK', linn_week_pack.linn_week_end_date(bu_date))
            month_end, sum(decode('{quantityOrValue}', 'Value', round(material_price + labour_price, 0),
                'Quantity', QUANTITY, 'Days', QUANTITY, null)) t_adj from v_builds a, linn_departments d, production_trigger_levels ptl
            where a.cr_dept = d.department_code
            and bu_date between trunc(to_date('{from.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}', 'dd/mm/yyyy') ) 
and trunc(to_date('{to.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}', 'dd/mm/yyyy')) +1
            and cr_dept = '{department}' and a.part_number = ptl.part_number(+)
            group by a.cr_dept , d.description , a.part_number , ptl.kanban_size , 
            decode('{totalBy}', 'MONTH', last_day(TRUNC(bu_date)), 'WEEK', linn_week_pack.linn_week_end_date(bu_date))
            ORDER BY 1 ASC,2 ASC,4 ASC,5 ASC,3 ASC";

            return this.db.ExecuteQuery(sql).Tables[0];
        }
    }
}