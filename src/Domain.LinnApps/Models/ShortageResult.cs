namespace Linn.Production.Domain.LinnApps.Models
{
    using System;
    using Linn.Common.Reporting.Models;

    public class ShortageResult
    {
        public ShortageResult()
        {
            var model = new ResultsModel();
            model.AddColumn("shortPartNumber", "Short Part Number");
            model.AddColumn("description", "Description");
            model.AddColumn("category", "Cat");
            model.AddColumn("reqt", "Reqt");
            model.AddColumn("stock", "Stock");
            model.AddColumn("avail", "Avail");
            model.AddColumn("res", "Res");
            model.AddColumn("canBuild", "Can Build");
            model.AddColumn("notes", "Notes");
            this.Results = model;
        }

        public string PartNumber { get; set; }

        public string Priority { get; set; }

        public int? Build { get; set; }

        public int? CanBuild { get; set; }

        public decimal? BackOrderQty { get; set; }

        public int Kanban { get; set; }

        public DateTime? EarliestRequestedDate { get; set; }

        public ResultsModel Results { get; private set; }

        public bool BoardShortage { get; set; } = false;

        public bool MetalworkShortage { get; set; } = false;

        public bool ProcurementShortage { get; set; } = false;

        public void AddWswShortage(WswShortage shortage)
        {
            var row = this.Results.AddRow(shortage.ShortPartNumber);
            this.Results.SetGridTextValue(row.RowIndex, this.Results.ColumnIndex("shortPartNumber"), shortage.ShortPartNumber);
            this.Results.SetGridTextValue(row.RowIndex, this.Results.ColumnIndex("description"), shortage.ShortPartDescription);
            this.Results.SetGridTextValue(row.RowIndex, this.Results.ColumnIndex("category"), shortage.ShortageCategory);
            this.Results.SetGridTextValue(row.RowIndex, this.Results.ColumnIndex("reqt"), shortage.Required.ToString());
            this.Results.SetGridTextValue(row.RowIndex, this.Results.ColumnIndex("stock"), shortage.Stock.ToString());
            this.Results.SetGridTextValue(row.RowIndex, this.Results.ColumnIndex("avail"), shortage.AdjustedAvailable.ToString());
            this.Results.SetGridTextValue(row.RowIndex, this.Results.ColumnIndex("res"), shortage.QtyReserved.ToString());
            this.Results.SetGridTextValue(row.RowIndex, this.Results.ColumnIndex("canBuild"), shortage.CanBuild.ToString());

            if (shortage.IsBoardShortage())
            {
                this.BoardShortage = true;
            }
            else if (shortage.IsMetalworkShortage())
            {
                this.MetalworkShortage = true;
            }
            else if (shortage.IsProcurementShortage())
            {
                this.ProcurementShortage = true;
            }
        }
    }
}