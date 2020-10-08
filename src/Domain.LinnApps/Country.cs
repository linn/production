namespace Linn.Production.Domain.LinnApps
{
    using System.Collections.Generic;

    public class Country
    {
        public string CountryCode { get; set; }

        public string Name { get; set; }

        public IEnumerable<Address> Addresses { get; set; }
    }
}