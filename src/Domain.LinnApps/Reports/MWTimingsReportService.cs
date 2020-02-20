namespace Linn.Production.Domain.LinnApps.Reports
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.RemoteServices;

    public class MWTimingsReportService : IMWTimingsReportService
    {
        private readonly IMWTimingsDatabaseReportService databaseService;

        private readonly IReportingHelper reportingHelper;

        public MWTimingsReportService(
            IMWTimingsDatabaseReportService databaseService, IReportingHelper reportingHelper)
        {
            this.databaseService = databaseService;
            this.reportingHelper = reportingHelper;
        }
        
        public ResultsModel GetTimingsReport(DateTime from, DateTime to)
        {
            var allOps = this.databaseService.GetAllOpsDetail(from, to);

            var mwBuilds = this.databaseService.GetCondensedMWBuildsDetail(from, to);

            var partGroups = allOps.Select().GroupBy(r => r[1]).ToList(); //groupby part no

            //go through part groups, fire all into a list
            //with each matching mwBuild and the sum after each one as a total
            //fire to front end innit
            //also add the blank rows at the bottom

            var tableWithTotal = partGroups
                .Select(
                    g => ((DateTime)g.First().ItemArray[4])).Distinct().OrderBy(w => w).ToList();

            //all from scots report also 
            var colHeaders = new List<string> { "Part Number" };
            colHeaders.AddRange(weeks.Select(w => w.ToShortDateString()));
            colHeaders.Add("Total");

            var results = new ResultsModel(colHeaders)
            {
                ReportTitle = new NameModel(
                    $"Builds {quantityOrValue} for {this.departmentRepository.FindById(department).Description}")
            };

            var rowIndex = 0;

            foreach (var partGroup in partGroups)
            {
                var partTotal = 0m;
                results.AddRow(partGroup.Key.ToString());
                results.SetGridTextValue(rowIndex, 0, partGroup.Key.ToString());

                for (var i = 0; i < weeks.Count; i++)
                {
                    var valueExistsThisWeek = partGroup.FirstOrDefault(g =>
                                          ((DateTime)g.ItemArray[4]).ToShortDateString()
                                          == weeks.ElementAt(i).ToShortDateString()) != null;

                    var val = valueExistsThisWeek
                                  ? ConvertFromDbVal<decimal>(
                                      partGroup.FirstOrDefault(
                                          g => ((DateTime)g.ItemArray[4]).ToShortDateString() == weeks.ElementAt(i).ToShortDateString())
                                          ?.ItemArray[quantityOrValue == "Mins" ? 6 : 5])
                                  : new decimal(0);

                    results.SetColumnType(i + 1, GridDisplayType.Value);
                    results.SetGridValue(rowIndex, i + 1, val, decimalPlaces: 2);

                    if (!valueExistsThisWeek)
                    {
                        continue;
                    }

                    var itemArray = partGroup.First(
                                g => ((DateTime)g.ItemArray[4]).ToShortDateString()
                                     == weeks.ElementAt(i).ToShortDateString())
                            ?.ItemArray;
                    {
                        if (itemArray != null)
                        {
                            partTotal += ConvertFromDbVal<decimal>(itemArray?[quantityOrValue == "Mins" ? 6 : 5]);
                        }
                    }
                }

                results.SetColumnType(weeks.Count, GridDisplayType.Value);

                results.SetGridValue(rowIndex, weeks.Count + 1, partTotal, decimalPlaces: 2);
                rowIndex++;
            }

            return results;
        }
    }
}