namespace Linn.Production.Domain.LinnApps.Reports
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.RemoteServices;
    using Linn.Production.Domain.LinnApps.ViewModels;

    public class PartsReportService : IPartsReportService
    {
        private readonly IQueryRepository<EmployeeDepartmentView> employeeDepartmentViewRepository;

        private readonly IRepository<PartFail, int> partFailLogRepository;

        private readonly IReportingHelper reportingHelper;

        private readonly ILinnWeekPack linnWeekPack;

        private readonly IRepository<Supplier, int> supplierRepository;

        private readonly IRepository<PurchaseOrder, int> purchaseOrderRepository;

        public PartsReportService(
            IRepository<PartFail, int> partFailLogRepository,
            IQueryRepository<EmployeeDepartmentView> employeeDepartmentViewRepository,
            IReportingHelper reportingHelper,
            ILinnWeekPack linnWeekPack,
            IRepository<Supplier, int> supplierRepository,
            IRepository<PurchaseOrder, int> purchaseOrderRepository)
        {
            this.partFailLogRepository = partFailLogRepository;
            this.reportingHelper = reportingHelper;
            this.linnWeekPack = linnWeekPack;
            this.supplierRepository = supplierRepository;
            this.purchaseOrderRepository = purchaseOrderRepository;
            this.employeeDepartmentViewRepository = employeeDepartmentViewRepository;
        }

        public ResultsModel PartFailDetailsReport(
            int? supplierId,
            string fromDate,
            string toDate,
            string errorType,
            string faultCode,
            string partNumber,
            string department)
        {
            var fromDateParse = DateTime.Parse(fromDate);
            var toDateParse = DateTime.Parse(toDate);

            var fails = this.partFailLogRepository.FilterBy(p => p.DateCreated >= fromDateParse && p.DateCreated <= toDateParse);

            var purchaseOrders =
                this.purchaseOrderRepository.FilterBy(p => fails.Any(f => f.PurchaseOrderNumber == p.OrderNumber));

            var suppliers =
                this.supplierRepository.FilterBy(s => purchaseOrders.Any(p => p.SupplierId == s.SupplierId));

            if (partNumber != "All")
            {
                fails = fails.Where(f => f.Part.PartNumber == partNumber);
            }

            if (faultCode != "All")
            {
                fails = fails.Where(f => f.FaultCode.FaultCode == faultCode);
            }

            if (errorType != "All")
            {
                fails = fails.Where(f => f.ErrorType.ErrorType == errorType);
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
                fails = fails.Where(f => validEmployees.Contains(f.EnteredBy.Id));
            }

            fails = fails.OrderBy(f => f.DateCreated);

            var model = new ResultsModel
                            {
                                ReportTitle = new NameModel(
                                    $"Part Fail - Details for dates {fromDateParse:dd/MM/yy} - {toDateParse:dd/MM/yy}")
                            };

            var columns = this.ModelColumns();

            model.AddSortedColumns(columns);

            var values = this.SetModelRows(fails, suppliers, purchaseOrders);

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
            model.SetTotalValue(model.ColumnIndex("Base Unit Price"), null);

            return model;
        }

        private List<CalculationValueModel> SetModelRows(IEnumerable<PartFail> fails, IEnumerable<Supplier> suppliers, IEnumerable<PurchaseOrder> purchaseOrders)
        {
            var values = new List<CalculationValueModel>();

            foreach (var fail in fails)
            {
                var purchaseOrder = purchaseOrders.FirstOrDefault(p => p.OrderNumber == fail.PurchaseOrderNumber);

                var supplier = suppliers.FirstOrDefault(s => purchaseOrder != null && s.SupplierId == purchaseOrder.SupplierId);

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
                        TextDisplay = fail.Part.PartNumber,
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
                        TextDisplay = fail.DateCreated.ToString("dd-MMM-yy"),
                        ColumnId = "Date Created"
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
                            TextDisplay = fail.FaultCode.FaultCode,
                            ColumnId = "Fault Code"
                        });
                values.Add(
                    new CalculationValueModel
                    {
                        RowId = fail.Id.ToString(),
                        TextDisplay = fail.ErrorType.ErrorType,
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
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = fail.Id.ToString(),
                            TextDisplay = fail.EnteredBy.FullName,
                            ColumnId = "Entered By"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = fail.Id.ToString(),
                            TextDisplay = fail.Comments,
                            ColumnId = "Comments"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = fail.Id.ToString(),
                            TextDisplay = fail.Owner?.FullName,
                            ColumnId = "Owner"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = fail.Id.ToString(),
                            TextDisplay = supplier?.SupplierName,
                            ColumnId = "Supplier"
                    });
            }

            return values;
        }

        private List<AxisDetailsModel> ModelColumns()
        {
            return new List<AxisDetailsModel>
                       {
                           new AxisDetailsModel("Id") { SortOrder = 0, GridDisplayType = GridDisplayType.TextValue },

                           new AxisDetailsModel("Part Number")
                               {
                                   SortOrder = 1, GridDisplayType = GridDisplayType.TextValue, AllowWrap = false
                               },
                           new AxisDetailsModel("Part Description")
                               {
                                   SortOrder = 2, GridDisplayType = GridDisplayType.TextValue
                               },
                           new AxisDetailsModel("Date Created")
                               {
                                   SortOrder = 3, GridDisplayType = GridDisplayType.TextValue, AllowWrap = false
                               },
                           new AxisDetailsModel("Story") { SortOrder = 4, GridDisplayType = GridDisplayType.TextValue },
                           new AxisDetailsModel("Quantity") { SortOrder = 5, GridDisplayType = GridDisplayType.Value },
                           new AxisDetailsModel("Fault Code") { SortOrder = 6, GridDisplayType = GridDisplayType.TextValue },

                           new AxisDetailsModel("Error Type")
                               {
                                   SortOrder = 7, GridDisplayType = GridDisplayType.TextValue
                               },
                           new AxisDetailsModel("Base Unit Price")
                               {
                                   SortOrder = 8, GridDisplayType = GridDisplayType.Value, DecimalPlaces = 2
                               },
                           new AxisDetailsModel("Total Price")
                               {
                                   SortOrder = 9, GridDisplayType = GridDisplayType.Value, DecimalPlaces = 2
                               },
                           new AxisDetailsModel("Entered By")
                               {
                                   SortOrder = 10, GridDisplayType = GridDisplayType.TextValue
                               },
                           new AxisDetailsModel("Comments")
                               {
                                   SortOrder = 11, GridDisplayType = GridDisplayType.TextValue
                               },
                           new AxisDetailsModel("Owner")
                               {
                                   SortOrder = 12, GridDisplayType = GridDisplayType.TextValue
                               },
                           new AxisDetailsModel("Supplier")
                               {
                                   SortOrder = 13, GridDisplayType = GridDisplayType.TextValue
                               }
                       };
        }
    }
}
