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
            bool monthly = false,
            string partNumbers = null)
        {
            var table = this.databaseService.GetBuildsDetail(from, to, quantityOrValue, department, monthly, partNumbers);
            var partGroups = table.Select().GroupBy(r => r[2]).ToList();
            var weeks = partGroups.SelectMany(x => x.Select(y => (DateTime)y.ItemArray[4]))
                .Distinct().OrderBy(date => date).ToList();

            var colHeaders = new List<string> { "Part Number", "Product" };
            colHeaders.AddRange(weeks.Select(w => w.ToString("dd-MMM-yyyy")));
            colHeaders.Add("Total");

            var results = new ResultsModel(colHeaders)
            {
                ReportTitle = new NameModel(
                    $"Builds {quantityOrValue} for {this.departmentRepository.FindById(department).Description}")
            };

            results.SetColumnType(1, GridDisplayType.TextValue);
            var rowIndex = 0;

            foreach (var partGroup in partGroups)
            {
                var partTotal = 0m;
                results.AddRow(partGroup.Key.ToString());
                results.SetGridTextValue(rowIndex, 0, partGroup.Key.ToString());
                results.SetGridTextValue(rowIndex, 1, partGroup.First().ItemArray[6].ToString());

                for (var i = 0; i < weeks.Count; i++)
                {
                    var valueExistsThisWeek = partGroup.FirstOrDefault(g =>
                                          ((DateTime)g.ItemArray[4]).ToString("dd-MMM-yyyy")
                                          == weeks.ElementAt(i).ToString("dd-MMM-yyyy")) != null;

                    var val = valueExistsThisWeek
                                  ? ConvertFromDbVal<decimal>(
                                      partGroup.FirstOrDefault(
                                          g => ((DateTime)g.ItemArray[4]).ToString("dd-MMM-yyyy") == weeks.ElementAt(i).ToString("dd-MMM-yyyy"))
                                          ?.ItemArray[quantityOrValue == "Mins" ? 7 : 5])
                                  : new decimal(0);

                    results.SetColumnType(i + 2, GridDisplayType.Value);
                    results.SetGridValue(rowIndex, i + 2, val, decimalPlaces: 2);

                    if (!valueExistsThisWeek)
                    {
                        continue;
                    }

                    var itemArray = partGroup.First(
                                g => ((DateTime)g.ItemArray[4]).ToString("dd-MMM-yyyy")
                                     == weeks.ElementAt(i).ToString("dd-MMM-yyyy"))
                            ?.ItemArray;
                        {
                            if (itemArray != null)
                            {
                                partTotal += ConvertFromDbVal<decimal>(itemArray?[quantityOrValue == "Mins" ? 7 : 5]);
                            }
                        }
                }

                results.SetColumnType(weeks.Count + 2, GridDisplayType.Value);

                results.SetGridValue(rowIndex, weeks.Count + 2, partTotal, decimalPlaces: 2);
                rowIndex++;
            }

            return results;
        }

        private static T ConvertFromDbVal<T>(object obj)
        {
            return obj == null || obj == DBNull.Value ? default(T) : (T)obj;
        }
    }
}