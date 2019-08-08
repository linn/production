using Linn.Common.Resources;

namespace Linn.Production.Resources
{
    public class BoardFailTypeResource : HypermediaResource
    {
        public int FailType { get; set; }

        public string Description { get; set; }
    }
}