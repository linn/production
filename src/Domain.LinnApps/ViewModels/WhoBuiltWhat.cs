namespace Linn.Production.Domain.LinnApps.ViewModels
{
    using System;

    public class WhoBuiltWhat
    {
        public string CitCode { get; set; }

        public string CitName { get; set; }

        public string ArticleNumber { get; set; }

        public DateTime SernosDate { get; set; }

        public int CreatedBy { get; set; }

        public string UserName { get; set; }

        public int QtyBuilt { get; set; }
    }
}
