namespace Linn.Production.Facade.ResourceBuilders
{
    using System;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Resources;

    public class PartFailSupplierResourceBuilder : IResourceBuilder<PartFailSupplierView>
    {
        public PartFailSupplierResource Build(PartFailSupplierView supplier)
        {
            return new PartFailSupplierResource
                       {
                           SupplierId = supplier.SupplierId,
                           SupplierName = supplier.SupplierName
                       };
        }
        
        public string GetLocation(PartFailSupplierView model)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<PartFailSupplierView>.Build(PartFailSupplierView supplier) => this.Build(supplier);
    }
}