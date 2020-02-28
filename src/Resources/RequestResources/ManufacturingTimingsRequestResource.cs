namespace Linn.Production.Resources
{
    using System;

    public class ManufacturingTimingsRequestResource
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public char CitCode { get; set; }
    }
}