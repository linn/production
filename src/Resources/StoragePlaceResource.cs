namespace Linn.Production.Resources
{
    public class StoragePlaceResource
    {
        public string StoragePlaceId { get; set; }

        public int? LocationId { get; set; }

        public string Description { get; set; }

        public string SiteCode { get; set; }

        public string StorageAreaCode { get; set; }

        public int? VaxPallet { get; set; }
    }
}