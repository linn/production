namespace Linn.Production.Domain.LinnApps.Exceptions
{
    using System;

    using Linn.Common.Domain.Exceptions;

    public class InvalidSerialNumberReissueException : DomainException
    {
        public InvalidSerialNumberReissueException(string message) 
            : base(message)
        {
        }

        public InvalidSerialNumberReissueException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}
