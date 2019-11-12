namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Resources;

    public class PartFailSuppliersResourceBuilder : IResourceBuilder<IEnumerable<PartFailSupplierView>>
    {
        private readonly PartFailSupplierResourceBuilder partFailSupplierResourceBuilder =
            new PartFailSupplierResourceBuilder();

        public IEnumerable<PartFailSupplierResource> Build(IEnumerable<PartFailSupplierView> suppliers)
        {
            return suppliers.OrderBy(s => s.SupplierName).Select(s => this.partFailSupplierResourceBuilder.Build(s));
        }

        object IResourceBuilder<IEnumerable<PartFailSupplierView>>.Build(IEnumerable<PartFailSupplierView> suppliers) =>
            this.Build(suppliers);

        public string GetLocation(IEnumerable<PartFailSupplierView> model)
        {
            throw new NotImplementedException();
        }
    }
}