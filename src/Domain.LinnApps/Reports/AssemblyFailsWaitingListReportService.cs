namespace Linn.Production.Domain.LinnApps.Reports
{
    using System.Globalization;
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.RemoteServices;

    public class AssemblyFailsWaitingListReportService : IAssemblyFailsWaitingListReportService
    {
        private readonly IRepository<AssemblyFail, int> assemblyFailsRepository;

        private readonly ILinnWeekPack weekPack;

        public AssemblyFailsWaitingListReportService(ILinnWeekPack weekPack, IRepository<AssemblyFail, int> assemblyFailRepository)
        {
            this.assemblyFailsRepository = assemblyFailRepository;
            this.weekPack = weekPack;
        }

        public ResultsModel GetAssemblyFailsWaitingListReport()
        {
            var assemblyFails = this.assemblyFailsRepository
                .FindAll().Where(f => f.CompletedBy == null && f.DateInvalid == null)
                .OrderBy(f => f.Id);

            var results =
                new ResultsModel(
                    new[]
                        {
                            "Id",
                            "Week",
                            "When Found",
                            "Part Number",
                            "Serial",
                            "Reported Fault",
                            "In Slot"
                        })
                    {
                        RowHeader = "Id",
                        ReportTitle = new NameModel("Assembly Fail Waiting List")
                    };

            foreach (AssemblyFail fail in assemblyFails.ToList())
            {
                var row = results.AddRow(fail.Id.ToString());

                results.SetGridTextValue(row.RowIndex, 0, fail.Id.ToString());
                results.SetGridTextValue(row.RowIndex, 1, this.weekPack.Wwsyy(fail.DateTimeFound));
                results.SetGridTextValue(row.RowIndex, 2, fail.DateTimeFound.ToString("d", new CultureInfo("en-GB")));
                results.SetGridTextValue(row.RowIndex, 3, fail.WorksOrder.PartNumber);
                results.SetGridTextValue(row.RowIndex, 4, fail.SerialNumber.ToString());
                results.SetGridTextValue(row.RowIndex, 5, fail.ReportedFault);
                results.SetGridTextValue(row.RowIndex, 6, fail.InSlot);
            }

            return results;
        }
    }
}
