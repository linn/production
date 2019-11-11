namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;

    public class SuppliersResourceBuilder : IResourceBuilder<IEnumerable<Supplier>>
    {
        private readonly SupplierResourceBuilder resourceBuilder = new SupplierResourceBuilder();

        public object Build(IEnumerable<Supplier> suppliers)
        {
            return suppliers.OrderBy(s => s.SupplierName).Select(s => this.resourceBuilder.Build(s));
        }

        public string GetLocation(IEnumerable<Supplier> model)
        {
            throw new System.NotImplementedException();
        }
    }
}