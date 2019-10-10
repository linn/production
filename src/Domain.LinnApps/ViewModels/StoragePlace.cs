namespace Linn.Production.Domain.LinnApps.ViewModels
{
    using System;
    using System.Collections.Generic;

    using Linn.Production.Domain.LinnApps.Measures;

    public class StoragePlace
    {
        public int? LocationId { get; set; }

        public string StoragePlaceId { get; set; }

        public string Description { get; set; }

        public string SiteCode { get; set; }

        public string StorageAreaCode { get; set; }

        public int? VaxPallet { get; set; }
    }
}