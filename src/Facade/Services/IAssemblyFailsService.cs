namespace Linn.Production.Facade.Services
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Resources;

    public interface IAssemblyFailsService : IFacadeService<AssemblyFail, int, AssemblyFailResource, AssemblyFailResource>
    {
        IResult<IEnumerable<AssemblyFail>> RefinedSearch(
            string partNumber, 
            int? productId,
            string date, 
            string boardPart,
            string circuitPart);
    }
}