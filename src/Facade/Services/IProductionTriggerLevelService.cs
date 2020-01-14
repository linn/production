namespace Linn.Production.Facade.Services
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public interface IProductionTriggerLevelsService : IFacadeService<ProductionTriggerLevel, string, ProductionTriggerLevelResource, ProductionTriggerLevelResource>
    {
        IResult<ResponseModel<IEnumerable<ProductionTriggerLevel>>> Search(ProductionTriggerLevelsSearchRequestResource resource, IEnumerable<string> privileges);

        IResult<ResponseModel<ProductionTriggerLevel>> Remove(string partNumber, IEnumerable<string> privileges);
        
    }
}
