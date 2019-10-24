namespace Linn.Production.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.ViewModels;

    public class StoragePlaceService : IStoragePlaceService
    {
        private readonly IQueryRepository<StoragePlace> storagePlaceQueryRepository;

        public StoragePlaceService(IQueryRepository<StoragePlace> storagePlaceQueryRepository)
        {
            this.storagePlaceQueryRepository = storagePlaceQueryRepository;
        }

        public SuccessResult<IEnumerable<StoragePlace>> Search(string searchTerm)
        {
            return new SuccessResult<IEnumerable<StoragePlace>>(
                this.storagePlaceQueryRepository
                    .FilterBy(p => p.StoragePlaceId.Contains(searchTerm.ToUpper())));
        }
    }
}