namespace Linn.Production.Domain.LinnApps
{
    using System.Collections.Generic;

    public class Bom
    {
        public int BomId { get; set; }

        public Part Part { get; set; }

        public string BomName { get; set; }

        public IEnumerable<BomDetail> Details { get; set; }
    }
}
