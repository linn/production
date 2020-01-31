namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class SupplierResourceBuilder : IResourceBuilder<Supplier>
    {
        public SupplierResource Build(Supplier supplier)
        {
            return new SupplierResource
            {
                SupplierId = supplier.SupplierId,
                SupplierName = supplier.SupplierName,
                DateClosed = supplier.DateClosed?.ToString("o"),
                InvoiceAddressId = supplier.InvoiceAddressId,
                OrderAddressId = supplier.OrderAddressId,
                
                Links = this.BuildLinks(supplier).ToArray()
            };
        }

        public string GetLocation(Supplier supplier)
        {
            return $"production/maintenance/labels/suppliers/{supplier.SupplierId}";
        }

        object IResourceBuilder<Supplier>.Build(Supplier supplier) => this.Build(supplier);

        private IEnumerable<LinkResource> BuildLinks(Supplier supplier)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(supplier) };
        }
    }
}
