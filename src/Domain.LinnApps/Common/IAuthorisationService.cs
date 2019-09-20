namespace Linn.Production.Domain.LinnApps.Common
{
    using System.Collections.Generic;

    public interface IAuthorisationService
    {
        bool HasPermissionFor(string action, IEnumerable<string> privileges);
    }
}