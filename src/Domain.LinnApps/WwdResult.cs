namespace Linn.Production.Domain.LinnApps
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class WwdResult
    {
        public int WwdJobId { get; set; }

        public DateTime WwdRunTime { get; set; }

        public string PartNumber { get; set; }

        public int Qty { get; set; }

        public string WorkStationCode { get; set; }

        public string PtlJobref { get; set; }

        public IEnumerable<WwdDetail> WwdDetails { get; set; }
    }
}