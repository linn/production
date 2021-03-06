﻿namespace Linn.Production.Facade.ResourceBuilders
{
    using System;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Resources;

    public class StoragePlaceResourceBuilder : IResourceBuilder<StoragePlace>
    {
        public StoragePlaceResource Build(StoragePlace storagePlace)
        {
            return new StoragePlaceResource
                       {
                         StoragePlaceId = storagePlace.StoragePlaceId,
                         LocationId = storagePlace.LocationId,
                         Description = storagePlace.Description,
                         SiteCode = storagePlace.SiteCode,
                         StorageAreaCode = storagePlace.StorageAreaCode,
                         VaxPallet = storagePlace.VaxPallet
                       };
        }

        public string GetLocation(StoragePlace storagePlace)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<StoragePlace>.Build(StoragePlace storagePlace) => this.Build(storagePlace);
    }
}