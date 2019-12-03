namespace Linn.Production.Domain.LinnApps.Exceptions
{
    using System;

    using Linn.Common.Domain.Exceptions;

    public class InvalidSerialNumberException : DomainException
    {
        public InvalidSerialNumberException(string message)
            : base(message)
        {
        }

        public InvalidSerialNumberException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}