namespace Linn.Production.Resources
{
    using System;

    public class SearchRequestResource
    {
        public string SearchTerm { get; set; }

        public int? Limit { get; set; }

        public string OrderByDesc { get; set; }
    }
}