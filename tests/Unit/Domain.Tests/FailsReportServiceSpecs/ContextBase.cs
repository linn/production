namespace Linn.Production.Domain.Tests.FailsReportServiceSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions.Extensions;

    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Production.Domain.LinnApps.Reports;
    using Linn.Production.Domain.LinnApps.ViewModels;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected FailsReportService Sut { get; set; }

        protected IQueryRepository<FailedParts> FailedPartsRepository { get; private set; }

        protected IReportingHelper ReportingHelper { get; private set; }

        protected IEnumerable<FailedParts> FailedParts { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.FailedPartsRepository = Substitute.For<IQueryRepository<FailedParts>>();
            this.ReportingHelper = new ReportingHelper();
            this.Sut = new FailsReportService(this.FailedPartsRepository, this.ReportingHelper);

            this.FailedParts = new List<FailedParts>
                                   {
                                       new FailedParts
                                           {
                                               PartNumber = "p1",
                                               PartDescription = "p1 desc",
                                               CitCode = "C",
                                               CitName = "C Name",
                                               PreferredSupplierId = 1,
                                               SupplierName = "s1",
                                               CreatedBy = "Person 1",
                                               DateBooked = 1.July(2021),
                                               Qty = 2,
                                               TotalValue = 34.34m,
                                               StoragePlace = "Store 1"
                                           },
                                       new FailedParts
                                           {
                                               PartNumber = "p2",
                                               PartDescription = "p2 desc",
                                               CitCode = "C",
                                               CitName = "C Name",
                                               PreferredSupplierId = 2,
                                               SupplierName = "s2",
                                               CreatedBy = "Person 2",
                                               DateBooked = 1.August(2021),
                                               Qty = 34,
                                               TotalValue = 10m,
                                               StoragePlace = "Store 2"
                                           },
                                       new FailedParts
                                           {
                                               PartNumber = "p1",
                                               PartDescription = "p1 desc",
                                               CitCode = "D",
                                               CitName = "D Name",
                                               PreferredSupplierId = 1,
                                               SupplierName = "s1",
                                               CreatedBy = "Person 1",
                                               DateBooked = 1.July(2021),
                                               Qty = 2,
                                               TotalValue = 808.08m,
                                               StoragePlace = "Store 34"
                                           }
                                   };
            this.FailedPartsRepository.FindAll().Returns(this.FailedParts.AsQueryable());
        }
    }
}