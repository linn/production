namespace Linn.Production.Domain.LinnApps.Measures
{
    using System;
    using System.Collections.Generic;

    public class PartFailErrorType
    {
        public string ErrorType { get; set; }

        public DateTime? DateInvalid { get; set; }

        public List<PartFail> PartFails { get; set; }
    }
}