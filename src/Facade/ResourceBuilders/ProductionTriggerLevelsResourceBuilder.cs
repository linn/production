namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public class ProductionTriggerLevelsResourceBuilder : IResourceBuilder<ResponseModel<IEnumerable<ProductionTriggerLevel>>>
    {
        private readonly ProductionTriggerLevelResourceBuilder productionTriggerLevelResourceBuilder; 

        public ProductionTriggerLevelsResourceBuilder(IAuthorisationService authorisationService)
        {
            this.productionTriggerLevelResourceBuilder = new ProductionTriggerLevelResourceBuilder(authorisationService);
        }

        public IEnumerable<ProductionTriggerLevelResource> Build(ResponseModel<IEnumerable<ProductionTriggerLevel>> model)
        {
            return model.ResponseData
                .OrderBy(b => b.PartNumber)
                .Select(a => this.productionTriggerLevelResourceBuilder.Build(new ResponseModel<ProductionTriggerLevel>(a, model.Privileges)));
        }

        object IResourceBuilder<ResponseModel<IEnumerable<ProductionTriggerLevel>>>.Build(ResponseModel<IEnumerable<ProductionTriggerLevel>> model) => this.Build(model);

        public string GetLocation(ResponseModel<IEnumerable<ProductionTriggerLevel>> productionTriggerLevels)
        {
            throw new NotImplementedException();
        }
    }
}