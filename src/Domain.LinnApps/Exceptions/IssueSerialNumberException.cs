namespace Linn.Production.Domain.LinnApps.Exceptions
{
    using System;

    using Linn.Common.Domain.Exceptions;

    public class IssueSerialNumberException : DomainException
    {
        public IssueSerialNumberException(string message)
            : base(message)
        {
        }

        public IssueSerialNumberException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}