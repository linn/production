namespace Linn.Production.Resources
{
    using Linn.Common.Resources;

    public class AssemblyFailFaultCodeResource : HypermediaResource
    {
        public string FaultCode { get; set; }

        public string DateInvalid { get; set; }

        public string Description { get; set; }

        public string Explanation { get; set; }
    }
}