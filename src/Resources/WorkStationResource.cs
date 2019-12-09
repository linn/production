namespace Linn.Production.Resources
{
    using Linn.Common.Resources;

    public class WorkStationResource : HypermediaResource
    {
        public string WorkStationCode { get; set; }

        public string Description { get; set; }

        public string CitCode { get; set; }

        public string VaxWorkStation { get; set; }

        public string AlternativeWorkStationCode { get; set; }

        public string ZoneType { get; set; }
    }
}
