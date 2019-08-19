namespace Linn.Production.Resources
{
    using Linn.Common.Resources;

    public class BoardFailTypeResource : HypermediaResource
    {
        public int FailType { get; set; }

        public string Description { get; set; }
    }
}