namespace Linn.Production.Domain.LinnApps.Exceptions
{
    using System;

    using Linn.Common.Domain.Exceptions;

    public class PartFailException : DomainException
    {
        public PartFailException(string message)
            : base(message)
        {
        }

        public PartFailException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
