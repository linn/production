namespace Linn.Production.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class PartsFacadeService : FacadeService<Part, string, PartResource, PartResource>
    {
        // private readonly IRepository<Part, string> repository;
        //
        // public PartsFacadeService(IRepository<Part, string> repository)
        // {
        //     this.repository = repository;
        // }
        //
        // public SuccessResult<IEnumerable<Part>> SearchParts(string searchTerm)
        // {
        //     return new SuccessResult<IEnumerable<Part>>(this.repository.FilterBy(s => s.PartNumber.Contains(searchTerm.ToUpper())).Take(10));
        // }
        //
        // public SuccessResult<IEnumerable<Part>> GetAll()
        // {
        //     return new SuccessResult<IEnumerable<Part>>(this.repository.FindAll());
        // }

        public PartsFacadeService(IRepository<Part, string> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override Part CreateFromResource(PartResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(Part entity, PartResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<Part, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}