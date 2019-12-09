namespace Linn.Production.Domain.LinnApps.Layouts
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Reporting.Layouts.BaseDataComponents;
    using Linn.Common.Reporting.Layouts.BaseLayouts;
    using Linn.Common.Reporting.Layouts.Exceptions;
    using Linn.Common.Reporting.Models;

    public class ValuesByWeekLayout : SingleModelLayout
    {
        private readonly IReportingHelper reportingHelper;

        private readonly DataComponent<AxisDetailsModel> weeksComponent;

        private readonly DataComponent<CalculationValueModel> dataComponent;

        private readonly IEnumerable<string> rowIds;

        private readonly bool zeroPad;

        public ValuesByWeekLayout(
            IReportingHelper reportingHelper,
            string reportTitle,
            IEnumerable<string> rowIds = null,
            bool zeroPad = true)
        {
            this.reportingHelper = reportingHelper;
            this.zeroPad = zeroPad;
            this.rowIds = rowIds;
            this.ReportTitle = reportTitle;
            this.weeksComponent = this.AddComponent<AxisDetailsModel>(
                "Weeks",
                "Weeks displayed and total column",
                false,
                DataComponentType.Column);

            this.dataComponent = this.AddComponent<CalculationValueModel>(
                "Data",
                "Quantities by week",
                false,
                DataComponentType.Grid);
        }

        public void AddWeeks(IEnumerable<AxisDetailsModel> values)
        {
            this.weeksComponent.SetAxisDetails(values.ToList());
            this.weeksComponent.SetDataAsSupplied();
        }

        public void AddData(IEnumerable<CalculationValueModel> data)
        {
            this.dataComponent.SetData(data.ToList());
        }

        protected override void CalculateResults()
        {
            this.Model = new ResultsModel
                             {
                                 ResultDisplayedAs = ReportResultType.Quantity
                             };
            this.AddReportColumns();

            if (this.rowIds != null)
            {
                foreach (var row in this.rowIds)
                {
                    this.Model.AddRow(row);
                }
            }

            this.CalculateQuantities();

            if (this.zeroPad)
            {
                this.reportingHelper.ZeroPad(this.Model);
            }

            this.reportingHelper.SetTotalColumn(
                this.Model,
                0,
                this.Model.ColumnIndex("Total") - 1,
                this.Model.ColumnIndex("Total"));
        }

        private void AddReportColumns()
        {
            if (!this.weeksComponent.AxisDetailsSupplied)
            {
                throw new ReportingLayoutException("Weeks have not been supplied to layout");
            }

            this.Model.AddSortedColumns(this.weeksComponent.AxisDetails);
            this.Model.AddColumn("Total");
        }

        private void CalculateQuantities()
        {
            var acceptNewRows = this.rowIds == null;

            this.reportingHelper.AddResultsToModel(this.Model, this.dataComponent.Data, CalculationValueModelType.Quantity, acceptNewRows);
        }
    }
}
