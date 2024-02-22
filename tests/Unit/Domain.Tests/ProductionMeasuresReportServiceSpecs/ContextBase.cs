namespace Linn.Production.Domain.Tests.ProductionMeasuresReportServiceSpecs
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
        protected ProductionMeasuresReportService Sut { get; set; }

        protected IQueryRepository<FailedParts> FailedPartsRepository { get; private set; }

        protected IQueryRepository<ProductionDaysRequired> ProductionDaysRequiredRepository { get; private set; }

        protected IReportingHelper ReportingHelper { get; private set; }

        protected IEnumerable<FailedParts> FailedParts { get; private set; }

        protected IEnumerable<ProductionDaysRequired> ProductionDaysRequired { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.FailedPartsRepository = Substitute.For<IQueryRepository<FailedParts>>();
            this.ProductionDaysRequiredRepository = Substitute.For<IQueryRepository<ProductionDaysRequired>>();
            this.ReportingHelper = new ReportingHelper();
            this.Sut = new ProductionMeasuresReportService(
                this.FailedPartsRepository,
                this.ProductionDaysRequiredRepository,
                this.ReportingHelper);

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
                                               Qty = 2.5m,
                                               TotalValue = 34.34m,
                                               StoragePlace = "Store 1",
                                               StockPoolCode = "SPC 1"
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
                                               StoragePlace = "Store 2",
                                               StockPoolCode = "SPC 2"
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
                                           },
                                       new FailedParts
                                           {
                                               PartNumber = "variant of p1",
                                               PartDescription = "p1 desc",
                                               CitCode = "C",
                                               CitName = "C Name",
                                               PreferredSupplierId = 1,
                                               SupplierName = "s1",
                                               CreatedBy = "Person 1",
                                               DateBooked = 1.July(2019),
                                               Qty = 2,
                                               TotalValue = 34.34m,
                                               StoragePlace = "Store 1"
                                           }
                                   };
            this.FailedPartsRepository.FindAll().Returns(this.FailedParts.AsQueryable());

            this.ProductionDaysRequired = new List<ProductionDaysRequired>
                                              {
                                                  new ProductionDaysRequired
                                                      {
                                                          Priority = "1",
                                                          PartNumber = "p1",
                                                          PartDescription = "p1 desc",
                                                          QtyBeingBuilt = 1,
                                                          BuildQty = 2,
                                                          CanBuild = 2,
                                                          CanBuildExcludingSubAssemblies = 1,
                                                          QtyBeingBuiltDays = 1.2345d,
                                                          CanBuildDays = 2.3456d,
                                                          CitCode = "C",
                                                          EffectiveKanbanSize = 2
                                                      },
                                                  new ProductionDaysRequired
                                                      {
                                                          Priority = "1",
                                                          PartNumber = "p2",
                                                          PartDescription = "p2 desc",
                                                          QtyBeingBuilt = 2,
                                                          BuildQty = 4,
                                                          CanBuild = 3,
                                                          CanBuildExcludingSubAssemblies = 3,
                                                          QtyBeingBuiltDays = 1d,
                                                          CanBuildDays = 4.456789d,
                                                          CitCode = "C",
                                                          EffectiveKanbanSize = 1
                                                      },
                                                  new ProductionDaysRequired
                                                      {
                                                          Priority = "2",
                                                          PartNumber = "p3",
                                                          PartDescription = "p3 desc",
                                                          QtyBeingBuilt = 1,
                                                          BuildQty = 2,
                                                          CanBuild = 2,
                                                          CanBuildExcludingSubAssemblies = 1,
                                                          QtyBeingBuiltDays = 1d,
                                                          CanBuildDays = 2d,
                                                          CitCode = "C",
                                                          EffectiveKanbanSize = 2
                                                      },
                                                  new ProductionDaysRequired
                                                      {
                                                          Priority = "2",
                                                          PartNumber = "p4",
                                                          PartDescription = "p4 desc",
                                                          QtyBeingBuilt = 1,
                                                          BuildQty = 2,
                                                          CanBuild = 2,
                                                          CanBuildExcludingSubAssemblies = 1,
                                                          QtyBeingBuiltDays = 1d,
                                                          CanBuildDays = 2d,
                                                          CitCode = "D",
                                                          EffectiveKanbanSize = 2
                                                      }
                                              };
            this.ProductionDaysRequiredRepository.FindAll().Returns(this.ProductionDaysRequired.AsQueryable());
        }
    }
}