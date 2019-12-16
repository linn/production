namespace Linn.Production.Domain.LinnApps.Exceptions
{
    using System;

    using Linn.Common.Domain.Exceptions;

    public class BuildPlanDetailInvalidException : DomainException
    {
        public BuildPlanDetailInvalidException(string message)
            : base(message)
        {
        }

        public BuildPlanDetailInvalidException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
