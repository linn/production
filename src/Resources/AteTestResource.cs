namespace Linn.Production.Resources
{  
    using System.Collections.Generic;

    public class AteTestResource
    {
        public int TestId { get; set; }

        public int UserNumber { get; set; }

        public string DateTested { get; set; }

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

        public string DateInvalid { get; set; }

        public string FlowMachine { get; set; }

        public string FlowSolderDate { get; set; }

        public IEnumerable<AteTestDetailResource> Details { get; set; }
    }
}