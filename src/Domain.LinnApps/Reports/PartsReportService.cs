﻿namespace Linn.Production.Domain.LinnApps.Reports
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

        private readonly IReportingHelper reportingHelper;

        private readonly ILinnWeekPack linnWeekPack;

        public PartsReportService(
            IQueryRepository<PartFailLog> partFailLogRepository,
            IQueryRepository<EmployeeDepartmentView> employeeDepartmentViewRepository,
            IReportingHelper reportingHelper,
            ILinnWeekPack linnWeekPack)
        {
            this.reportingHelper = reportingHelper;
            this.linnWeekPack = linnWeekPack;
            this.partFailLogRepository = partFailLogRepository;
            this.employeeDepartmentViewRepository = employeeDepartmentViewRepository;
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

            if (supplierId != null)
            {
                fails = fails.Where(f => f.Part.PreferredSupplier == supplierId);
            }

            if (department != "All")
            {
                var employeeDetails =
                    this.employeeDepartmentViewRepository.FilterBy(e => e.DepartmentCode == department);

                var validEmployees = employeeDetails.Select(e => e.UserNumber);
                fails = fails.Where(f => validEmployees.Contains(f.EnteredBy));
            }

            fails = fails.OrderBy(f => f.DateCreated);

            var model = new ResultsModel
                            {
                                ReportTitle = new NameModel(
                                    $"Part Fail - Details for weeks {this.linnWeekPack.Wwsyy(fromDate)} - {this.linnWeekPack.Wwsyy(toDate)}")
                            };

            var columns = this.ModelColumns();

            model.AddSortedColumns(columns);

            var values = this.SetModelRows(fails);

            this.reportingHelper.AddResultsToModel(model, values, CalculationValueModelType.Quantity, true);

            model.ValueDrillDownTemplates.Add(
                new DrillDownModel(
                    "Details",
                    "/production/quality/part-fails/{rowId}",
                    null,
                    model.ColumnIndex("Id")));
            model.ColumnDrillDownTemplates.Add(
                new DrillDownModel(
                    "Details",
                    "/production/quality/part-fails/{rowId}",
                    null,
                    model.ColumnIndex("Id")));

            return model;
        }

        private List<CalculationValueModel> SetModelRows(IQueryable<PartFailLog> fails)
        {
            var values = new List<CalculationValueModel>();

            foreach (var fail in fails)
            {
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = fail.Id.ToString(),
                            TextDisplay = fail.Id.ToString(),
                            ColumnId = "Id",
                        });
                values.Add(
                    new CalculationValueModel
                    {
                        RowId = fail.Id.ToString(),
                        TextDisplay = fail.PartNumber,
                        ColumnId = "Part Number"
                    });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = fail.Id.ToString(),
                            TextDisplay = fail.Part.Description,
                            ColumnId = "Part Description"
                        });
                values.Add(
                    new CalculationValueModel
                    {
                        RowId = fail.Id.ToString(),
                        TextDisplay = fail.DateCreated?.ToString("dd-MMM-yy"),
                        ColumnId = "Date Created"
                    });
                values.Add(
                    new CalculationValueModel
                    {
                        RowId = fail.Id.ToString(),
                        TextDisplay = fail.Batch,
                        ColumnId = "Batch"
                    });
                values.Add(
                    new CalculationValueModel
                    {
                        RowId = fail.Id.ToString(),
                        TextDisplay = fail.FaultCode,
                        ColumnId = "Fault Code"
                    });
                values.Add(
                    new CalculationValueModel
                    {
                        RowId = fail.Id.ToString(),
                        TextDisplay = fail.Story,
                        ColumnId = "Story"
                    });
                values.Add(
                    new CalculationValueModel
                    {
                        RowId = fail.Id.ToString(),
                        Quantity = fail.Quantity ?? 0,
                        ColumnId = "Quantity"
                    });
                values.Add(
                    new CalculationValueModel
                    {
                        RowId = fail.Id.ToString(),
                        Quantity = fail.MinutesWasted ?? 0,
                        ColumnId = "Minutes Wasted"
                    });
                values.Add(
                    new CalculationValueModel
                    {
                        RowId = fail.Id.ToString(),
                        TextDisplay = fail.ErrorType,
                        ColumnId = "Error Type"
                    });
                values.Add(
                    new CalculationValueModel
                    {
                        RowId = fail.Id.ToString(),
                        Quantity = fail.Part.BaseUnitPrice ?? 0,
                        ColumnId = "Base Unit Price"
                    });
                values.Add(
                    new CalculationValueModel
                    {
                        RowId = fail.Id.ToString(),
                        Quantity = fail.Part.BaseUnitPrice * fail.Quantity ?? 0,
                        ColumnId = "Total Price"
                    });
            }

            return values;
        }

        private List<AxisDetailsModel> ModelColumns()
        {
            return new List<AxisDetailsModel>
                       {
                           new AxisDetailsModel("Id") { SortOrder = 0, GridDisplayType = GridDisplayType.Value },

                           new AxisDetailsModel("Part Number")
                               {
                                   SortOrder = 1, GridDisplayType = GridDisplayType.TextValue
                               },
                           new AxisDetailsModel("Part Description")
                               {
                                   SortOrder = 2, GridDisplayType = GridDisplayType.TextValue
                               },
                           new AxisDetailsModel("Date Created")
                               {
                                   SortOrder = 3, GridDisplayType = GridDisplayType.TextValue
                               },
                           new AxisDetailsModel("Batch") { SortOrder = 4, GridDisplayType = GridDisplayType.TextValue },
                           new AxisDetailsModel("Fault Code")
                               {
                                   SortOrder = 5, GridDisplayType = GridDisplayType.TextValue
                               },
                           new AxisDetailsModel("Story") { SortOrder = 6, GridDisplayType = GridDisplayType.TextValue },
                           new AxisDetailsModel("Quantity") { SortOrder = 7, GridDisplayType = GridDisplayType.Value },
                           new AxisDetailsModel("Minutes Wasted")
                               {
                                   SortOrder = 8, GridDisplayType = GridDisplayType.Value
                               },
                           new AxisDetailsModel("Error Type")
                               {
                                   SortOrder = 9, GridDisplayType = GridDisplayType.TextValue
                               },
                           new AxisDetailsModel("Base Unit Price")
                               {
                                   SortOrder = 10, GridDisplayType = GridDisplayType.Value, DecimalPlaces = 2
                               },
                           new AxisDetailsModel("Total Price")
                               {
                                   SortOrder = 11, GridDisplayType = GridDisplayType.Value, DecimalPlaces = 2
                               }
                       };
        }
    }
}
