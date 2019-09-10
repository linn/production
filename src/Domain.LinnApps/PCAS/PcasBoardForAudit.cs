namespace Linn.Production.Domain.LinnApps.PCAS
{
    using System;

    public class PcasBoardForAudit
    {
        public string BoardCode { get; set; }

        public DateTime? DateAdded { get; set; }

        public string ForAudit { get; set; }

        public string CutClinch { get; set; }
    }
}
