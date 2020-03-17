namespace Linn.Production.Resources
{
    using Linn.Common.Resources;

    public class PartResource : HypermediaResource
    {
        public string PartNumber { get; set; }

        public string Description { get; set; }

        public int? BomId { get; set; }

        public string BomType { get; set; }

        public string DecrementRule { get; set; }

        public string SernosSequence { get; set; }

        public string LibraryRef { get; set; }

        public string FootprintRef { get; set; }
    }
}
