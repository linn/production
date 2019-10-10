namespace Linn.Production.Domain.LinnApps
{
    using System;
    using System.Collections.Generic;

    using Linn.Production.Domain.LinnApps.Measures;

    public class StorageLocation
    {
        public int LocationId { get; set; }

        public string LocationCode { get; set; }

        public string Description { get; set; }

        public List<PartFail> PartFails { get; set; }
    }
}