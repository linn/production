namespace Linn.Production.Domain.LinnApps
{
    using System;

    public class AccountingCompany
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int LatestSosJobId { get; set; }

        public DateTime? DateLatestSosJobId { get; set; }
    }
}