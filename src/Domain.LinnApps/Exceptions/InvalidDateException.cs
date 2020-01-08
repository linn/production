namespace Linn.Production.Domain.LinnApps.Exceptions
{
    using System;

    using Linn.Common.Domain.Exceptions;

    public class InvalidDateException : DomainException
    {
        public InvalidDateException(string message)
            : base(message)
        {
        }

        public InvalidDateException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}