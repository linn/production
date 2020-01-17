namespace Linn.Production.Resources
{  
    using System.Collections.Generic;

    using Linn.Common.Resources;

    public class AteTestResource : HypermediaResource
    {
        public int? TestId { get; set; }

        public int UserNumber { get; set; }

        public string UserName { get; set; }

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

        public string PcbOperatorName { get; set; }

        public int? MinutesSpent { get; set; }

        public string Machine { get; set; }

        public string PlaceFound { get; set; }

        public string DateInvalid { get; set; }

        public string FlowMachine { get; set; }

        public string FlowSolderDate { get; set; }

        public string PartNumber { get; set; }

        public string PartDescription { get; set; }

        public int WorksOrderQuantity { get; set; }

        public IEnumerable<AteTestDetailResource> Details { get; set; }
    }
}