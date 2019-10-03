namespace Linn.Production.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Repositories;
    using Linn.Production.Domain.LinnApps.ViewModels;

    public class StoragePlaceService : IStoragePlaceService
    {
        private readonly IQueryRepository<StoragePlace> storagePlaceQueryRepository;

        public StoragePlaceService(IQueryRepository<StoragePlace> storagePlaceQueryRepository)
        {
            this.storagePlaceQueryRepository = storagePlaceQueryRepository;
        }

        public SuccessResult<IEnumerable<StoragePlace>> GetAll()
        {
            return new SuccessResult<IEnumerable<StoragePlace>>(this.storagePlaceQueryRepository.FindAll());
        }
    }
}