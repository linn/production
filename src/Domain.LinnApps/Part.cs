namespace Linn.Production.Domain.LinnApps
{
    using System.Collections.Generic;

    using Linn.Production.Domain.LinnApps.Measures;

    public class Part
    {
        public string PartNumber { get; set; }

        public string Description { get; set; }

        public List<AssemblyFail> AssemblyFails { get; set; }

        public List<WorksOrder> WorksOrders { get; set; }
    }
}