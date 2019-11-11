namespace Linn.Production.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class SupplierResourceBuilder : IResourceBuilder<Supplier>
    {
        public SupplierResource Build(Supplier model)
        {
            return new SupplierResource { SupplierId = model.SupplierId, SupplierName = model.SupplierName };
        }

        object IResourceBuilder<Supplier>.Build(Supplier model) => this.Build(model);

        public string GetLocation(Supplier model)
        {
            throw new System.NotImplementedException();
        }
    }
}