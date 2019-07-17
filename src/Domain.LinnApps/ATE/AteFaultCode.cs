namespace Linn.Production.Domain.LinnApps.ATE
{
    using System;

    public class AteFaultCode
    {
        public AteFaultCode(string faultCode)
        {
            this.FaultCode = faultCode;
        }

        private AteFaultCode()
        {
            // entity framework
        }

        public string FaultCode { get; set; }

        public string Description { get; set; }

        public DateTime? DateInvalid { get; set; }
    }
}
