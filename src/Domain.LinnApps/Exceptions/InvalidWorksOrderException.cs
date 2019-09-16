namespace Linn.Production.Domain.LinnApps.Exceptions
{
    using System;

    using Linn.Common.Domain.Exceptions;

    public class InvalidWorksOrderException : DomainException
    {
        public InvalidWorksOrderException(string message)
            : base(message)
        {
        }

        public InvalidWorksOrderException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
