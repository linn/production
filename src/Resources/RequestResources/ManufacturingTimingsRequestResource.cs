namespace Linn.Production.Resources.RequestResources
{
    using System;

    public class ManufacturingTimingsRequestResource
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string CitCode { get; set; }
    }
}