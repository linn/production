namespace Linn.Production.Domain.LinnApps.ATE
{
    using System;
    using System.Collections.Generic;

    public class AteTest
    {
        public int TestId { get; set; }

        public int UserNumber { get; set; }

        public DateTime? DateTested { get; set; }

        public int WorksOrderNumber { get; set; }

        public int NumberTested { get; set; }

        public int NumberOfSmtComponents { get; set; }

        public int NumberOfSmtFails { get; set; }

        public int NumberOfPcbComponents { get; set; }

        public int NumberOfPcbFails { get; set; }

        public int NumberOfPcbBoardFails { get; set; }

        public int NumberOfSmtBoardFails { get; set; }

        public int PcbOperator { get; set; }

        public int? MinutesSpent { get; set; }

        public string Machine { get; set; }

        public string PlaceFound { get; set; }

        public DateTime? DateInvalid { get; set; }

        public string FlowMachine { get; set; }

        public DateTime? FlowSolderDate { get; set; }

        public IEnumerable<AteTestDetail> Details { get; set; }
    }
}