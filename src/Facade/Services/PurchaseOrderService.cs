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
        private readonly IQueryRepository<SernosIssued> sernosIssuedRepository;

        private readonly IQueryRepository<SernosBuilt> sernosBuiltRepository;

        private static readonly List<string> PurchaseOrderDocTypes = new List<string> { "PO", "P", "RO" };

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

        //-- first and last sernos
        //SELECT MIN(SERNOS_NUMBER),MAX(SERNOS_NUMBER),COUNT(SERNOS_NUMBER),
        //SERNOS_GROUP
        //    FROM SERNOS_ISSUED_VIEW
        //    WHERE DOCUMENT_TYPE IN('PO','P','RO')
        //AND DOCUMENT_NUMBER = 610262
        //GROUP BY SERNOS_GROUP; -- sernos issued

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
            throw new NotImplementedException();
        }

        public int GetSernosBuilt(int documentNumber, string SernosGroup)
        {
            throw new NotImplementedException();
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