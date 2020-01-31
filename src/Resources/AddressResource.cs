namespace Linn.Production.Resources
{
    using System;

    using Linn.Common.Resources;

    public class AddressResource : HypermediaResource
    {
        public int Id { get; set; }

        public string Addressee { get; set; }

        public string Addressee2 { get; set; }

        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string Line3 { get; set; }

        public string Line4 { get; set; }

        public string Country { get; set; }

        public string PostCode { get; set; }

        public string DateInvalid { get; set; }
    }
}
