namespace Linn.Production.Domain.LinnApps
{
    using System.Collections.Generic;

    using Linn.Production.Domain.LinnApps.Measures;

    public class StorageLocation
    {
        public string LocationId { get; set; }

        public string LocationCode { get; set; }

        public List<PartFail> PartFails { get; set; }
    }
}