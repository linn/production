namespace Linn.Production.Domain.LinnApps.Measures
{
    public class Cit
    {
        public string Code { get; set;}

        public string Name { get; set; }

        public string BuildGroup { get; set; }

        public int? SortOrder { get; set; }

        public ProductionMeasures Measures { get; set; }
    }
}
