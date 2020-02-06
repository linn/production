namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class AddressResourceBuilder : IResourceBuilder<Address>
    {
        public AddressResource Build(Address address)
        {
            return new AddressResource
            {
                Id = address.Id,
                Addressee = address.Addressee,
                Addressee2 = address.Addressee2,
                Line1 = address.Line1,
                Line2 = address.Line2,
                Line3 = address.Line3,
                Line4 = address.Line4,
                PostCode = address.PostCode,
                Country = address.Country.Name,
                DateInvalid = address.DateInvalid?.ToString("o"),
                Links = this.BuildLinks(address).ToArray()
            };
        }

        public string GetLocation(Address address)
        {
            return $"production/maintenance/labels/addresses/{address.Id}";
        }

        object IResourceBuilder<Address>.Build(Address address) => this.Build(address);

        private IEnumerable<LinkResource> BuildLinks(Address address)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(address) };
        }
    }
}
