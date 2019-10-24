namespace Linn.Production.Domain.LinnApps.Exceptions
{
    using System;

    using Linn.Common.Domain.Exceptions;

    public class InvalidPartNumberException : DomainException
    {
        public InvalidPartNumberException(string message)
            : base(message)
        {
        }

        public InvalidPartNumberException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}