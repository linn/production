namespace Linn.Production.Resources
{
    using System;

    public class SearchByDatesRequestResource
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}