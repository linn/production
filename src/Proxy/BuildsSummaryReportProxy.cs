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

        public IEnumerable<BuildsSummary> GetBuildsSummaries(DateTime from, DateTime to, bool monthly, string partNumbers)
        {
            var dateSelect = monthly
                ? "last_day(trunc(a.bu_date)),"
                : "linn_week_pack.linn_week_end_date(trunc(bu_date)) week_end";

            var groupByClause = monthly 
                ? "last_day(TRUNC(bu_date))"
                : "linn_week_pack.linn_week_end_date(trunc(bu_date)) ";

            var partNumbersClause = string.Empty;

            if (!string.IsNullOrEmpty(partNumbers))
            {
                partNumbersClause = "and a.part_number in (";
                var parts = partNumbers.Trim().Split(",").Where(x => !string.IsNullOrEmpty(x)).Select(str => $"'{str.Trim().ToUpper()}'");
                for (int i = 0; i < parts.Count(); i++)
                {
                    partNumbersClause += parts.ElementAt(i);
                    partNumbersClause += i == parts.Count() - 1 ? ")" : ",";
                }
            }

            var sql = $@"select d.description,
                        a.cr_dept dept,
                        {dateSelect},
                        round(sum (material_price + labour_price), 10) t_adj,
                        round(lrp_pack.days_to_build_part(a.part_number,sum(a.QUANTITY ) ), 10) mins
                        from v_builds a,linn_departments d
                        where a.cr_dept=d.department_code
                        and bu_date between trunc(to_date('{from.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}', 'dd/mm/yyyy')) 
                        and trunc(to_date('{to.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}', 'dd/mm/yyyy')) + 1
                        {partNumbersClause}
                        group by a.cr_dept,a.part_number, 
                        d.description, {groupByClause}
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
                    DaysToBuild = (decimal)tableRow[4]
                });
            }

            var r = results.GroupBy(x => new
                {
                    x.DepartmentCode, x.WeekEnd, x.DepartmentDescription
                }).Select(g => new BuildsSummary
                                   {
                                       DepartmentCode = g.Key.DepartmentCode,
                                       DepartmentDescription = g.Key.DepartmentDescription,
                                       Value = g.Sum(x => x.Value),
                                       DaysToBuild = Math.Round((decimal)g.Sum(x => x.DaysToBuild), 1),
                                       WeekEnd = g.Key.WeekEnd
                                   }).OrderBy(s => s.WeekEnd);

            return r;
        }
    }
}