namespace Linn.Production.Domain.LinnApps
{
    using System.Collections.Generic;

    public class WorksOrder
    {
        public int OrderNumber { get; set; }

        public Part Part { get; set; }

        public string PartNumber { get; set; }

        public List<AssemblyFail> AssemblyFails { get; set; }
    }
}
