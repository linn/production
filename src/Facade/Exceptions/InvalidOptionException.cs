namespace Linn.Production.Facade.Exceptions
{
    using System;

    public class InvalidOptionException : Exception
    {
        public InvalidOptionException(string message)
            : base(message)
        {
        }

        public InvalidOptionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}