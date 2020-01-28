namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Resources;

    public class AddressesResourceBuilder : IResourceBuilder<IEnumerable<Address>>
    {
        private readonly AddressResourceBuilder addressResourceBuilder = new AddressResourceBuilder();

        public IEnumerable<AddressResource> Build(IEnumerable<Address> addresses)
        {
            return addresses
                .OrderBy(b => b.Id)
                .Select(a => this.addressResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<Address>>.Build(IEnumerable<Address> addresses) => this.Build(addresses);

        public string GetLocation(IEnumerable<Address> addresses)
        {
            throw new NotImplementedException();
        }
    }
}