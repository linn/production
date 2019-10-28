namespace Linn.Production.Resources
{
    using Linn.Common.Resources;

    public class PartFailErrorTypeResource : HypermediaResource
    {
        public string ErrorType { get; set; }

        public string DateInvalid { get; set; }
    }
}