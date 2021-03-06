﻿namespace Linn.Production.Service.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;

    public static class ClaimsPrincipalExtensions
    {
        public static string GetEmployeeUri(this ClaimsPrincipal principal)
        {
            return principal?.Claims?.FirstOrDefault(c => c.Type == "employee")?.Value;
        }

        public static IEnumerable<string> GetPrivileges(this ClaimsPrincipal principal)
        {
            return principal.Claims
                .Where(b => b.Type == "privilege")
                .Select(a => a.Value);
        }
    }
}