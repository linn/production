namespace Linn.Production.Domain.LinnApps.Exceptions
{
    using System;

    using Linn.Common.Domain.Exceptions;

    public class InvalidPurchaseOrderException : DomainException
    {
        public InvalidPurchaseOrderException(string message)
            : base(message)
        {
        }

        public InvalidPurchaseOrderException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}