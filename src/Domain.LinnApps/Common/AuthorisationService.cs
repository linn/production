namespace Linn.Production.Domain.LinnApps.Common
{
    using System.Collections.Generic;
    using System.Linq;

    public class AuthorisationService : IAuthorisationService
    {
        public bool HasPermissionFor(string action, IEnumerable<string> privileges)
        {
            return this.Satisfies(action, privileges);
        }

        private bool Satisfies(string privilegeRequired, IEnumerable<string> privileges)
        {
            return privileges != null && privileges.Contains(privilegeRequired);
        }
    }
}