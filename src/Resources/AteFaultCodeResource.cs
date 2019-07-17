namespace Linn.Production.Resources
{
    using Linn.Common.Resources;

    public class AteFaultCodeResource : HypermediaResource
    {
        public string FaultCode { get; set; }

        public string Description { get; set; }

        public string DateInvalid { get; set; }
    }
}
