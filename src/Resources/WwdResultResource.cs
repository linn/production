namespace Linn.Production.Resources
{
    using System.Collections.Generic;

    public class WwdResultResource
    {
        public int WwdJobId { get; set; }

        public string PartNumber { get; set; }

        public int Qty { get; set; }

        public string WorkStationCode { get; set; }

        public IEnumerable<WwdDetailResource> WwdDetails { get; set; }
    }
}