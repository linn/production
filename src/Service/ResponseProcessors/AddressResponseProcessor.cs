namespace Linn.Production.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class AddressResponseProcessor : JsonResponseProcessor<Address>
    {
        public AddressResponseProcessor(IResourceBuilder<Address> resourceBuilder)
            : base(resourceBuilder, "addresses", 1)
        {
        }
    }
}