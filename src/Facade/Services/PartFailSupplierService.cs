namespace Linn.Production.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.ViewModels;

    public class PartFailSupplierService : IPartFailSupplierService
    {
        private readonly IQueryRepository<PartFailSupplierView> partFailSupplierRepository;

        public PartFailSupplierService(IQueryRepository<PartFailSupplierView> partFailSupplierRepository)
        {
            this.partFailSupplierRepository = partFailSupplierRepository;
        }

        public SuccessResult<IEnumerable<PartFailSupplierView>> GetAll()
        {
            return new SuccessResult<IEnumerable<PartFailSupplierView>>(this.partFailSupplierRepository.FindAll());
        }
    }
}