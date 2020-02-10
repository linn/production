namespace Linn.Production.Domain.LinnApps
{
    using System;

    public class WswShortageStory
    {
        public string Jobref { get; set; }

        public string CitCode { get; set; }

        public string PartNumber { get; set; }

        public string ShortPartNumber { get; set; }

        public string Story { get; set; }

        public DateTime SortDate { get; set; }
    }
}