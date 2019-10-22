namespace Linn.Production.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Production.Domain.LinnApps.ViewModels;

    public class StoragePlacesResponseProcessor : JsonResponseProcessor<IEnumerable<StoragePlace>>
    {
        public StoragePlacesResponseProcessor(IResourceBuilder<IEnumerable<StoragePlace>> resourceBuilder)
            : base(resourceBuilder, "storage-places", 1)
        {
        }
    }
}