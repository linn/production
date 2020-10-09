namespace Linn.Production.Domain.LinnApps.Services
{
    using System;
    using System.Collections.Generic;

    using Linn.Production.Domain.LinnApps.Measures;

    public interface IAssemblyFailsDomainService
    {
        IEnumerable<AssemblyFail> RefinedSearchAssemblyFails(
            string partNumber,
            int? productId,
            DateTime? date,
            string boardPart,
            string circuitPart);
    }
}