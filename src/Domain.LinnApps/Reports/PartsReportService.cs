namespace Linn.Production.Domain.LinnApps.Reports
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Domain.LinnApps.ViewModels;

    public class PartsReportService : IPartsReportService
    {
        private readonly IQueryRepository<PartFailLog> partFailLogRepository;

        private readonly IQueryRepository<EmployeeDepartmentView> employeeDepartmentViewRepository;

        private readonly IRepository<Part, string> partRepository;

        private readonly IReportingHelper reportingHelper;

        private readonly ILinnWeekPack linnWeekPack;

        public PartsReportService(
            IQueryRepository<PartFailLog> partFailLogRepository,
            IQueryRepository<EmployeeDepartmentView> employeeDepartmentViewRepository,
            IRepository<Part, string> partRepository,
            IReportingHelper reportingHelper,
            ILinnWeekPack linnWeekPack)
        {
            this.reportingHelper = reportingHelper;
            this.linnWeekPack = linnWeekPack;
            this.partFailLogRepository = partFailLogRepository;
            this.employeeDepartmentViewRepository = employeeDepartmentViewRepository;
            this.partRepository = partRepository;
        }

        public ResultsModel PartFailDetailsReport(
            int? supplierId,
            string fromWeek,
            string toWeek,
            string errorType,
            string faultCode,
            string partNumber,
            string department)
        {
            var fromDate = DateTime.Parse(fromWeek);
            var toDate = DateTime.Parse(toWeek);

            var fails = this.partFailLogRepository.FilterBy(p => p.DateCreated > fromDate && p.DateCreated < toDate);

            if (partNumber != "All")
            {
                fails = fails.Where(f => f.PartNumber == partNumber);
            }

            if (faultCode != "All")
            {
                fails = fails.Where(f => f.FaultCode == faultCode);
            }

            if (errorType != "All")
            {
                fails = fails.Where(f => f.ErrorType == errorType);
            }

            var partNumbers = fails.Select(f => f.PartNumber).ToList();

            var parts = this.partRepository.FilterBy(p => partNumbers.Contains(p.PartNumber));

            if (supplierId != null)
            {
                parts = parts.Where(p => p.PreferredSupplier == supplierId);
                var validPartNumbers = parts.Select(p => p.PartNumber).ToList();
                fails = fails.Where(f => validPartNumbers.Contains(f.PartNumber));
            }

            if (department != "All")
            {
                var employeeDetails =
                    this.employeeDepartmentViewRepository.FilterBy(e => e.DepartmentCode == department);

                var validEmployees = employeeDetails.Select(e => e.UserNumber);
                fails = fails.Where(f => validEmployees.Contains(f.EnteredBy));
            }

            fails = fails.OrderBy(f => f.PartNumber);

            var model = new ResultsModel { ReportTitle = new NameModel("Part Fail - Detail") };

            var columns = this.ModelColumns();

            model.AddSortedColumns(columns);

            var values = this.SetModelRows(fails, parts);

            this.reportingHelper.AddResultsToModel(model, values, CalculationValueModelType.Quantity, true);

            return model;
        }

        private List<CalculationValueModel> SetModelRows(IQueryable<PartFailLog> fails, IQueryable<Part> parts)
        {
            var values = new List<CalculationValueModel>();

            var rowId = 0;

            foreach (var fail in fails)
            {
                var newRowId = rowId++;

                var part = parts.FirstOrDefault(p => p.PartNumber == fail.PartNumber);

                var totalPrice = part?.BaseUnitPrice * fail.Quantity;

                values.Add(
                    new CalculationValueModel
                    {
                        RowId = newRowId.ToString(),
                        TextDisplay = fail.PartNumber,
                        ColumnId = "Part Number"
                    });
                values.Add(
                    new CalculationValueModel
                    {
                        RowId = newRowId.ToString(),
                        TextDisplay = fail.DateCreated?.ToString("dd-MMM-yy"),
                        ColumnId = "Date Created"
                    });
                values.Add(
                    new CalculationValueModel
                    {
                        RowId = newRowId.ToString(),
                        TextDisplay = fail.Batch,
                        ColumnId = "Batch"
                    });
                values.Add(
                    new CalculationValueModel
                    {
                        RowId = newRowId.ToString(),
                        TextDisplay = fail.FaultCode,
                        ColumnId = "Fault Code"
                    });
                values.Add(
                    new CalculationValueModel
                    {
                        RowId = newRowId.ToString(),
                        TextDisplay = fail.Story,
                        ColumnId = "Story"
                    });
                values.Add(
                    new CalculationValueModel
                    {
                        RowId = newRowId.ToString(),
                        Quantity = fail.Quantity ?? 0,
                        ColumnId = "Quantity"
                    });
                values.Add(
                    new CalculationValueModel
                    {
                        RowId = newRowId.ToString(),
                        Quantity = fail.MinutesWasted ?? 0,
                        ColumnId = "Minutes Wasted"
                    });
                values.Add(
                    new CalculationValueModel
                    {
                        RowId = newRowId.ToString(),
                        TextDisplay = fail.ErrorType,
                        ColumnId = "Error Type"
                    });
                values.Add(
                    new CalculationValueModel
                    {
                        RowId = newRowId.ToString(),
                        Quantity = part?.BaseUnitPrice ?? 0,
                        ColumnId = "Base Unit Price"
                    });
                values.Add(
                    new CalculationValueModel
                    {
                        RowId = newRowId.ToString(),
                        Quantity = totalPrice ?? 0,
                        ColumnId = "Total Price"
                    });
                values.Add(
                    new CalculationValueModel
                    {
                        RowId = newRowId.ToString(),
                        Quantity = fail.Id,
                        ColumnId = "Id"
                    });
            }

            return values;
        }

        private List<AxisDetailsModel> ModelColumns()
        {
            return new List<AxisDetailsModel>
                       {
                           new AxisDetailsModel("Part Number")
                               {
                                   SortOrder = 0, GridDisplayType = GridDisplayType.TextValue
                               },
                           new AxisDetailsModel("Date Created")
                               {
                                   SortOrder = 1, GridDisplayType = GridDisplayType.TextValue
                               },
                           new AxisDetailsModel("Batch") { SortOrder = 2, GridDisplayType = GridDisplayType.TextValue },
                           new AxisDetailsModel("Fault Code")
                               {
                                   SortOrder = 3, GridDisplayType = GridDisplayType.TextValue
                               },
                           new AxisDetailsModel("Story") { SortOrder = 4, GridDisplayType = GridDisplayType.TextValue },
                           new AxisDetailsModel("Quantity") { SortOrder = 5, GridDisplayType = GridDisplayType.Value },
                           new AxisDetailsModel("Minutes Wasted")
                               {
                                   SortOrder = 6, GridDisplayType = GridDisplayType.Value
                               },
                           new AxisDetailsModel("Error Type")
                               {
                                   SortOrder = 7, GridDisplayType = GridDisplayType.TextValue
                               },
                           new AxisDetailsModel("Base Unit Price")
                               {
                                   SortOrder = 8, GridDisplayType = GridDisplayType.Value
                               },
                           new AxisDetailsModel("Total Price")
                               {
                                   SortOrder = 9, GridDisplayType = GridDisplayType.Value
                               },
                           new AxisDetailsModel("Id") { SortOrder = 10, GridDisplayType = GridDisplayType.Value }
                       };
        }
    }
}
