namespace Linn.Production.Domain.LinnApps.Models
{
    using System;
    using Linn.Common.Reporting.Models;

    public class ShortageResult
    {
        public ShortageResult()
        {
            this.Results = new ResultsModel();
            this.Results.AddColumn("shortPartNumber", "Short Part Number");
            this.Results.AddColumn("description", "Description");
            this.Results.AddColumn("category", "Cat");
            this.Results.AddColumn("reqt", "Reqt");
            this.Results.AddColumn("stock", "Stock");
            this.Results.AddColumn("avail", "Avail");
            this.Results.AddColumn("res", "Res");
            this.Results.AddColumn("canBuild", "Can Build");
            this.Results.AddColumn("notes", "Notes");
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

            if (!string.IsNullOrEmpty(shortage.CrfStory))
            {
                this.AddToNotes(shortage.ShortPartNumber, shortage.CrfStory);
            }
        }

        public void AddWswShortageStory(WswShortageStory story)
        {
            this.AddToNotes(story.ShortPartNumber, story.Story);
        }

        public void AddToNotes(string shortPartNumber, string note)
        {
            var rowIndex = this.Results.RowIndex(shortPartNumber);
            var notes = this.Results.GetGridTextValue(rowIndex,
                this.Results.ColumnIndex("notes"));
            if (string.IsNullOrEmpty(notes))
            {
                notes = note;
            }
            else
            {
                notes += "   " + note;
            }
            this.Results.SetGridTextValue(rowIndex, this.Results.ColumnIndex("notes"), notes);
        }
    }
}