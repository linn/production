namespace Linn.Production.Domain.LinnApps.Reports
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.RemoteServices;

    public class BuildsDetailReportService : IBuildsDetailReportService
    {
        private readonly IBuildsDetailReportDatabaseService databaseService;

        private readonly IRepository<Department, string> departmentRepository;

        public BuildsDetailReportService(
            IRepository<Department, string> departmentRepository,
            IBuildsDetailReportDatabaseService databaseService)
        {
            this.databaseService = databaseService;
            this.departmentRepository = departmentRepository;
        }

        public ResultsModel GetBuildsDetailReport(
            DateTime from,
            DateTime to,
            string department,
            string quantityOrValue,
            bool monthly = false)
        {
            var table = this.databaseService.GetBuildsDetail(from, to, quantityOrValue, department, monthly);
            var partGroups = table.Select().GroupBy(r => r[2]).ToList();
            var weeks = partGroups.Select(g => ((DateTime)g.First().ItemArray[4]).ToShortDateString()).Distinct().ToList();

            var colHeaders = new List<string> { "Part Number" };
            colHeaders.AddRange(weeks);
            colHeaders.Add("Total");
            var results = new ResultsModel(colHeaders)
            {
                ReportTitle = new NameModel($"Builds {quantityOrValue} for {this.departmentRepository.FindById(department).Description}")
            };
            var rowIndex = 0;
            foreach (var partGroup in partGroups)
            {
                var partTotal = new decimal();
                results.AddRow(partGroup.Key.ToString());
                results.SetGridTextValue(rowIndex, 0, partGroup.Key.ToString());

                for (var i = 0; i < weeks.Count; i++)
                {
                    var valueExistsThisWeek = partGroup.FirstOrDefault(g =>
                                          ((DateTime)g.ItemArray[4]).ToShortDateString() == weeks.ElementAt(i)) != null;

                    results.SetColumnType(i + 1, GridDisplayType.Value);
                    results.SetGridValue(rowIndex, i + 1, (decimal?)(valueExistsThisWeek ? partGroup.FirstOrDefault(g => ((DateTime)g.ItemArray[4]).ToShortDateString() == weeks.ElementAt(i))?.ItemArray[5] : new decimal(0)));

                    if (valueExistsThisWeek)
                    {
                        partTotal += (decimal)partGroup.First(g => ((DateTime)g.ItemArray[4]).ToShortDateString() == weeks.ElementAt(i))?.ItemArray[5];
                    }
                }

                results.SetColumnType(weeks.Count, GridDisplayType.Value);
                results.SetGridValue(rowIndex, weeks.Count + 1, partTotal);
                rowIndex++;
            }

            return results;
        }
    }
}