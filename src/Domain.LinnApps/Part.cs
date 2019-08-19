namespace Linn.Production.Domain.LinnApps
{
    using System.Collections.Generic;

    public class Part
    {
        public string PartNumber { get; set; }

        public string Description { get; set; }

        public List<AssemblyFail> AssemblyFails { get; set; }
    }
}