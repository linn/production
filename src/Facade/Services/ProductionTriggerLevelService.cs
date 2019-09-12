namespace Linn.Production.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class ProductionTriggerLevelService : FacadeService<ProductionTriggerLevel, string,ProductionTriggerLevelResource, ProductionTriggerLevelResource>
    {
        public ProductionTriggerLevelService(IRepository<ProductionTriggerLevel, string> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override ProductionTriggerLevel CreateFromResource(ProductionTriggerLevelResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(ProductionTriggerLevel entity, ProductionTriggerLevelResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<ProductionTriggerLevel, bool>> SearchExpression(string searchTerm)
        {
            return w => w.PartNumber.Contains(searchTerm);
        }
    }
}