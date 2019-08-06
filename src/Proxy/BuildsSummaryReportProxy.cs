namespace Linn.Production.Proxy
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.Linq;

    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.RemoteServices;

    public class BuildsSummaryReportProxy : IBuildsSummaryReportDatabaseService
    {
        private readonly IDatabaseService db;

        public BuildsSummaryReportProxy(IDatabaseService db)
        {
            this.db = db;
        }

        public IEnumerable<BuildsSummary> GetBuildsSummaries(DateTime from, DateTime to, bool monthly)
        {
            var dateSelect = monthly
                ? "last_day(trunc(a.bu_date)),"
                : "linn_week_pack.linn_week_end_date(trunc(bu_date)) week_end,";

            var groupByClause = monthly 
                ? "last_day(TRUNC(bu_date))"
                : "linn_week_pack.linn_week_end_date(trunc(bu_date)) ";

            var sql = $@"select 
                        d.description,
                        a.CR_DEPT,
                        {dateSelect}
                        round(sum (material_price + labour_price ),1) value,
                        round(sum(lrp_pack.days_to_build_part(a.part_number,a.QUANTITY )), 1) mins 
                        from v_builds a,linn_departments d
                        where a.cr_dept = d.DEPARTMENT_CODE
                        and bu_date between trunc(to_date('{from.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}', 'dd/mm/yyyy')) 
                        and trunc(to_date('{to.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}', 'dd/mm/yyyy')) + 1
                        group by a.cr_dept,
                        d.description,
                        {groupByClause}
                        order by 2 asc";

            var table = this.db.ExecuteQuery(sql).Tables[0];
            var results = new List<BuildsSummary>();
            foreach (DataRow tableRow in table.Rows)
            {
                results.Add(new BuildsSummary
                {
                    DepartmentDescription = tableRow[0].ToString(),
                    DepartmentCode = tableRow[1].ToString(),
                    WeekEnd = ((DateTime)tableRow[2]).Date,
                    Value = (decimal)tableRow[3],
                    DaysToBuild = (decimal)tableRow[4],
                });
            }

            return results.OrderBy(s => s.WeekEnd);
        }
    }
}