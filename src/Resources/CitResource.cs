namespace Linn.Production.Resources
{
    using Linn.Common.Resources;

    public class CitResource : HypermediaResource
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string BuildGroup { get; set; }

        public string DateInvalid { get; set; }

        public int? SortOrder { get; set; }

        public EmployeeResource CitLeader { get; set; }
    }
}
