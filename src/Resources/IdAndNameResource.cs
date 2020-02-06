namespace Linn.Production.Resources
{
    using Linn.Common.Resources;

    public class IdAndNameResource : HypermediaResource
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
