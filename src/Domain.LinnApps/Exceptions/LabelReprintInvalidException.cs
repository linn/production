namespace Linn.Production.Domain.LinnApps.Exceptions
{
    using System;

    using Linn.Common.Domain.Exceptions;

    public class LabelReprintInvalidException : DomainException
    {
        public LabelReprintInvalidException(string message)
            : base(message)
        {
        }

        public LabelReprintInvalidException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}