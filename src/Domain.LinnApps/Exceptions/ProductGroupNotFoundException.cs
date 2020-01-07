namespace Linn.Production.Domain.LinnApps.Exceptions
{
    using System;

    using Linn.Common.Domain.Exceptions;

    public class ProductGroupNotFoundException : DomainException
    {
        public ProductGroupNotFoundException(string message)
            : base(message)
        {
        }

        public ProductGroupNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}