namespace Linn.Production.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps;

    public class AddressesResponseProcessor : JsonResponseProcessor<IEnumerable<Address>>
    {
        public AddressesResponseProcessor(IResourceBuilder<IEnumerable<Address>> resourceBuilder)
            : base(resourceBuilder, "address", 1)
        {
        }
    }
}