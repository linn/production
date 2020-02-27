namespace Linn.Production.Domain.LinnApps.Reports
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.RemoteServices;

    public class ManufacturingTimingsReportService : IManufacturingTimingsReportService
    {
        private readonly IManufacturingTimingsDatabaseReportService databaseService;
        
        public ManufacturingTimingsReportService(
            IManufacturingTimingsDatabaseReportService databaseService)
        {
            this.databaseService = databaseService;
        }

        public ResultsModel GetTimingsReport(DateTime from, DateTime to, char citCode)
        {
            var allOps = this.databaseService.GetAllOpsDetail(from, to, citCode);

            var metalWorkBuilds = this.databaseService.GetCondensedBuildsDetail(from, to, citCode);

            var partGroups = allOps.Select().GroupBy(r => r[1]).ToList(); //groupby part no

            var buildsList = metalWorkBuilds.Select().ToList();

            var colHeaders = new List<string>
                                 {
                                     "Order Number", "Part Number", "Quantity", "Operation Type", "Resource Code", "Start Time",
                                     "End Time", "Built By", "Minutes Taken", "Days taken", "Expected days (from routes)"
                                 };

            var results = new ResultsModel(colHeaders)
            {
                ReportTitle = new NameModel(
                    $"Manufacturing Timings Report")
            };
            
            var rowIndex = 0;

            foreach (var partGroup in partGroups)
            {
                var partNumber = partGroup.First().ItemArray[1].ToString();
                decimal totalMinsTakenForPart = 0;

                foreach (var entry in partGroup)
                {
                    results.AddRow(rowIndex.ToString());
                    results.SetGridTextValue(rowIndex, 0, entry.ItemArray[0].ToString());             //order no
                    results.SetGridTextValue(rowIndex, 1, entry.ItemArray[1].ToString()); //part no
                    results.SetGridTextValue(rowIndex, 2, entry.ItemArray[2].ToString());              //qty
                    results.SetGridTextValue(rowIndex, 3, entry.ItemArray[3].ToString()); //Operation type
                    results.SetGridTextValue(rowIndex, 4, entry.ItemArray[4].ToString()); //resource code
                    results.SetGridTextValue(rowIndex, 5, entry.ItemArray[5].ToString()); //start
                    results.SetGridTextValue(rowIndex, 6, entry.ItemArray[6].ToString()); //end
                    results.SetGridTextValue(rowIndex, 7, entry.ItemArray[7].ToString()); //built by
                    results.SetGridTextValue(rowIndex, 8, entry.ItemArray[8].ToString());              //mins taken
                    rowIndex++;
                    totalMinsTakenForPart += (decimal)entry.ItemArray[8];
                }

                var build = buildsList.FirstOrDefault(x => x.ItemArray[0].ToString() == partNumber);

                results.AddRow(rowIndex.ToString());
                results.SetGridTextValue(rowIndex, 1, $"{partNumber} Total");
                results.SetGridTextValue(rowIndex, 8, totalMinsTakenForPart.ToString()); //total mins taken

                decimal totalDaysTakenForPart = Math.Round(totalMinsTakenForPart / 410, 2);
                results.SetGridTextValue(rowIndex, 9, totalDaysTakenForPart.ToString()); //% of day(s) taken

                if (build != null)
                {
                    buildsList.Remove(build);
                    results.SetGridTextValue(rowIndex, 10, build.ItemArray[4].ToString()); //Expected days
                }
                else
                {
                    results.SetGridTextValue(rowIndex, 10, "Error"); //Expected days
                }

                rowIndex++;
                results.AddRow(rowIndex.ToString());
                rowIndex++;
            }

            foreach (var remainingBuild in buildsList)
            {
                results.AddRow(rowIndex.ToString());
                results.SetGridTextValue(rowIndex, 1, remainingBuild.ItemArray[0].ToString()); //part no
                results.SetGridTextValue(rowIndex, 10, remainingBuild.ItemArray[4].ToString()); // expected total

                rowIndex++;
            }

            return results;
        }
    }
}
