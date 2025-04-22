namespace Linn.Production.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Resources;

    public interface IManufacturingSkillFacadeService : IFacadeFilterService<ManufacturingSkill, string, ManufacturingSkillResource, ManufacturingSkillResource, ManufacturingSkillResource>
    {
        IResult<IEnumerable<ManufacturingSkill>> GetValid();
    }
}