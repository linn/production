namespace Linn.Production.Domain.LinnApps.ViewModels
{
    using System.Collections.Generic;

    using Linn.Production.Domain.LinnApps.Measures;

    public class StoragePlace
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public string SiteCode { get; set; }

        public string StorageAreaCode { get; set; }

        public string VaxPallet { get; set; }

        public List<PartFail> PartFails { get; set; }
    }
}