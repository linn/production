namespace Linn.Production.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Resources;

    public class PurchaseOrderService : FacadeService<PurchaseOrder, int, PurchaseOrderResource, PurchaseOrderResource>, IPurchaseOrderService
    {
        private static readonly List<string> PurchaseOrderDocTypes = new List<string> { "PO", "P", "RO" };

        private readonly IQueryRepository<SernosIssued> sernosIssuedRepository;

        private readonly IQueryRepository<SernosBuilt> sernosBuiltRepository;

        public PurchaseOrderService(
            IRepository<PurchaseOrder, int> repository,
            IQueryRepository<SernosIssued> sernosIssuedRepository,
            IQueryRepository<SernosBuilt> sernosBuiltRepository,
            ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
            this.sernosBuiltRepository = sernosBuiltRepository;
            this.sernosIssuedRepository = sernosIssuedRepository;
        }

        public int GetFirstSernos(int documentNumber)
        {
            return this.sernosIssuedRepository.FilterBy(
                s => PurchaseOrderDocTypes.Contains(s.DocumentType) && s.DocumentNumber == documentNumber).Min(s => s.SernosNumber);
        }

        public int GetLastSernos(int documentNumber)
        {
            return this.sernosIssuedRepository.FilterBy(
                s => PurchaseOrderDocTypes.Contains(s.DocumentType) && s.DocumentNumber == documentNumber).Max(s => s.SernosNumber);
        }

        public int GetSernosIssued(int documentNumber)
        {
            return this.sernosIssuedRepository.FilterBy(
                s => PurchaseOrderDocTypes.Contains(s.DocumentType) && s.DocumentNumber == documentNumber).Count();
        }

        public int GetSernosBuilt(int documentNumber, string partNumber, int firstSernos, int lastSernos)
        {
            var sernosGroup = this.sernosBuiltRepository.FilterBy(s => s.ArticleNumber == partNumber).ToList().FirstOrDefault()?.SernosGroup;
            return this.sernosBuiltRepository.FilterBy(
                s => s.SernosGroup == sernosGroup 
                     && s.SernosNumber >= firstSernos 
                     && s.SernosNumber <= lastSernos 
                     && s.ArticleNumber == partNumber).ToList().Count();
        } 

        protected override PurchaseOrder CreateFromResource(PurchaseOrderResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(PurchaseOrder entity, PurchaseOrderResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<PurchaseOrder, bool>> SearchExpression(string searchTerm)
        {
            return order => order.OrderNumber.ToString().Contains(searchTerm);
        }
    }
}
