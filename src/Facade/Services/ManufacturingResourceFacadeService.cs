﻿namespace Linn.Production.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;
    using Linn.Production.Resources.RequestResources;

    public class ManufacturingResourceFacadeService : FacadeFilterService<ManufacturingResource, string, ManufacturingResourceResource, ManufacturingResourceResource, IncludeInvalidRequestResource>
    {
        private readonly IRepository<ManufacturingResource, string> manufacturingResourceRepository;

        public ManufacturingResourceFacadeService(
            IRepository<ManufacturingResource, string> manufacturingResourceRepository,
            ITransactionManager transactionManager)
            : base(manufacturingResourceRepository, transactionManager)
        {
            this.manufacturingResourceRepository = manufacturingResourceRepository;
        }

        protected override ManufacturingResource CreateFromResource(ManufacturingResourceResource resource)
        {
            return new ManufacturingResource(
                resource.ResourceCode, 
                resource.Description, 
                resource.Cost);
        }

        protected override void UpdateFromResource(ManufacturingResource manufacturingResource, ManufacturingResourceResource updateResource)
        {
            manufacturingResource.Description = updateResource.Description;
            manufacturingResource.Cost = updateResource.Cost;
            manufacturingResource.DateInvalid = updateResource.DateInvalid != null
                                                    ? DateTime.Parse(updateResource.DateInvalid)
                                                    : (DateTime?)null;
        }

        protected override Expression<Func<ManufacturingResource, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<ManufacturingResource, bool>> FilterExpression(IncludeInvalidRequestResource searchTerms)
        {
            return m => searchTerms.IncludeInvalid.GetValueOrDefault() || !m.DateInvalid.HasValue;
        }
    }
}
