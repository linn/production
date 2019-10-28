namespace Linn.Production.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Resources;

    public class StoragePlacesResourceBuilder : IResourceBuilder<IEnumerable<StoragePlace>>
    {
        private readonly StoragePlaceResourceBuilder storagePlaceResourceBuilder = new StoragePlaceResourceBuilder();

        public IEnumerable<StoragePlaceResource> Build(IEnumerable<StoragePlace> storagePlaces)
        {
            return storagePlaces.Select(a => this.storagePlaceResourceBuilder.Build(a));
        }

        public string GetLocation(IEnumerable<StoragePlace> model)
        {
            throw new System.NotImplementedException();
        }

        object IResourceBuilder<IEnumerable<StoragePlace>>.Build(IEnumerable<StoragePlace> storagePlaces) => this.Build(storagePlaces);
    }
}