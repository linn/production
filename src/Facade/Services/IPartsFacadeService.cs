namespace Linn.Production.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;

    public interface IPartsFacadeService
    {
        SuccessResult<IEnumerable<Part>> SearchParts(string searchTerm);

        SuccessResult<IEnumerable<Part>> GetAll();
    }
}