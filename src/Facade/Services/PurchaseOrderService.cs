namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.Models;
    using Linn.Production.Domain.LinnApps.Services;
    using Linn.Production.Resources;

    public class PurchaseOrderService : FacadeService<PurchaseOrder, int, PurchaseOrderResource, PurchaseOrderResource>, IPurchaseOrderService
    {
        private readonly IPurchaseOrderDomainService domainService;

        private readonly IRepository<PurchaseOrder, int> repository;

        public PurchaseOrderService(
            IRepository<PurchaseOrder, int> repository,
            IPurchaseOrderDomainService domainService,
            ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
            this.domainService = domainService;
            this.repository = repository;
        }

        public SuccessResult<PurchaseOrderWithSernosInfo> GetPurchaseOrderWithSernosInfo(int id)
        {
            var result = this.domainService.BuildPurchaseOrderWithSernosInfo(this.repository.FindById(id));
            return new SuccessResult<PurchaseOrderWithSernosInfo>(result);
        }

        protected override PurchaseOrder CreateFromResource(PurchaseOrderResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(PurchaseOrder entity, PurchaseOrderResource updateResource)
        {
            entity.Remarks = updateResource.Remarks;
        }

        protected override Expression<Func<PurchaseOrder, bool>> SearchExpression(string searchTerm)
        {
            return order => order.OrderNumber.ToString().Contains(searchTerm);
        }
    }
}
