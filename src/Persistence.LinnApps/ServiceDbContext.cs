namespace Linn.Production.Persistence.LinnApps
{
    using Linn.Common.Configuration;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Domain.LinnApps.BackOrders;
    using Linn.Production.Domain.LinnApps.BoardTests;
    using Linn.Production.Domain.LinnApps.BuildPlans;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.Models;
    using Linn.Production.Domain.LinnApps.PCAS;
    using Linn.Production.Domain.LinnApps.Products;
    using Linn.Production.Domain.LinnApps.SerialNumberReissue;
    using Linn.Production.Domain.LinnApps.Triggers;
    using Linn.Production.Domain.LinnApps.ViewModels;
    using Linn.Production.Domain.LinnApps.WorksOrders;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class ServiceDbContext : DbContext
    {
        public static readonly LoggerFactory MyLoggerFactory =
            new LoggerFactory(new[] { new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider() });

        public DbSet<Department> Departments { get; set; }

        public DbQuery<Build> Builds { get; set; }

        public DbSet<AteFaultCode> AteFaultCodes { get; set; }

        public DbSet<SerialNumberReissue> SerialNumberReissues { get; set; }

        public DbSet<Cit> Cits { get; set; }

        public DbSet<ProductionMeasures> ProductionMeasures { get; set; }

        public DbQuery<WhoBuiltWhat> WhoBuiltWhat { get; set; }

        public DbQuery<FailedParts> FailedParts { get; set; }

        public DbQuery<ProductionDaysRequired> ProductionDaysRequired { get; set; }

        public DbQuery<BomDetailExplodedPhantomPartView> BomDetailExplodedPhantomPartView { get; set; }

        public DbSet<ManufacturingSkill> ManufacturingSkills { get; set; }

        public DbSet<ManufacturingRoute> ManufacturingRoutes { get; set; }

        public DbSet<ManufacturingOperation> ManufacturingOperations { get; set; }

        public DbSet<BoardFailType> BoardFailTypes { get; set; }

        public DbSet<AssemblyFail> AssemblyFails { get; set; }

        public DbSet<WorksOrder> WorksOrders { get; set; }

        public DbSet<WorksOrderLabel> WorksOrderLabels { get; set; }

        public DbSet<Part> Parts { get; set; }

        public DbSet<AssemblyFailFaultCode> AssemblyFailFaultCodes { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<ProductionTriggerLevel> ProductionTriggerLevels { get; set; }

        public DbSet<WorkStation> WorkStations { get; set; }

        public DbSet<PcasBoardForAudit> PcasBoardsForAudit { get; set; }

        public DbQuery<PcasRevision> PcasRevisions { get; set; }

        public DbSet<ManufacturingResource> ManufacturingResources { get; set; }

        public DbQuery<SmtShift> SmtShifts { get; set; }

        public DbQuery<ProductionTrigger> ProductionTriggers { get; set; }

        public DbQuery<ProductionBackOrder> ProductionBackOrders { get; set; }

        public DbQuery<ProductionTriggerAssembly> ProductionTriggerAssemblies { get; set; }

        public DbSet<LinnWeek> LinnWeeks { get; set; }

        public DbSet<PtlSettings> PtlSettings { get; set; }

        public DbQuery<AccountingCompany> AccountingCompanies { get; set; }

        public DbQuery<MCDLine> MCDLines { get; set; }

        public DbQuery<OverdueOrderLine> OverdueOrderLines { get; set; }

        public DbQuery<StoragePlace> StoragePlaces { get; set; }

        public DbSet<StorageLocation> StorageLocations { get; set; }

        public DbSet<PartFail> PartFails { get; set; }

        public DbSet<PartFailErrorType> PartFailErrorTypes { get; set; }

        public DbSet<PartFailFaultCode> PartFailFaultCodes { get; set; }

        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }

        public DbQuery<EmployeeDepartmentView> EmployeeDepartmentView { get; set; }

        public DbSet<ProductData> ProductData { get; set; }

        public DbSet<BoardTest> BoardTests { get; set; }

        public DbSet<TestMachine> TestMachines { get; set; }

        public DbSet<LabelType> LabelTypes { get; set; }

        public DbQuery<WwdDetail> WwdDetails { get; set; }

        public DbQuery<PartFailSupplierView> PartFailSuppliersView { get; set; }

        public DbQuery<ProductionBackOrdersView> ProductionBackOrdersView { get; set; }

        public DbSet<LabelReprint> LabelReprints { get; set; }

        public DbSet<SerialNumber> SerialNumbers { get; set; }

        public DbSet<BuildPlan> BuildPlans { get; set; }

        public DbQuery<BuildPlanDetailsReportLine> BuildPlanDetailsReportLines { get; set; }

        public DbSet<BuildPlanDetail> BuildPlanDetails { get; set; }

        public DbQuery<BuildPlanRule> BuildPlanRules { get; set; }

        public DbSet<AteTest> AteTests { get; set; }

        public DbSet<AteTestDetail> AteTestDetails { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Address> Addresses { get; set; } 

        public DbQuery<BuiltThisWeekStatistic> BuiltThisWeekStatistics { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbQuery<PtlStat> PtlStats { get; set; }

        public DbQuery<SernosIssued> SernosIssuedView { get; set; }

        public DbQuery<SernosBuilt> SernosBuiltView { get; set; }

        public DbQuery<PurchaseOrdersReceived> PurchaseOrdersReceivedView { get; set; }
        public DbQuery<WswShortage> WswShortages { get; set; }

        public DbQuery<OsrRunMaster> OsrRunMaster { get; set; }

        public DbQuery<PtlMaster> PtlMaster { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            this.BuildAte(builder);
            this.BuildSerialNumberReissues(builder);
            this.BuildDepartments(builder);
            this.BuildBuilds(builder);
            this.BuildCits(builder);
            this.BuildProductionMeasures(builder);
            this.QueryWhoBuildWhat(builder);
            this.QueryFailedParts(builder);
            this.QueryDaysRequired(builder);
            this.QueryMCDLines(builder);
            this.BuildManufacturingResources(builder);
            this.BuildManufacturingSkills(builder);
            this.BuildManufacturingRoutes(builder);
            this.BuildManufacturingOperations(builder);
            this.BuildBoardFailTypes(builder);
            this.BuildTestMachines(builder);
            this.BuildBoardTests(builder);
            this.BuildAssemblyFails(builder);
            this.BuildWorkOrders(builder);
            this.BuildParts(builder);
            this.BuildEmployees(builder);
            this.BuildAssemblyFailFaultCodes(builder);
            this.BuildWorkStations(builder);
            this.BuildProductionTriggerLevels(builder);
            this.BuildPcasBoardsForAudit(builder);
            this.QueryPcasRevisions(builder);
            this.QueryPtlMaster(builder);
            this.QueryOsrRunMaster(builder);
            this.QueryProductionTriggers(builder);
            this.BuildLinnWeeks(builder);
            this.BuildBomDetailPhantomView(builder);
            this.QuerySmtShifts(builder);
            this.BuildPtlSettings(builder);
            this.QueryStoragePlaces(builder);
            this.BuildPartFailFaultCodes(builder);
            this.BuildPartFails(builder);
            this.BuildPartFailErrorTypes(builder);
            this.BuildLabelReprints(builder);
            this.BuildStorageLocations(builder);
            this.BuildPurchaseOrders(builder);
            this.QueryAccountingCompanies(builder);
            this.QueryProductionBackOrders(builder);
            this.QueryProductionTriggerAssemblies(builder);
            this.BuildPurchaseOrderDetails(builder);
            this.QueryOverdueOrderLines(builder);
            this.QueryEmployeeDepartmentView(builder);
            this.BuildProductData(builder);
            this.BuildWorksOrdersLabels(builder);
            this.QueryPartFailSuppliersView(builder);
            this.QueryProductionBackOrdersView(builder);
            this.QueryWwdDetails(builder);
            this.BuildSerialNumbers(builder);
            this.BuildBuildPlans(builder);
            this.QueryBuildPlanDetailsReportLines(builder);
            this.QueryBuildPlanDetails(builder);
            this.BuildAteTests(builder);
            this.BuildAteTestDetails(builder);
            this.QueryBuildPlanRules(builder);
            this.QueryBuiltThisWeekStatistics(builder);
            this.QueryPtlStats(builder);
            this.QueryWswShortages(builder);
            base.OnModelCreating(builder);
            this.BuildLabelTypes(builder);
            this.BuildCountries(builder);
            this.BuildAddresses(builder);
            this.QuerySernosBuiltView(builder);
            this.QuerySernosIssuedView(builder);
            this.QueryPurchaseOrdersReceivedView(builder);
            this.BuildSuppliers(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var host = ConfigurationManager.Configuration["DATABASE_HOST"];
            var userId = ConfigurationManager.Configuration["DATABASE_USER_ID"];
            var password = ConfigurationManager.Configuration["DATABASE_PASSWORD"];
            var serviceId = ConfigurationManager.Configuration["DATABASE_NAME"];

            var dataSource =
                $"(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={host})(PORT=1521))(CONNECT_DATA=(SERVICE_NAME={serviceId})(SERVER=dedicated)))";

            optionsBuilder.UseOracle($"Data Source={dataSource};User Id={userId};Password={password};");
            optionsBuilder.UseLoggerFactory(MyLoggerFactory);
            optionsBuilder.EnableSensitiveDataLogging(true);
            base.OnConfiguring(optionsBuilder);
        }

        private void BuildSerialNumbers(ModelBuilder builder)
        {
            builder.Entity<SerialNumber>().ToTable("SERNOS");
            builder.Entity<SerialNumber>().HasKey(s => s.SernosTRef);
            builder.Entity<SerialNumber>().HasAlternateKey(r => new { r.SernosGroup, r.SernosNumber, r.TransCode });
            builder.Entity<SerialNumber>().Property(s => s.SernosTRef).HasColumnName("SERNOS_TREF");
            builder.Entity<SerialNumber>().Property(s => s.SernosGroup).HasColumnName("SERNOS_GROUP").HasMaxLength(10);
            builder.Entity<SerialNumber>().Property(s => s.SernosNumber).HasColumnName("SERNOS_NUMBER");
            builder.Entity<SerialNumber>().Property(s => s.SernosDate).HasColumnName("SERNOS_DATE");
            builder.Entity<SerialNumber>().Property(s => s.DocumentType).HasColumnName("DOCUMENT_TYPE").HasMaxLength(2);
            builder.Entity<SerialNumber>().Property(s => s.DocumentNumber).HasColumnName("DOCUMENT_NUMBER");
            builder.Entity<SerialNumber>().Property(s => s.DocumentLine).HasColumnName("DOCUMENT_LINE");
            builder.Entity<SerialNumber>().Property(s => s.DatePostedToVax).HasColumnName("DATE_POSTED_TO_VAX");
            builder.Entity<SerialNumber>().Property(s => s.OutletNumber).HasColumnName("OUTLET_NUMBER");
            builder.Entity<SerialNumber>().Property(s => s.PrevSernosNumber).HasColumnName("PREV_SERNOS_NUMBER");
            builder.Entity<SerialNumber>().Property(s => s.OutletNumber).HasColumnName("OUTLET_NUMBER");
            builder.Entity<SerialNumber>().Property(s => s.AccountId).HasColumnName("ACCOUNT_ID");
            builder.Entity<SerialNumber>().Property(s => s.CreatedBy).HasColumnName("CREATED_BY");
            builder.Entity<SerialNumber>().Property(s => s.TransCode).HasColumnName("TRANS_CODE").HasMaxLength(10);
            builder.Entity<SerialNumber>().Property(s => s.ArticleNumber).HasColumnName("ARTICLE_NUMBER").HasMaxLength(14);
        }

        private void BuildPtlSettings(ModelBuilder builder)
        {
            var q = builder.Entity<PtlSettings>().ToTable("PTL_SETTINGS");
            q.HasKey(e => e.Key);
            q.Property(e => e.Key).HasColumnName("PRIMARY_KEY").HasMaxLength(10);
            q.Property(e => e.DaysToLookAhead).HasColumnName("DAYS_TO_LOOK_AHEAD");
            q.Property(e => e.BuildToMonthEndFromDays).HasColumnName("BUILD_TO_MONTH_END_FROM_DAYS");
            q.Property(e => e.FinalAssemblyDaysToLookAhead).HasColumnName("FA_DAYS_TO_LOOK_AHEAD");
            q.Property(e => e.SubAssemblyDaysToLookAhead).HasColumnName("SUBASSY_DAYS_TO_LOOK_AHEAD");
            q.Property(e => e.PriorityCutOffDays).HasColumnName("PRIORITY_CUT_OFF_DAYS");
            q.Property(e => e.PriorityStrategy).HasColumnName("PRIORITY_STRATEGY");
        }

        private void BuildBomDetailPhantomView(ModelBuilder builder)
        {
            builder.Query<BomDetailExplodedPhantomPartView>().ToView("BOM_DET_EXP_PHANTOM_PART_VIEW");
            builder.Query<BomDetailExplodedPhantomPartView>().Property(r => r.BomName).HasColumnName("BOM_NAME");
            builder.Query<BomDetailExplodedPhantomPartView>().Property(r => r.PartNumber).HasColumnName("PART_NUMBER");
            builder.Query<BomDetailExplodedPhantomPartView>().Property(r => r.Quantity).HasColumnName("QTY");
            builder.Query<BomDetailExplodedPhantomPartView>().Property(r => r.BomId).HasColumnName("BOM_ID");
            builder.Query<BomDetailExplodedPhantomPartView>().Property(r => r.BomType).HasColumnName("BOM_TYPE");
            builder.Query<BomDetailExplodedPhantomPartView>().Property(r => r.DecrementRule).HasColumnName("DECREMENT_RULE");
        }

        private void QueryPcasRevisions(ModelBuilder builder)
        {
            builder.Query<PcasRevision>().ToView("PCAS_REVISION_COMP_VIEW");
            builder.Query<PcasRevision>().Property(r => r.Cref).HasColumnName("CREF");
            builder.Query<PcasRevision>().Property(r => r.PartNumber).HasColumnName("PART_NUMBER");
            builder.Query<PcasRevision>().Property(r => r.PcasPartNumber).HasColumnName("PCAS_PART_NUMBER");
            builder.Query<PcasRevision>().Property(r => r.BoardCode).HasColumnName("BOARD_CODE");
        }

        private void BuildWorkOrders(ModelBuilder builder)
        {
            var q = builder.Entity<WorksOrder>().ToTable("WORKS_ORDERS");
            q.HasKey(e => e.OrderNumber);
            q.Property(e => e.BatchNumber).HasColumnName("BATCH_NUMBER");
            q.Property(e => e.CancelledBy).HasColumnName("CANCELLED_BY");
            q.Property(e => e.DateCancelled).HasColumnName("DATE_CANCELLED");
            q.Property(e => e.DateRaised).HasColumnName("DATE_RAISED");
            q.Property(e => e.KittedShort).HasColumnName("KITTED_SHORT").HasMaxLength(1);
            q.Property(e => e.LabelsPrinted).HasColumnName("LABELS_PRINTED");
            q.Property(e => e.OrderNumber).HasColumnName("ORDER_NUMBER");
            q.Property(e => e.PartNumber).HasColumnName("PART_NUMBER").HasMaxLength(14);
            q.Property(e => e.Quantity).HasColumnName("QTY");
            q.Property(e => e.QuantityOutstanding).HasColumnName("QTY_OUTSTANDING");
            q.Property(e => e.QuantityBuilt).HasColumnName("QTY_BUILT");
            q.Property(e => e.RaisedBy).HasColumnName("RAISED_BY");
            q.Property(e => e.RaisedByDepartment).HasColumnName("RAISED_BY_DEPT").HasMaxLength(10);
            q.Property(e => e.ReasonCancelled).HasColumnName("REASON_CANCELLED").HasMaxLength(200);
            q.Property(e => e.StartedByShift).HasColumnName("STARTED_BY_SHIFT").HasMaxLength(1);
            q.Property(e => e.DocType).HasColumnName("DOC_TYPE").HasMaxLength(6);
            q.Property(e => e.WorkStationCode).HasColumnName("WORK_STATION_CODE").HasMaxLength(16);
            q.Property(e => e.Outstanding).HasColumnName("OUTSTANDING").HasMaxLength(1);
            q.Property(e => e.ZoneName).HasColumnName("ZONE_NAME").HasMaxLength(20);
            q.Property(e => e.BatchNotes).HasColumnName("QUALITY_ISSUES");
            q.Property(e => e.SaveBatchNotes).HasColumnName("SAVE_BATCH_NOTES");
            q.HasOne<Part>(o => o.Part).WithMany(w => w.WorksOrders).HasForeignKey(o => o.PartNumber);
        }

        private void BuildWorksOrdersLabels(ModelBuilder builder)
        {
            var e = builder.Entity<WorksOrderLabel>().ToTable("WO_LABELS");
            e.HasKey(l => new { l.Sequence, l.PartNumber });
            e.Property(l => l.PartNumber).HasColumnName("PART_NUMBER");
            e.Property(l => l.LabelText).HasColumnName("LABEL_TEXT");
            e.Property(l => l.Sequence).HasColumnName("SEQ");
        }

        private void BuildAte(ModelBuilder builder)
        {
            builder.Entity<AteFaultCode>().ToTable("ATE_TEST_FAULT_CODES");
            builder.Entity<AteFaultCode>().HasKey(t => t.FaultCode);
            builder.Entity<AteFaultCode>().Property(t => t.FaultCode).HasColumnName("FAULT_CODE").HasMaxLength(10);
            builder.Entity<AteFaultCode>().Property(t => t.Description).HasColumnName("DESCRIPTION").HasMaxLength(50);
            builder.Entity<AteFaultCode>().Property(t => t.DateInvalid).HasColumnName("DATE_INVALID");
        }

        private void BuildAssemblyFails(ModelBuilder builder)
        {
            var e = builder.Entity<AssemblyFail>().ToTable("ASSEMBLY_FAILS");
            e.HasKey(f => f.Id);
            e.Property(f => f.Id).HasColumnName("ASSEMBLY_FAIL_ID");
            e.HasOne<WorksOrder>(f => f.WorksOrder).WithMany(o => o.AssemblyFails).HasForeignKey("WORKS_ORDER_NUMBER");
            e.HasOne<Employee>(f => f.EnteredBy).WithMany(m => m.AssemblyFailsEntered)
                .HasForeignKey("ENTERED_BY");
            e.HasOne<Employee>(f => f.ReturnedBy).WithMany(m => m.AssemblyFailsReturned)
                .HasForeignKey("RETURNED_BY");
            e.HasOne<Employee>(f => f.PersonResponsible).WithMany(m => m.AssemblyFailsResponsibleFor).HasForeignKey("PERSON_RESPONSIBLE");
            e.Property(f => f.NumberOfFails).HasColumnName("NUMBER_OF_FAILS");
            e.Property(f => f.InSlot).HasColumnName("IN_SLOT");
            e.Property(f => f.DateTimeFound).HasColumnName("DATE_TIME_FOUND");
            e.Property(f => f.SerialNumber).HasColumnName("SERIAL_NUMBER");
            e.Property(f => f.InSlot).HasColumnName("IN_SLOT");
            e.Property(f => f.Machine).HasColumnName("MACHINE");
            e.HasOne<Employee>(f => f.CompletedBy).WithMany(m => m.AssemblyFailsCompleted).HasForeignKey("COMPLETED_BY");
            e.Property(f => f.DateInvalid).HasColumnName("DATE_INVALID");
            e.Property(f => f.DateTimeFound).HasColumnName("DATE_TIME_FOUND");
            e.Property(f => f.ReportedFault).HasColumnName("REPORTED_FAULT");
            e.Property(f => f.BoardSerial).HasColumnName("BOARD_SERIAL_NUMBER");
            e.HasOne<Cit>(f => f.CitResponsible).WithMany(c => c.AssemblyFails).HasForeignKey("CIT_RESPONSIBLE");
            e.Property(f => f.Shift).HasColumnName("SHIFT");
            e.Property(f => f.Batch).HasColumnName("BATCH");
            e.Property(f => f.AoiEscape).HasColumnName("AOI_ESCAPE");
            e.Property(f => f.CircuitPart).HasColumnName("CIRCUIT_PART_NUMBER");
            e.Property(f => f.BoardPartNumber).HasColumnName("BOARD_PART_NUMBER");
            e.HasOne<Part>(f => f.BoardPart).WithMany(f => f.AssemblyFails).HasForeignKey(f => f.BoardPartNumber);
            e.Property(f => f.DateTimeComplete).HasColumnName("DATE_TIME_COMPLETE");
            e.Property(f => f.CaDate).HasColumnName("CA_DATE");
            e.Property(f => f.OutSlot).HasColumnName("OUT_SLOT");
            e.Property(f => f.CorrectiveAction).HasColumnName("CORRECTIVE_ACTION");
            e.Property(f => f.CircuitPartRef).HasColumnName("CIRCUIT_REF");
            e.HasOne<AssemblyFailFaultCode>(f => f.FaultCode).WithMany(c => c.AssemblyFails)
                .HasForeignKey("FAULT_CODE");
            e.Property(f => f.Analysis).HasColumnName("ANALYSIS");
            e.Property(f => f.EngineeringComments).HasColumnName("ENGINEERING_COMMENTS");
        }

        private void BuildBoardFailTypes(ModelBuilder builder)
        {
            builder.Entity<BoardFailType>().ToTable("BOARD_FAIL_TYPES");
            builder.Entity<BoardFailType>().HasKey(t => t.Type);
            builder.Entity<BoardFailType>().Property(t => t.Type).HasColumnName("FAIL_TYPE");
            builder.Entity<BoardFailType>().Property(t => t.Description).HasColumnName("FAIL_DESCRIPTION").HasMaxLength(300);
        }

        private void BuildTestMachines(ModelBuilder builder)
        {
            builder.Entity<TestMachine>().ToTable("TEST_MACHINES");
            builder.Entity<TestMachine>().HasKey(t => t.MachineCode);
            builder.Entity<TestMachine>().Property(t => t.MachineCode).HasColumnName("MACHINE_CODE");
            builder.Entity<TestMachine>().Property(t => t.Description).HasColumnName("DESCRIPTION");
        }

        private void BuildBoardTests(ModelBuilder builder)
        {
            builder.Entity<BoardTest>().ToTable("BOARD_TESTS");
            builder.Entity<BoardTest>().HasKey(t => new { t.BoardSerialNumber, t.Seq });
            builder.Entity<BoardTest>().Property(t => t.BoardName).HasColumnName("BOARD_NAME").HasMaxLength(14);
            builder.Entity<BoardTest>().Property(t => t.BoardSerialNumber).HasColumnName("BOARD_SN").HasMaxLength(20);
            builder.Entity<BoardTest>().Property(t => t.Seq).HasColumnName("SEQ");
            builder.Entity<BoardTest>().Property(t => t.DateTested).HasColumnName("DATE_TESTED");
            builder.Entity<BoardTest>().Property(t => t.TimeTested).HasColumnName("TIME_TESTED");
            builder.Entity<BoardTest>().Property(t => t.Status).HasColumnName("STATUS").HasMaxLength(10);
            builder.Entity<BoardTest>().Property(t => t.TestMachine).HasColumnName("TEST_MACHINE").HasMaxLength(30);
            builder.Entity<BoardTest>().HasOne<BoardFailType>(f => f.FailType).WithMany(o => o.BoardTests).HasForeignKey("FAIL_TYPE");
        }

        private void BuildWorkStations(ModelBuilder builder)
        {
            var e = builder.Entity<WorkStation>();
            e.ToTable("WORK_STATIONS");
            e.HasKey(w => w.WorkStationCode);
            e.Property(w => w.WorkStationCode).HasColumnName("WORK_STATION_CODE").HasMaxLength(16);
            e.Property(w => w.Description).HasColumnName("DESCRIPTION").HasMaxLength(50);
            e.Property(w => w.CitCode).HasColumnName("CIT_CODE").HasMaxLength(10);
            e.Property(w => w.VaxWorkStation).HasColumnName("VAX_WORK_STATION").HasMaxLength(8);
            e.Property(w => w.AlternativeWorkStationCode).HasColumnName("ALTERNATIVE_WORK_STATION_CODE").HasMaxLength(16);
            e.Property(w => w.ZoneType).HasColumnName("ZONE_TYPE").HasMaxLength(20);
        }

        private void BuildProductionTriggerLevels(ModelBuilder builder)
        {
            var e = builder.Entity<ProductionTriggerLevel>();
            e.ToTable("PRODUCTION_TRIGGER_LEVELS");
            e.HasKey(p => p.PartNumber);
            e.Property(p => p.PartNumber).HasColumnName("PART_NUMBER").HasMaxLength(14);
            e.Property(p => p.Description).HasColumnName("DESCRIPTION").HasMaxLength(100);
            e.Property(p => p.TriggerLevel).HasColumnName("TRIGGER_LEVEL");
            e.Property(p => p.KanbanSize).HasColumnName("KANBAN_SIZE");
            e.Property(p => p.MaximumKanbans).HasColumnName("MAXIMUM_KANBANS");
            e.Property(p => p.CitCode).HasColumnName("CIT_CODE").HasMaxLength(10);
            e.Property(p => p.BomLevel).HasColumnName("BOM_LEVEL");
            e.Property(p => p.WorkStationName).HasColumnName("WS_NAME").HasMaxLength(16);
            e.Property(p => p.FaZoneType).HasColumnName("FA_ZONE_TYPE").HasMaxLength(20);
            e.Property(p => p.VariableTriggerLevel).HasColumnName("VARIABLE_TRIGGER_LEVEL");
            e.Property(p => p.OverrideTriggerLevel).HasColumnName("OVERRIDE_TRIGGER_LEVEL");
            e.Property(p => p.Temporary).HasColumnName("TEMPORARY").HasMaxLength(1);
            e.Property(p => p.Story).HasColumnName("STORY").HasMaxLength(200);
            e.Property(p => p.EngineerId).HasColumnName("PRODUCTION_ENGINEER").HasMaxLength(6);
            e.Property(p => p.RouteCode).HasColumnName("MFG_ROUTE_CODE").HasMaxLength(20);
        }

        private void BuildPcasBoardsForAudit(ModelBuilder builder)
        {
            var e = builder.Entity<PcasBoardForAudit>();
            e.ToTable("PCAS_BOARDS_FOR_AUDIT");
            e.HasKey(p => p.BoardCode);
            e.Property(p => p.BoardCode).HasColumnName("BOARD_CODE").HasMaxLength(6);
            e.Property(p => p.DateAdded).HasColumnName("DATE_ADDED");
            e.Property(p => p.ForAudit).HasColumnName("FOR_AUDIT").HasMaxLength(1);
            e.Property(p => p.CutClinch).HasColumnName("CUT_CLINCH").HasMaxLength(1);
        }

        private void BuildLinnWeeks(ModelBuilder builder)
        {
            builder.Entity<LinnWeek>().ToTable("LINN_WEEKS");
            builder.Entity<LinnWeek>().HasKey(t => t.LinnWeekNumber);
            builder.Entity<LinnWeek>().Property(t => t.LinnWeekNumber).HasColumnName("LINN_WEEK_NUMBER");
            builder.Entity<LinnWeek>().Property(t => t.StartDate).HasColumnName("LINN_WEEK_START_DATE");
            builder.Entity<LinnWeek>().Property(t => t.EndDate).HasColumnName("LINN_WEEK_END_DATE");
            builder.Entity<LinnWeek>().Property(t => t.WWYYYY).HasColumnName("WWYYYY").HasMaxLength(8);
            builder.Entity<LinnWeek>().Property(t => t.WWSYY).HasColumnName("WWSYY").HasMaxLength(5);
            builder.Entity<LinnWeek>().Property(t => t.WeekEndingDDMON).HasColumnName("WEEK_ENDING_DDMON").HasMaxLength(5);
            builder.Entity<LinnWeek>().Property(t => t.LinnMonthEndWeekNumber).HasColumnName("LINN_MONTH_END_WEEK_NUMBER");
        }

        private void QueryWhoBuildWhat(ModelBuilder builder)
        {
            builder.Query<WhoBuiltWhat>().ToView("V_WHO_BUILT_WHAT");
            builder.Query<WhoBuiltWhat>().Property(v => v.CitCode).HasColumnName("CIT_CODE");
            builder.Query<WhoBuiltWhat>().Property(t => t.CitName).HasColumnName("CIT_NAME");
            builder.Query<WhoBuiltWhat>().Property(t => t.SernosDate).HasColumnName("SERNOS_DATE");
            builder.Query<WhoBuiltWhat>().Property(t => t.ArticleNumber).HasColumnName("ARTICLE_NUMBER");
            builder.Query<WhoBuiltWhat>().Property(t => t.CreatedBy).HasColumnName("CREATED_BY");
            builder.Query<WhoBuiltWhat>().Property(t => t.UserName).HasColumnName("USER_NAME");
            builder.Query<WhoBuiltWhat>().Property(t => t.QtyBuilt).HasColumnName("QTY_BUILT");
            builder.Query<WhoBuiltWhat>().Property(t => t.SernosNumber).HasColumnName("SERNOS_NUMBER");
            builder.Query<WhoBuiltWhat>().Property(t => t.DocumentNumber).HasColumnName("DOCUMENT_NUMBER");
        }

        private void QueryFailedParts(ModelBuilder builder)
        {
            builder.Query<FailedParts>().ToView("FAILED_PARTS_REPORT_VIEW");
            builder.Query<FailedParts>().Property(t => t.PartNumber).HasColumnName("PART_NUMBER");
            builder.Query<FailedParts>().Property(t => t.PartDescription).HasColumnName("DESCRIPTION");
            builder.Query<FailedParts>().Property(t => t.Qty).HasColumnName("QTY");
            builder.Query<FailedParts>().Property(t => t.BatchRef).HasColumnName("BATCH_REF");
            builder.Query<FailedParts>().Property(t => t.DateBooked).HasColumnName("DATE_BOOKED");
            builder.Query<FailedParts>().Property(t => t.StockRotationDate).HasColumnName("STOCK_ROTATION_DATE");
            builder.Query<FailedParts>().Property(v => v.TotalValue).HasColumnName("TOTAL_VALUE");
            builder.Query<FailedParts>().Property(v => v.StockLocatorRemarks).HasColumnName("STOCK_LOCATOR_REMARKS");
            builder.Query<FailedParts>().Property(v => v.CreatedBy).HasColumnName("CREATED_BY");
            builder.Query<FailedParts>().Property(v => v.CitCode).HasColumnName("CIT_CODE");
            builder.Query<FailedParts>().Property(t => t.CitName).HasColumnName("CIT_NAME");
            builder.Query<FailedParts>().Property(t => t.StoragePlace).HasColumnName("STORAGE_PLACE");
            builder.Query<FailedParts>().Property(t => t.PreferredSupplierId).HasColumnName("PREFERRED_SUPPLIER");
            builder.Query<FailedParts>().Property(t => t.SupplierName).HasColumnName("SUPPLIER_NAME");
        }

        private void QueryDaysRequired(ModelBuilder builder)
        {
            builder.Query<ProductionDaysRequired>().ToView("OSR_DAYS_REQUIRED_VIEW");
            builder.Query<ProductionDaysRequired>().Property(t => t.Priority).HasColumnName("PRIORITY");
            builder.Query<ProductionDaysRequired>().Property(t => t.JobRef).HasColumnName("JOBREF");
            builder.Query<ProductionDaysRequired>().Property(t => t.CitCode).HasColumnName("CIT_CODE");
            builder.Query<ProductionDaysRequired>().Property(t => t.PartNumber).HasColumnName("PART_NUMBER");
            builder.Query<ProductionDaysRequired>().Property(t => t.PartDescription).HasColumnName("DESCRIPTION");
            builder.Query<ProductionDaysRequired>().Property(t => t.QtyBeingBuilt).HasColumnName("QTY_BEING_BUILT");
            builder.Query<ProductionDaysRequired>().Property(t => t.BuildQty).HasColumnName("BUILD");
            builder.Query<ProductionDaysRequired>().Property(t => t.CanBuild).HasColumnName("CAN_BUILD");
            builder.Query<ProductionDaysRequired>().Property(v => v.CanBuildExcludingSubAssemblies).HasColumnName("CAN_BUILD_EX_SUB_ASSEMBLIES");
            builder.Query<ProductionDaysRequired>().Property(v => v.EffectiveKanbanSize).HasColumnName("EFFECTIVE_KANBAN_SIZE");
            builder.Query<ProductionDaysRequired>().Property(v => v.QtyBeingBuiltDays).HasColumnName("QTY_BEING_BUILT_DAYS");
            builder.Query<ProductionDaysRequired>().Property(v => v.CanBuildDays).HasColumnName("CAN_BUILD_DAYS");
            builder.Query<ProductionDaysRequired>().Property(t => t.BuildExcludingSubAssembliesDays).HasColumnName("EX_SUB_ASSEMBLIES_DAYS");
            builder.Query<ProductionDaysRequired>().Property(t => t.EarliestRequestedDate).HasColumnName("EARLIEST_REQUESTED_DATE");
            builder.Query<ProductionDaysRequired>().Property(t => t.SortOrder).HasColumnName("SORT_ORDER");
            builder.Query<ProductionDaysRequired>().Property(t => t.MfgRouteCode).HasColumnName("MFG_ROUTE_CODE");
            builder.Query<ProductionDaysRequired>().Property(t => t.DaysToBuildKanban).HasColumnName("DAYS_TO_BUILD_KANBAN");
            builder.Query<ProductionDaysRequired>().Property(t => t.DaysToSetUpKanban).HasColumnName("DAYS_TO_SETUP_KANBAN");
            builder.Query<ProductionDaysRequired>().Property(t => t.RecommendedBuildQty).HasColumnName("BT");
            builder.Query<ProductionDaysRequired>().Property(t => t.RecommendedBuildQtyDays).HasColumnName("BT_DAYS");
            builder.Query<ProductionDaysRequired>().Property(t => t.FixedBuild).HasColumnName("FIXED_BUILD");
            builder.Query<ProductionDaysRequired>().Property(t => t.FixedBuildDays).HasColumnName("FIXED_BUILD_DAYS");
            builder.Query<ProductionDaysRequired>().Property(t => t.BuildDays).HasColumnName("BUILD_DAYS");
        }

        private void QueryMCDLines(ModelBuilder builder)
        {
            builder.Query<MCDLine>().ToView("MCD_VIEW");
            builder.Query<MCDLine>().Property(v => v.OrderNumber).HasColumnName("ORDER_NUMBER");
            builder.Query<MCDLine>().Property(t => t.OrderLine).HasColumnName("ORDER_LINE");
            builder.Query<MCDLine>().Property(t => t.OrderDate).HasColumnName("ORDER_DATE");
            builder.Query<MCDLine>().Property(t => t.RequestedDeliveryDate).HasColumnName("REQUESTED_DELIVERY_DATE");
            builder.Query<MCDLine>().Property(t => t.CoreType).HasColumnName("CORE_TYPE");
            builder.Query<MCDLine>().Property(t => t.ArticleNumber).HasColumnName("ARTICLE_NUMBER");
            builder.Query<MCDLine>().Property(t => t.QtyAllocated).HasColumnName("QTY_ALLOCATED");
            builder.Query<MCDLine>().Property(t => t.QtyOrdered).HasColumnName("QTY_ORDERED");
            builder.Query<MCDLine>().Property(t => t.QtyOutstanding).HasColumnName("QTY_OUTSTANDING");
            builder.Query<MCDLine>().Property(t => t.Invoiced).HasColumnName("INV");
            builder.Query<MCDLine>().Property(t => t.MCDDate).HasColumnName("MCD_DATE");
            builder.Query<MCDLine>().Property(t => t.Status).HasColumnName("STATUS");
            builder.Query<MCDLine>().Property(t => t.OrderLineCompleted).HasColumnName("COMPLETE");
            builder.Query<MCDLine>().Property(t => t.Reason).HasColumnName("REASON");
            builder.Query<MCDLine>().Property(t => t.CouldGo).HasColumnName("COULD_GO");
        }

        private void QueryEmployeeDepartmentView(ModelBuilder builder)
        {
            var q = builder.Query<EmployeeDepartmentView>();
            q.ToView("EMP_DEPT_VIEW");
            q.Property(t => t.UserNumber).HasColumnName("USER_NUMBER");
            q.Property(t => t.DepartmentCode).HasColumnName("DEPARTMENT_CODE");
        }

        private void QueryPartFailSuppliersView(ModelBuilder builder)
        {
            var q = builder.Query<PartFailSupplierView>();
            q.ToView("V_PART_FAIL_SUPPLIERS");
            q.Property(t => t.SupplierId).HasColumnName("SUPPLIER_ID");
            q.Property(t => t.SupplierName).HasColumnName("SUPPLIER_NAME");
        }

        private void QueryProductionBackOrdersView(ModelBuilder builder)
        {
            builder.Query<ProductionBackOrdersView>().ToView("PRODUCTION_BACK_ORDERS_VIEW");
            builder.Query<ProductionBackOrdersView>().Property(p => p.CitCode).HasColumnName("CIT_CODE");
            builder.Query<ProductionBackOrdersView>().Property(p => p.JobId).HasColumnName("JOB_ID");
            builder.Query<ProductionBackOrdersView>().Property(p => p.ArticleNumber).HasColumnName("ARTICLE_NUMBER");
            builder.Query<ProductionBackOrdersView>().Property(p => p.InvoiceDescription).HasColumnName("INVOICE_DESCRIPTION");
            builder.Query<ProductionBackOrdersView>().Property(p => p.OrderQuantity).HasColumnName("ORDER_QTY");
            builder.Query<ProductionBackOrdersView>().Property(p => p.OrderValue).HasColumnName("ORDER_VALUE");
            builder.Query<ProductionBackOrdersView>().Property(p => p.OldestDate).HasColumnName("OLDEST_DATE");
            builder.Query<ProductionBackOrdersView>().Property(p => p.CanBuildQuantity).HasColumnName("CAN_BUILD_QTY");
            builder.Query<ProductionBackOrdersView>().Property(p => p.CanBuildValue).HasColumnName("CAN_BUILD_VALUE");
        }

        private void QueryOverdueOrderLines(ModelBuilder builder)
        {
            var q = builder.Query<OverdueOrderLine>();
            q.ToView("V_OVERDUE_ORDERS_REPORT");
            q.Property(t => t.JobId).HasColumnName("JOB_ID");
            q.Property(t => t.AccountId).HasColumnName("ACCOUNT_ID");
            q.Property(t => t.OutletNumber).HasColumnName("OUTLET_NUMBER");
            q.Property(t => t.OutletName).HasColumnName("OUTLET_NAME");
            q.Property(t => t.OrderNumber).HasColumnName("ORDER_NUMBER");
            q.Property(t => t.OrderLine).HasColumnName("ORDER_LINE");
            q.Property(t => t.OrderRef).HasColumnName("ORDER_REF");
            q.Property(t => t.ArticleNumber).HasColumnName("ARTICLE_NUMBER");
            q.Property(t => t.InvoiceDescription).HasColumnName("INVOICE_DESCRIPTION");
            q.Property(t => t.RequestedDeliveryDate).HasColumnName("REQUESTED_DELIVERY_DATE");
            q.Property(t => t.FirstAdvisedDespatchDate).HasColumnName("FIRST_ADVISED_DESPATCH_DATE");
            q.Property(t => t.NoStockQuantity).HasColumnName("NO_STOCK_QTY");
            q.Property(t => t.AllocVal).HasColumnName("ALLOC_VAL");
            q.Property(t => t.RequiredNowStockDespatchableValue).HasColumnName("RN_S_D_VAL");
            q.Property(t => t.RequiredNowStockNotDespatchableValue).HasColumnName("RN_S_ND_VAL");
            q.Property(t => t.RequiredNowNoStockValue).HasColumnName("RN_NS_VAL");
            q.Property(t => t.RequiredThisMonthStockValue).HasColumnName("RTM_S_VAL");
            q.Property(t => t.RequiredThisMonthNoStockValue).HasColumnName("RTM_NS_VAL");
            q.Property(t => t.RequiredNextMonthStockValue).HasColumnName("RNM_S_VAL");
            q.Property(t => t.RequiredNextMonthNoStockValue).HasColumnName("RNM_NS_VAL");
            q.Property(t => t.Reasons).HasColumnName("REASONS");
            q.Property(t => t.Quantity).HasColumnName("QTY");
            q.Property(t => t.DaysLate).HasColumnName("DAYS_LATE");
            q.Property(t => t.DaysLateFa).HasColumnName("DAYS_LATE_FA");
            q.Property(t => t.WorkingDaysLate).HasColumnName("WORKING_DAYS_LATE");
            q.Property(t => t.WorkingDaysLateFa).HasColumnName("WORKING_DAYS_LATE_FA");
            q.Property(t => t.BaseValue).HasColumnName("BASE_VALUE");
            q.Property(t => t.OrderValue).HasColumnName("ORDER_VALUE");
        }

        private void BuildSerialNumberReissues(ModelBuilder builder)
        {
            builder.Entity<SerialNumberReissue>().ToTable("SERNOS_RENUM");
            builder.Entity<SerialNumberReissue>().HasKey(s => s.Id);
            builder.Entity<SerialNumberReissue>().Property(s => s.Id).HasColumnName("SNRENUM_ID");
            builder.Entity<SerialNumberReissue>().Property(s => s.SernosGroup).HasColumnName("SERNOS_GROUP").HasMaxLength(10);
            builder.Entity<SerialNumberReissue>().Property(s => s.SerialNumber).HasColumnName("SERNOS_NUMBER");
            builder.Entity<SerialNumberReissue>().Property(s => s.NewSerialNumber).HasColumnName("NEW_SERNOS_NUMBER");
            builder.Entity<SerialNumberReissue>().Property(s => s.Comments).HasColumnName("COMMENTS").HasMaxLength(200);
            builder.Entity<SerialNumberReissue>().Property(s => s.CreatedBy).HasColumnName("CREATED_BY");
            builder.Entity<SerialNumberReissue>().Property(s => s.ArticleNumber).HasColumnName("ARTICLE_NUMBER").HasMaxLength(14);
            builder.Entity<SerialNumberReissue>().Property(s => s.NewArticleNumber).HasColumnName("NEW_ARTICLE_NUMBER").HasMaxLength(14);
        }

        private void BuildDepartments(ModelBuilder builder)
        {
            var e = builder.Entity<Department>();
            e.ToTable("LINN_DEPARTMENTS");
            e.HasKey(d => d.DepartmentCode);
            e.Property(d => d.DepartmentCode).HasColumnName("DEPARTMENT_CODE").HasMaxLength(10);
            e.Property(d => d.Description).HasColumnName("DESCRIPTION").HasMaxLength(50);
            e.Property(d => d.PersonnelDepartment).HasColumnName("PERSONNEL_DEPARTMENT").HasMaxLength(1);
        }

        private void BuildBuilds(ModelBuilder builder)
        {
            var e = builder.Query<Build>();
            e.ToView("V_BUILDS");
            e.Property(b => b.Tref).HasColumnName("TREF");
            e.Property(b => b.PartNumber).HasColumnName("PART_NUMBER");
            e.Property(b => b.BuildDate).HasColumnName("BU_DATE");
            e.Property(b => b.LabourPrice).HasColumnName("LABOUR_PRICE");
            e.Property(b => b.MaterialPrice).HasColumnName("MATERIAL_PRICE");
            e.Property(b => b.Quantity).HasColumnName("QUANTITY");
            e.Property(b => b.DepartmentCode).HasColumnName("CR_DEPT");
        }

        private void BuildCits(ModelBuilder builder)
        {
            var e = builder.Entity<Cit>();
            e.ToTable("CITS");
            e.HasKey(c => c.Code);
            e.Property(c => c.Code).HasColumnName("CODE").HasMaxLength(10);
            e.Property(c => c.Name).HasColumnName("NAME").HasMaxLength(50);
            e.Property(c => c.BuildGroup).HasColumnName("BUILD_GROUP").HasMaxLength(2);
            e.Property(c => c.DateInvalid).HasColumnName("DATE_INVALID");
            e.Property(c => c.SortOrder).HasColumnName("SORT_ORDER");
            e.Property(c => c.DepartmentCode).HasColumnName("DEPARTMENT_CODE").HasMaxLength(10);
        }

        private void BuildProductionMeasures(ModelBuilder builder)
        {
            var e = builder.Entity<ProductionMeasures>();
            e.ToTable("PM_WORK");
            e.HasKey(d => d.CitCode);
            e.Property(d => d.CitCode).HasColumnName("CIT_CODE").HasMaxLength(10);
            e.Property(d => d.BuiltThisWeekValue).HasColumnName("BUILT_THIS_WEEK_VALUE");
            e.Property(d => d.BuiltThisWeekQty).HasColumnName("BUILT_THIS_WEEK_QTY");
            e.Property(d => d.BackOrderValue).HasColumnName("BACK_ORDER_VALUE");
            e.Property(d => d.FFlaggedValue).HasColumnName("FFLAGGED_VALUE");
            e.Property(d => d.FFlaggedQty).HasColumnName("FFLAGGED_QTY");
            e.Property(d => d.StockValue).HasColumnName("STOCK_VALUE");
            e.Property(d => d.OverStockValue).HasColumnName("OVERSTOCK_VALUE");
            e.Property(d => d.NumberOfBackOrders).HasColumnName("NUMBER_OF_BACK_ORDERS");
            e.Property(d => d.OldestBackOrder).HasColumnName("OLDEST_BACK_ORDER");
            e.Property(d => d.PtlJobref).HasColumnName("PTL_JOBREF").HasMaxLength(6);
            e.Property(d => d.PboJobref).HasColumnName("PBO_JOBREF").HasMaxLength(6);
            e.Property(d => d.DaysRequired).HasColumnName("DAYS_REQUIRED");
            e.Property(d => d.DaysRequired3).HasColumnName("DAYS_REQUIRED_3");
            e.Property(d => d.DaysRequiredCanDo12).HasColumnName("DAYS_REQUIRED_CAN_DO_12");
            e.Property(d => d.DaysRequiredCanDo3).HasColumnName("DAYS_REQUIRED_CAN_DO_3");
            e.Property(d => d.PboJobId).HasColumnName("PBO_JOB_ID");
            e.Property(d => d.NumberOfBackOrders).HasColumnName("NUMBER_OF_BACK_ORDERS");
            e.Property(d => d.NumberOfPartsBackOrdered).HasColumnName("NUMBER_OF_PARTS_BACK_ORDERED");
            e.Property(d => d.OldestBackOrder).HasColumnName("OLDEST_BACK_ORDER");
            e.Property(d => d.UsageValue).HasColumnName("USAGE_VALUE");
            e.Property(d => d.UsageForTotalValue).HasColumnName("USAGE_FOR_TOTAL_VALUE");
            e.Property(d => d.AvgStockValue).HasColumnName("AVG_STOCK_VALUE");
            e.Property(d => d.ShortBat).HasColumnName("SHORT_BAT");
            e.Property(d => d.ShortMetalwork).HasColumnName("SHORT_CMILK");
            e.Property(d => d.ShortProc).HasColumnName("SHORT_PROC");
            e.Property(d => d.ShortAny).HasColumnName("SHORT_ANY");
            e.Property(d => d.DeliveryPerformance1s).HasColumnName("DELIVERY_PERFORMANCE_1S");
            e.Property(d => d.DeliveryPerformance2s).HasColumnName("DELIVERY_PERFORMANCE_2S");
            e.Property(d => d.Ones).HasColumnName("ONES");
            e.Property(d => d.Twos).HasColumnName("TWOS");
            e.Property(d => d.Threes).HasColumnName("THREES");
            e.Property(d => d.Fours).HasColumnName("FOURS");
            e.Property(d => d.Fives).HasColumnName("FIVES");
            e.HasOne<Cit>(d => d.Cit).WithOne(c => c.Measures);
        }

        private void BuildManufacturingSkills(ModelBuilder builder)
        {
            var e = builder.Entity<ManufacturingSkill>();
            e.ToTable("MFG_SKILLS");
            e.HasKey(s => s.SkillCode);
            e.Property(s => s.SkillCode).HasColumnName("MFG_SKILL_CODE").HasMaxLength(10);
            e.Property(s => s.Description).HasColumnName("DESCRIPTION").HasMaxLength(50);
            e.Property(s => s.HourlyRate).HasColumnName("HOURLY_RATE");
        }

        private void BuildManufacturingResources(ModelBuilder builder)
        {
            var e = builder.Entity<ManufacturingResource>();
            e.ToTable("MFG_RESOURCES");
            e.HasKey(c => c.ResourceCode);
            e.Property(c => c.ResourceCode).HasColumnName("MFG_RESOURCE_CODE").HasMaxLength(10);
            e.Property(c => c.Description).HasColumnName("DESCRIPTION").HasMaxLength(50);
            e.Property(c => c.Cost).HasColumnName("COST_POUNDS_PER_HOUR").HasMaxLength(14);
        }

        private void BuildParts(ModelBuilder builder)
        {
            var e = builder.Entity<Part>();
            e.ToTable("PARTS");
            e.HasKey(p => p.PartNumber);
            e.Property(p => p.PartNumber).HasColumnName("PART_NUMBER").HasMaxLength(14);
            e.Property(p => p.Description).HasColumnName("DESCRIPTION").HasMaxLength(200);
            e.Property(p => p.DecrementRule).HasColumnName("DECREMENT_RULE").HasMaxLength(10);
            e.Property(p => p.BomType).HasColumnName("BOM_TYPE").HasMaxLength(1);
            e.Property(p => p.BomId).HasColumnName("BOM_ID");
            e.Property(p => p.SernosSequence).HasColumnName("SERNOS_SEQUENCE").HasMaxLength(10);
            e.Property(p => p.AccountingCompany).HasColumnName("ACCOUNTING_COMPANY").HasMaxLength(10);
            e.Property(p => p.BaseUnitPrice).HasColumnName("BASE_UNIT_PRICE");
            e.Property(p => p.PreferredSupplier).HasColumnName("PREFERRED_SUPPLIER");
        }

        private void BuildAssemblyFailFaultCodes(ModelBuilder builder)
        {
            var e = builder.Entity<AssemblyFailFaultCode>();
            e.ToTable("ASSEMBLY_FAIL_FAULT_CODES");
            e.HasKey(c => c.FaultCode);
            e.Property(c => c.FaultCode).HasColumnName("FAULT_CODE").HasMaxLength(10);
            e.Property(c => c.Description).HasColumnName("DESCRIPTION").HasMaxLength(50);
            e.Property(c => c.DateInvalid).HasColumnName("DATE_INVALID");
            e.Property(c => c.Explanation).HasColumnName("EXPLANATION").HasMaxLength(2000);
        }

        private void BuildEmployees(ModelBuilder builder)
        {
            var q = builder.Entity<Employee>();
            q.HasKey(e => e.Id);
            q.ToTable("AUTH_USER_NAME_VIEW");
            q.Property(e => e.Id).HasColumnName("USER_NUMBER");
            q.Property(e => e.FullName).HasColumnName("USER_NAME");
            q.Property(e => e.DateInvalid).HasColumnName("DATE_INVALID");
        }

        private void QueryPtlMaster(ModelBuilder builder)
        {
            var q = builder.Query<PtlMaster>();
            q.ToView("PTL_MASTER");
            q.Property(e => e.LastFullRunJobref).HasColumnName("LAST_FULL_RUN_JOBREF").HasMaxLength(6);
            q.Property(e => e.LastFullRunDateTime).HasColumnName("LAST_FULL_RUN_DATE");
            q.Property(e => e.LastPtlShortageJobref).HasColumnName("LAST_PTL_SHORTAGE_JOBREF").HasMaxLength(6);
            q.Property(e => e.LastDaysToLookAhead).HasColumnName("LAST_DAYS_TO_LOOK_AHEAD");
            q.Property(e => e.Status).HasColumnName("STATUS").HasMaxLength(2000);
        }

        private void QueryOsrRunMaster(ModelBuilder builder)
        {
            var q = builder.Query<OsrRunMaster>();
            q.ToView("OSR_RUN_MASTER");
            q.Property(e => e.LastTriggerJobref).HasColumnName("LAST_TRIGGER_JOBREF").HasMaxLength(6);
            q.Property(e => e.LastTriggerRunDateTime).HasColumnName("LAST_TRIGGER_RUNDATE");
            q.Property(e => e.RunDateTime).HasColumnName("RUN_DATETIME");
        }

        private void BuildManufacturingRoutes(ModelBuilder builder)
        {
            var e = builder.Entity<ManufacturingRoute>();
            e.ToTable("MFG_ROUTES");
            e.HasKey(s => s.RouteCode);
            e.Property(s => s.RouteCode).HasColumnName("MFG_ROUTE_CODE").HasMaxLength(10);
            e.Property(s => s.Description).HasColumnName("DESCRIPTION").HasMaxLength(50);
            e.Property(s => s.Notes).HasColumnName("NOTES");
            builder.Entity<ManufacturingRoute>().HasMany(t => t.Operations).WithOne(x => x.ManufacturingRoute);
        }

        private void BuildManufacturingOperations(ModelBuilder builder)
        {
            var e = builder.Entity<ManufacturingOperation>();
            e.ToTable("MFG_OPERATIONS");
            builder.Entity<ManufacturingOperation>().HasKey(d => new { d.ManufacturingId, d.RouteCode });
            e.Property(s => s.RouteCode).HasColumnName("MFG_ROUTE_CODE").HasMaxLength(20);
            e.Property(s => s.ManufacturingId).HasColumnName("MFG_ID").HasMaxLength(8);
            e.Property(s => s.OperationNumber).HasColumnName("OPERATION_NUMBER").HasMaxLength(38);
            e.Property(s => s.Description).HasColumnName("DESCRIPTION").HasMaxLength(50);
            e.Property(s => s.SkillCode).HasColumnName("MFG_SKILL_CODE").HasMaxLength(10);
            e.Property(s => s.ResourceCode).HasColumnName("MFG_RESOURCE_CODE").HasMaxLength(10);
            e.Property(s => s.SetAndCleanTime).HasColumnName("SET_AND_CLEAN_TIME_MINS").HasMaxLength(7);
            e.Property(s => s.CycleTime).HasColumnName("CYCLE_TIME_MINS").HasMaxLength(7);
            e.Property(s => s.LabourPercentage).HasColumnName("LABOUR_PERCENTAGE").HasMaxLength(38);
            e.Property(s => s.CITCode).HasColumnName("CIT_CODE").HasMaxLength(10);
            e.HasOne<ManufacturingRoute>(s => s.ManufacturingRoute).WithMany(g => g.Operations)
                .HasForeignKey(s => s.RouteCode);
        }

        private void QueryProductionTriggers(ModelBuilder builder)
        {
            var q = builder.Query<ProductionTrigger>();
            q.ToView("V_PRODUCTION_TRIGGERS_EF");
            q.Property(e => e.Jobref).HasColumnName("JOBREF").HasMaxLength(6);
            q.Property(e => e.PartNumber).HasColumnName("PART_NUMBER").HasMaxLength(14);
            q.Property(e => e.Description).HasColumnName("DESCRIPTION").HasMaxLength(100);
            q.Property(e => e.Citcode).HasColumnName("CIT_CODE").HasMaxLength(10);
            q.Property(e => e.CitName).HasColumnName("CIT_NAME").HasMaxLength(50);
            q.Property(e => e.TriggerLevel).HasColumnName("TRIGGER_LEVEL");
            q.Property(e => e.OverrideTriggerLevel).HasColumnName("OVERRIDE_TRIGGER_LEVEL");
            q.Property(e => e.VariableTriggerLevel).HasColumnName("VARIABLE_TRIGGER_LEVEL");
            q.Property(e => e.EffectiveTriggerLevel).HasColumnName("EFFECTIVE_TRIGGER_LEVEL");
            q.Property(e => e.TriggerLevelText).HasColumnName("TRIGGER_LEVEL_TEXT").HasMaxLength(40);
            q.Property(e => e.KanbanSize).HasColumnName("KANBAN_SIZE");
            q.Property(e => e.EffectiveKanbanSize).HasColumnName("EFFECTIVE_KANBAN_SIZE");
            q.Property(e => e.MaximumKanbans).HasColumnName("MAXIMUM_KANBANS");
            q.Property(e => e.MfgRouteCode).HasColumnName("MFG_ROUTE_CODE").HasMaxLength(20);
            q.Property(e => e.DaysToBuildKanban).HasColumnName("DAYS_TO_BUILD_KANBAN");
            q.Property(e => e.NettSalesOrders).HasColumnName("NETT_SALES_ORDERS");
            q.Property(e => e.QtyFree).HasColumnName("QTY_FREE");
            q.Property(e => e.RemainingBuild).HasColumnName("REMAINING_BUILD");
            q.Property(e => e.QtyBeingBuilt).HasColumnName("QTY_BEING_BUILT");
            q.Property(e => e.ReqtForSalesOrdersBE).HasColumnName("BE");
            q.Property(e => e.ReqtForInternalCustomersBI).HasColumnName("BI");
            q.Property(e => e.ReqtForInternalAndTriggerLevelBT).HasColumnName("BT");
            q.Property(e => e.ReqtForSalesOrdersGBE).HasColumnName("GBE");
            q.Property(e => e.ReqtForInternalCustomersGBI).HasColumnName("GBI");
            q.Property(e => e.FixedBuild).HasColumnName("FIXED_BUILD");
            q.Property(e => e.Priority).HasColumnName("PRIORITY").HasMaxLength(1);
            q.Property(e => e.ReqtFromFixedBuild).HasColumnName("REQT_FROM_FIXED_BUILD").HasMaxLength(1);
            q.Property(e => e.DaysTriggerLasts).HasColumnName("LDAYS");
            q.Property(e => e.Story).HasColumnName("STORY").HasMaxLength(200);
            q.Property(e => e.OnHold).HasColumnName("ON_HOLD").HasMaxLength(9);
            q.Property(e => e.ReasonStarted).HasColumnName("REASON_STARTED").HasMaxLength(2000);
            q.Property(e => e.SortOrder).HasColumnName("SORT_ORDER");
            q.Property(e => e.ShortNowBackOrdered).HasColumnName("SNBO");
            q.Property(e => e.ShortNowMonthEnd).HasColumnName("SNME");
            q.Property(e => e.QtyBeingBuiltDays).HasColumnName("QTY_BEING_BUILT_DAYS");
            q.Property(e => e.ReqtForSalesOrdersBEDays).HasColumnName("BE_DAYS");
            q.Property(e => e.ReqtForInternalCustomersBIDays).HasColumnName("BI_DAYS");
            q.Property(e => e.ReqtForInternalTriggerBTDays).HasColumnName("BT_DAYS");
            q.Property(e => e.FixedBuildDays).HasColumnName("FIXED_BUILD_DAYS");
            q.Property(e => e.QtyNFlagged).HasColumnName("QTY_N_FLAGGED");
            q.Property(e => e.QtyFFlagged).HasColumnName("QTY_F_FLAGGED");
            q.Property(e => e.QtyYFlagged).HasColumnName("QTY_Y_FLAGGED");
            q.Property(e => e.EarliestRequestedDate).HasColumnName("EARLIEST_REQUESTED_DATE");
            q.Property(e => e.StockReqtPercNt).HasColumnName("STOCK_REQT_PCNT");
            q.Property(e => e.CanBuild).HasColumnName("CAN_BUILD");
            q.Property(e => e.QtyManualWo).HasColumnName("QTY_MANUAL_WO");
            q.Property(e => e.StockAvailableShortNowBackOrdered).HasColumnName("SA_SNBO");
            q.Property(e => e.MWPriority).HasColumnName("MW_PRIORITY");
            q.Property(e => e.CanBuildExSubAssemblies).HasColumnName("CAN_BUILD_EX_SUB_ASSEMBLIES");
            q.Property(e => e.ReportType).HasColumnName("REPORT_TYPE").HasMaxLength(5);
        }

        private void QuerySmtShifts(ModelBuilder builder)
        {
            var q = builder.Query<SmtShift>();
            q.ToView("SMT_SHIFTS");
            q.Property(e => e.Shift).HasColumnName("SHIFT");
            q.Property(e => e.Description).HasColumnName("DESCRIPTION");
        }

        private void QueryBuildPlanDetailsReportLines(ModelBuilder builder)
        {
            var q = builder.Query<BuildPlanDetailsReportLine>();
            q.ToView("V_BUILD_PLAN_REPORT");
            q.Property(e => e.SortOrder).HasColumnName("SORT_ORDER");
            q.Property(e => e.PartNumber).HasColumnName("PART_NUMBER");
            q.Property(e => e.CitName).HasColumnName("CIT_NAME");
            q.Property(e => e.LinnWeekNumber).HasColumnName("LINN_WEEK_NUMBER");
            q.Property(e => e.LinnWeek).HasColumnName("LINN_WEEK");
            q.Property(e => e.DDMon).HasColumnName("DDMON");
            q.Property(e => e.FixedBuild).HasColumnName("FIXED_BUILD");
            q.Property(e => e.BuildPlanName).HasColumnName("BUILD_PLAN_NAME");
        }

        private void QueryBuildPlanDetails(ModelBuilder builder)
        {
            var e = builder.Entity<BuildPlanDetail>();
            e.ToTable("BUILD_PLAN_DETAILS");
            e.HasKey(b => new { b.BuildPlanName, b.PartNumber, b.FromLinnWeekNumber });
            e.Property(b => b.BuildPlanName).HasColumnName("BUILD_PLAN_NAME");
            e.Property(b => b.PartNumber).HasColumnName("PART_NUMBER");
            e.Property(b => b.FromLinnWeekNumber).HasColumnName("FROM_LINN_WEEK_NUMBER");
            e.Property(b => b.ToLinnWeekNumber).HasColumnName("TO_LINN_WEEK_NUMBER");
            e.Property(b => b.RuleCode).HasColumnName("RULE_CODE");
            e.Property(b => b.Quantity).HasColumnName("QUANTITY");
        }

        private void QueryBuildPlanRules(ModelBuilder builder)
        {
            var q = builder.Query<BuildPlanRule>();
            q.ToView("BUILD_PLAN_RULES");
            q.Property(e => e.RuleCode).HasColumnName("RULE_CODE");
            q.Property(e => e.Description).HasColumnName("DESCRIPTION");
        }

        private void QueryStoragePlaces(ModelBuilder builder)
        {
            var q = builder.Query<StoragePlace>();
            q.ToView("V_STORAGE_PLACES");
            q.Property(p => p.LocationId).HasColumnName("LOCATION_ID");
            q.Property(p => p.StoragePlaceId).HasColumnName("STORAGE_PLACE");
            q.Property(p => p.Description).HasColumnName("STORAGE_PLACE_DESCRIPTION");
            q.Property(p => p.SiteCode).HasColumnName("SITE_CODE");
            q.Property(p => p.VaxPallet).HasColumnName("VAX_PALLET");
            q.Property(p => p.StorageAreaCode).HasColumnName("STORAGE_AREA_CODE");
        }

        private void BuildStorageLocations(ModelBuilder builder)
        {
            var e = builder.Entity<StorageLocation>();
            e.ToTable("STORAGE_LOCATIONS");
            e.HasKey(l => l.LocationId);
            e.Property(l => l.LocationId).HasColumnName("LOCATION_ID");
            e.Property(l => l.LocationCode).HasColumnName("LOCATION_CODE");
            e.Property(l => l.Description).HasColumnName("DESCRIPTION");
        }

        private void BuildLabelReprints(ModelBuilder builder)
        {
            builder.Entity<LabelReprint>().ToTable("LABEL_REPRINTS");
            builder.Entity<LabelReprint>().HasKey(c => c.LabelReprintId);
            builder.Entity<LabelReprint>().Property(c => c.LabelReprintId).HasColumnName("LABEL_REP_ID");
            builder.Entity<LabelReprint>().Property(c => c.DateIssued).HasColumnName("DATE_ISSUED");
            builder.Entity<LabelReprint>().Property(c => c.RequestedBy).HasColumnName("REQUESTED_BY");
            builder.Entity<LabelReprint>().Property(c => c.Reason).HasColumnName("REASON").HasMaxLength(200);
            builder.Entity<LabelReprint>().Property(c => c.PartNumber).HasColumnName("PART_NUMBER").HasMaxLength(14);
            builder.Entity<LabelReprint>().Property(c => c.SerialNumber).HasColumnName("SERIAL_NUMBER");
            builder.Entity<LabelReprint>().Property(c => c.DocumentType).HasColumnName("DOC_TYPE").HasMaxLength(6);
            builder.Entity<LabelReprint>().Property(c => c.WorksOrderNumber).HasColumnName("DOCUMENT_NUMBER");
            builder.Entity<LabelReprint>().Property(c => c.LabelTypeCode).HasColumnName("LABEL_TYPE_CODE").HasMaxLength(16);
            builder.Entity<LabelReprint>().Property(c => c.NumberOfProducts).HasColumnName("NUMBER_OF_PRODUCTS");
            builder.Entity<LabelReprint>().Property(c => c.ReprintType).HasColumnName("REPRINT_TYPE").HasMaxLength(10);
            builder.Entity<LabelReprint>().Property(c => c.NewPartNumber).HasColumnName("NEW_PART_NUMBER").HasMaxLength(14);
        }

        private void BuildPartFailFaultCodes(ModelBuilder builder)
        {
            builder.Entity<PartFailFaultCode>().ToTable("PART_FAIL_FAULT_CODES");
            builder.Entity<PartFailFaultCode>().HasKey(c => c.FaultCode);
            builder.Entity<PartFailFaultCode>().Property(c => c.FaultCode).HasColumnName("FAULT_CODE");
            builder.Entity<PartFailFaultCode>().Property(c => c.Description).HasColumnName("FAULT_DESCRIPTION");
        }

        private void BuildPartFails(ModelBuilder builder)
        {
            var e = builder.Entity<PartFail>();
            e.ToTable("PART_FAIL_LOG");
            e.HasKey(f => f.Id);
            e.Property(f => f.Id).HasColumnName("ID");
            e.Property(f => f.Batch).HasColumnName("BATCH").HasMaxLength(20);
            e.HasOne<WorksOrder>(f => f.WorksOrder).WithMany(o => o.PartFails).HasForeignKey("WORKS_ORDER_NUMBER");
            e.Property(f => f.PurchaseOrderNumber).HasColumnName("PURCHASE_ORDER_NUMBER");
            e.Property(f => f.DateCreated).HasColumnName("DATE_CREATED");
            e.HasOne<Employee>(f => f.EnteredBy).WithMany(m => m.PartFailsEntered).HasForeignKey("ENTERED_BY");
            e.Property(f => f.MinutesWasted).HasColumnName("MINUTES_WASTED");
            e.HasOne<Part>(f => f.Part).WithMany(p => p.Fails).HasForeignKey("PART_NUMBER");
            e.Property(f => f.Quantity).HasColumnName("QTY");
            e.Property(f => f.Story).HasColumnName("STORY").HasMaxLength(200);
            e.Property(f => f.SerialNumber).HasColumnName("SERIAL_NUMBER");
            e.HasOne<PartFailFaultCode>(f => f.FaultCode).WithMany(c => c.PartFails).HasForeignKey("FAULT_CODE");
            e.HasOne<StorageLocation>(f => f.StorageLocation).WithMany(l => l.PartFails).HasForeignKey("LOCATION_ID");
            e.HasOne<PartFailErrorType>(f => f.ErrorType).WithMany(t => t.PartFails).HasForeignKey("ERROR_TYPE");
        }

        private void BuildPartFailErrorTypes(ModelBuilder builder)
        {
            builder.Entity<PartFailErrorType>().ToTable("PART_FAIL_ERROR_TYPES");
            builder.Entity<PartFailErrorType>().HasKey(t => t.ErrorType);
            builder.Entity<PartFailErrorType>().Property(e => e.ErrorType).HasColumnName("ERROR_TYPE");
            builder.Entity<PartFailErrorType>().Property(e => e.DateInvalid).HasColumnName("DATE_INVALID");
        }

        private void BuildPurchaseOrders(ModelBuilder builder)
        {
            builder.Entity<PurchaseOrder>().ToTable("PL_ORDERS");
            builder.Entity<PurchaseOrder>().HasKey(o => o.OrderNumber);
            builder.Entity<PurchaseOrder>().Property(o => o.OrderNumber).HasColumnName("ORDER_NUMBER");
            builder.Entity<PurchaseOrder>().Property(o => o.DateOfOrder).HasColumnName("DATE_OF_ORDER");
            builder.Entity<PurchaseOrder>().Property(o => o.OrderAddressId).HasColumnName("ORDER_ADDRESS_ID");
            builder.Entity<PurchaseOrder>().HasMany<PurchaseOrderDetail>(o => o.Details).WithOne(d => d.PurchaseOrder)
                .HasForeignKey(d => d.OrderNumber);
            builder.Entity<PurchaseOrder>().HasOne<Address>(p => p.OrderAddress).WithMany(a => a.PurchaseOrders).HasForeignKey(o => o.OrderAddressId);
            builder.Entity<PurchaseOrder>().Property(o => o.DocumentType).HasColumnName("DOCUMENT_TYPE");
            builder.Entity<PurchaseOrder>().Property(o => o.Remarks).HasColumnName("REMARKS");
        }

        private void BuildPurchaseOrderDetails(ModelBuilder builder)
        {
            builder.Entity<PurchaseOrderDetail>().ToTable("PL_ORDER_DETAILS");
            builder.Entity<PurchaseOrderDetail>().HasKey(d => new { d.OrderNumber, d.OrderLine });
            builder.Entity<PurchaseOrderDetail>().Property(d => d.OrderNumber).HasColumnName("ORDER_NUMBER");
            builder.Entity<PurchaseOrderDetail>().Property(d => d.OrderLine).HasColumnName("ORDER_LINE");
            builder.Entity<PurchaseOrderDetail>().Property(d => d.PartNumber).HasColumnName("PART_NUMBER");
            builder.Entity<PurchaseOrderDetail>().HasOne(t => t.Part).WithMany().HasForeignKey(d => d.PartNumber);
            builder.Entity<PurchaseOrderDetail>().Property(d => d.OrderQuantity).HasColumnName("ORDER_QTY");
            builder.Entity<PurchaseOrderDetail>().Property(d => d.OurUnitOfMeasure).HasColumnName("OUR_UNIT_OF_MEASURE");
            builder.Entity<PurchaseOrderDetail>().Property(d => d.IssuedSerialNumbers).HasColumnName("ISSUED_SERIAL_NUMBERS");
        }

        private void QueryAccountingCompanies(ModelBuilder builder)
        {
            var q = builder.Query<AccountingCompany>();
            q.ToView("ACCOUNTING_COMPANIES");
            q.Property(e => e.Name).HasColumnName("ACCOUNTING_COMPANY").HasMaxLength(10);
            q.Property(e => e.Description).HasColumnName("DESCRIPTION").HasMaxLength(50);
            q.Property(e => e.LatestSosJobId).HasColumnName("LATEST_SOS_JOB_ID");
            q.Property(e => e.DateLatestSosJobId).HasColumnName("DATE_LATEST_SOS_JOB_ID");
        }

        private void QueryProductionBackOrders(ModelBuilder builder)
        {
            var q = builder.Query<ProductionBackOrder>();
            q.ToView("V_PRODUCTION_BACK_ORDERS");
            q.Property(e => e.CitCode).HasColumnName("CIT_CODE").HasMaxLength(10);
            q.Property(e => e.ArticleNumber).HasColumnName("ARTICLE_NUMBER").HasMaxLength(14);
            q.Property(e => e.JobId).HasColumnName("JOB_ID");
            q.Property(e => e.OrderNumber).HasColumnName("ORDER_NUMBER");
            q.Property(e => e.OrderLine).HasColumnName("ORDER_LINE");
            q.Property(e => e.BackOrderQty).HasColumnName("BACK_ORDER_QTY");
            q.Property(e => e.BaseValue).HasColumnName("BASE_VALUE");
            q.Property(e => e.RequestedDeliveryDate).HasColumnName("REQUESTED_DELIVERY_DATE");
            q.Property(e => e.DatePossible).HasColumnName("DATE_POSSIBLE");
            q.Property(e => e.QueuePosition).HasColumnName("QUEUE_POSITION");
        }

        private void BuildProductData(ModelBuilder builder)
        {
            builder.Entity<ProductData>().ToTable("PRODUCT_DATA");
            builder.Entity<ProductData>().HasKey(o => o.ProductId);
            builder.Entity<ProductData>().Property(o => o.ProductId).HasColumnName("PRODUCT_ID");
            builder.Entity<ProductData>().Property(o => o.MACAddress).HasColumnName("MAC_ADDRESS").HasMaxLength(20);
            builder.Entity<ProductData>().Property(o => o.ProductGroup).HasColumnName("PRODUCT_GROUP").HasMaxLength(10);
        }

        private void QueryProductionTriggerAssemblies(ModelBuilder builder)
        {
            var q = builder.Query<ProductionTriggerAssembly>();
            q.ToView("PTL_ASSEMBLIES_VIEW_EF");
            q.Property(e => e.Jobref).HasColumnName("JOBREF").HasMaxLength(6);
            q.Property(e => e.PartNumber).HasColumnName("PART_NUMBER").HasMaxLength(14);
            q.Property(e => e.AssemblyNumber).HasColumnName("ASSEMBLY_NUMBER").HasMaxLength(14);
            q.Property(e => e.NettSalesOrders).HasColumnName("NETT_SALES_ORDERS");
            q.Property(e => e.QtyUsed).HasColumnName("QTY_USED");
            q.Property(e => e.QtyBeingBuilt).HasColumnName("QTY_BEING_BUILT");
            q.Property(e => e.ReqtForInternalAndTriggerLevelBT).HasColumnName("BT");
            q.Property(e => e.BomLevel).HasColumnName("BOM_LEVEL");
            q.Property(e => e.ReqtForPriorityBuildBE).HasColumnName("BE");
            q.Property(e => e.RemainingBuild).HasColumnName("REMAINING_BUILD");
        }

        private void QueryWwdDetails(ModelBuilder builder)
        {
            var q = builder.Query<WwdDetail>();
            q.ToView("WWD_EF_VIEW");
            q.Property(e => e.WwdJobId).HasColumnName("JOB_ID");
            q.Property(e => e.PtlJobref).HasColumnName("JOBREF").HasMaxLength(6);
            q.Property(e => e.PartNumber).HasColumnName("PART_NUMBER").HasMaxLength(14);
            q.Property(e => e.Description).HasColumnName("DESCRIPTION").HasMaxLength(200);
            q.Property(e => e.QtyKitted).HasColumnName("QTY_KITTED");
            q.Property(e => e.QtyAtLocation).HasColumnName("QTY_AT_LOCATION");
            q.Property(e => e.QtyReserved).HasColumnName("QTY_RESERVED");
            q.Property(e => e.LocationId).HasColumnName("LOCATION_ID");
            q.Property(e => e.PalletNumber).HasColumnName("PALLET_NUMBER");
            q.Property(e => e.Remarks).HasColumnName("REMARKS").HasMaxLength(200);
            q.Property(e => e.StoragePlace).HasColumnName("STORAGE_PLACE").HasMaxLength(41);
        }

        private void BuildLabelTypes(ModelBuilder builder)
        {
            var e = builder.Entity<LabelType>();
            e.ToTable("STORES_LABEL_TYPES");
            e.HasKey(s => s.LabelTypeCode);
            e.Property(s => s.LabelTypeCode).HasColumnName("LABEL_TYPE_CODE").HasMaxLength(16);
            e.Property(s => s.Description).HasColumnName("DESCRIPTION").HasMaxLength(50);
            e.Property(s => s.Filename).HasColumnName("FILENAME").HasMaxLength(50);
            e.Property(s => s.BarcodePrefix).HasColumnName("BARCODE_PREFIX").HasMaxLength(2);
            e.Property(s => s.DefaultPrinter).HasColumnName("DEFAULT_PRINTER").HasMaxLength(50);
            e.Property(s => s.NSBarcodePrefix).HasColumnName("BARCODE_PREFIX_NS").HasMaxLength(2);
            e.Property(s => s.CommandFilename).HasColumnName("CMD_FILENAME").HasMaxLength(50);
            e.Property(s => s.TestFilename).HasColumnName("TEST_FILENAME").HasMaxLength(50);
            e.Property(s => s.TestPrinter).HasColumnName("TEST_PRINTER").HasMaxLength(50);
            e.Property(s => s.TestCommandFilename).HasColumnName("TEST_CMD_FILENAME").HasMaxLength(50);
        }

        private void BuildBuildPlans(ModelBuilder builder)
        {
            var e = builder.Entity<BuildPlan>();
            e.ToTable("BUILD_PLANS");
            e.HasKey(b => b.BuildPlanName);
            e.Property(b => b.BuildPlanName).HasColumnName("BUILD_PLAN_NAME").HasMaxLength(10);
            e.Property(b => b.Description).HasColumnName("DESCRIPTION").HasMaxLength(50);
            e.Property(b => b.DateCreated).HasColumnName("DATE_CREATED");
            e.Property(b => b.DateInvalid).HasColumnName("DATE_INVALID");
            e.Property(b => b.LastMrpJobRef).HasColumnName("LAST_MRP_JOBREF").HasMaxLength(6);
            e.Property(b => b.LastMrpDateStarted).HasColumnName("LAST_MRP_DATE_STARTED");
            e.Property(b => b.LastMrpDateFinished).HasColumnName("LAST_MRP_DATE_FINISHED");
        }

        private void BuildAteTests(ModelBuilder builder)
        {
            var e = builder.Entity<AteTest>();
            e.ToTable("ATE_TESTS");
            e.HasKey(t => t.TestId);
            e.Property(t => t.TestId).HasColumnName("ATE_TEST_ID");
            e.HasOne<Employee>(f => f.User).WithMany(m => m.AteTestsEntered).HasForeignKey("USER_NUMBER");
            e.HasOne<Employee>(f => f.PcbOperator).WithMany(m => m.AteTestsPcbOperatorOn).HasForeignKey("PCB_OPERATOR");
            e.Property(t => t.DateTested).HasColumnName("DATE_TESTED");
            e.HasOne<WorksOrder>(f => f.WorksOrder).WithMany(o => o.AteTests).HasForeignKey("WORKS_ORDER_NUMBER");
            e.Property(t => t.NumberTested).HasColumnName("NUMBER_TESTED");
            e.Property(t => t.NumberOfSmtComponents).HasColumnName("NUMBER_SMT_COMPONENTS");
            e.Property(t => t.NumberOfSmtFails).HasColumnName("NUMBER_SMT_FAILS");
            e.Property(t => t.NumberOfPcbComponents).HasColumnName("NUMBER_PCB_COMPONENTS");
            e.Property(t => t.NumberOfPcbFails).HasColumnName("NUMBER_PCB_FAILS");
            e.Property(t => t.NumberOfPcbBoardFails).HasColumnName("NUMBER_PCB_BOARD_FAILS");
            e.Property(t => t.NumberOfSmtBoardFails).HasColumnName("NUMBER_SMT_BOARD_FAILS");
            e.Property(t => t.MinutesSpent).HasColumnName("MINUTES_SPENT");
            e.Property(t => t.Machine).HasColumnName("MACHINE").HasMaxLength(10);
            e.Property(t => t.PlaceFound).HasColumnName("PLACE_FOUND").HasMaxLength(10);
            e.Property(t => t.DateInvalid).HasColumnName("DATE_INVALID");
            e.Property(t => t.FlowMachine).HasColumnName("FLOW_MACHINE").HasMaxLength(16);
            e.Property(t => t.FlowSolderDate).HasColumnName("FLOW_SOLDER_DATE");
            e.HasMany(t => t.Details).WithOne().HasForeignKey(d => d.TestId);
        }

        private void BuildAteTestDetails(ModelBuilder builder)
        {
            var e = builder.Entity<AteTestDetail>();
            e.ToTable("ATE_TEST_DETAILS");
            e.HasKey(d => new { d.ItemNumber, d.TestId });
            e.Property(d => d.TestId).HasColumnName("ATE_TEST_ID");
            e.Property(d => d.ItemNumber).HasColumnName("ITEM_NO");
            e.Property(d => d.PartNumber).HasColumnName("PART_NUMBER").HasMaxLength(14);
            e.Property(d => d.NumberOfFails).HasColumnName("NUMBER_FAILURES");
            e.Property(d => d.CircuitRef).HasColumnName("CIRCUIT_REF").HasMaxLength(8);
            e.Property(d => d.AteTestFaultCode).HasColumnName("ATE_TEST_FAULT_CODE").HasMaxLength(10);
            e.Property(d => d.SmtOrPcb).HasColumnName("SMT_OR_PCB").HasMaxLength(12);
            e.Property(d => d.Shift).HasColumnName("SHIFT").HasMaxLength(10);
            e.Property(d => d.BatchNumber).HasColumnName("BATCH_NO").HasMaxLength(6);
            e.HasOne<Employee>(f => f.PcbOperator).WithMany(m => m.AteTestDetailsPcbOperatorOn).HasForeignKey("PCB_OPERATOR");
            e.Property(d => d.Comments).HasColumnName("COMMENTS").HasMaxLength(2000);
            e.Property(d => d.Machine).HasColumnName("MACHINE").HasMaxLength(10);
            e.Property(d => d.BoardFailNumber).HasColumnName("BOARD_FAIL_NUMBER");
            e.Property(d => d.AoiEscape).HasColumnName("AOI_ESCAPE").HasMaxLength(21);
            e.Property(d => d.CorrectiveAction).HasColumnName("CORRECTIVE_ACTION").HasMaxLength(2000);
            e.Property(d => d.SmtFailId).HasColumnName("SMT_FAIL_ID");
            e.Property(d => d.BoardSerialNumber).HasColumnName("BOARD_SN").HasMaxLength(20);
        }

        private void QueryBuiltThisWeekStatistics(ModelBuilder builder)
        {
            var q = builder.Query<BuiltThisWeekStatistic>();
            q.ToView("BUILT_THIS_WEEK_VIEW");
            q.Property(b => b.CitCode).HasColumnName("CODE").HasMaxLength(10);
            q.Property(b => b.CitName).HasColumnName("CIT_NAME").HasMaxLength(50);
            q.Property(b => b.PartNumber).HasColumnName("PART_NUMBER").HasMaxLength(14);
            q.Property(b => b.Description).HasColumnName("DESCRIPTION").HasMaxLength(200);
            q.Property(b => b.BuiltThisWeek).HasColumnName("BUILT_THIS_WEEK");
            q.Property(b => b.Value).HasColumnName("VALUE");
            q.Property(b => b.Days).HasColumnName("DAYS");
        }

        private void BuildAddresses(ModelBuilder builder)
        {
            var q = builder.Entity<Address>();
            q.ToTable("ADDRESSES");
            q.Property(b => b.Id).HasColumnName("ADDRESS_ID").HasMaxLength(38);
            q.Property(b => b.Addressee).HasColumnName("ADDRESSEE").HasMaxLength(40);
            q.Property(b => b.Addressee2).HasColumnName("ADDRESSEE_2").HasMaxLength(40);
            q.Property(b => b.Line1).HasColumnName("ADDRESS_1").HasMaxLength(40);
            q.Property(b => b.Line2).HasColumnName("ADDRESS_2").HasMaxLength(40);
            q.Property(b => b.Line3).HasColumnName("ADDRESS_3").HasMaxLength(40);
            q.Property(b => b.Line4).HasColumnName("ADDRESS_4").HasMaxLength(40);
            q.Property(b => b.Country).HasColumnName("COUNTRY").HasMaxLength(2);
            q.Property(b => b.PostCode).HasColumnName("POSTAL_CODE");
            q.Property(b => b.DateInvalid).HasColumnName("DATE_INVALID");
        }

        private void BuildSuppliers(ModelBuilder builder)
        {
            var q = builder.Entity<Supplier>();
            q.ToTable("SUPPLIERS");
            q.Property(b => b.SupplierId).HasColumnName("SUPPLIER_ID").HasMaxLength(6);
            q.Property(b => b.SupplierName).HasColumnName("SUPPLIER_NAME").HasMaxLength(50);
            q.Property(b => b.OrderAddressId).HasColumnName("ORD_ADDRESS_ID").HasMaxLength(10);
            q.Property(b => b.InvoiceAddressId).HasColumnName("INV_ADDRESS_ID").HasMaxLength(10);
            q.Property(b => b.DateClosed).HasColumnName("DATE_CLOSED");
        }

        private void QueryPtlStats(ModelBuilder builder)
        {
            var q = builder.Query<PtlStat>();
            q.ToView("PTL_STAT_VIEW");
            q.Property(s => s.BuildGroup).HasColumnName("BUILD_GROUP").HasMaxLength(2);
            q.Property(s => s.CitName).HasColumnName("NAME").HasMaxLength(50);
            q.Property(s => s.CitCode).HasColumnName("CIT_CODE").HasMaxLength(10);
            q.Property(s => s.PartNumber).HasColumnName("PART_NUMBER").HasMaxLength(14);
            q.Property(s => s.PartDescription).HasColumnName("PART_DESCRIPTION").HasMaxLength(200);
            q.Property(s => s.TriggerId).HasColumnName("TRIGGER_ID");
            q.Property(s => s.PtlPriority).HasColumnName("PRIORITY");
            q.Property(s => s.SortOrder).HasColumnName("SORT_ORDER");
            q.Property(s => s.DateCompleted).HasColumnName("DATE_COMPLETED");
            q.Property(s => s.TriggerDate).HasColumnName("TRIGGER_DATE");
            q.Property(s => s.WorkingDays).HasColumnName("WORKING_DAYS");
        }

        private void BuildCountries(ModelBuilder builder)
        {
            builder.Entity<Country>().ToTable("COUNTRIES");
            builder.Entity<Country>().HasKey(c => c.CountryCode);
            builder.Entity<Country>().Property(c => c.CountryCode).HasColumnName("COUNTRY_CODE");
            builder.Entity<Country>().Property(c => c.Name).HasColumnName("NAME");
        }

        private void QuerySernosBuiltView(ModelBuilder builder)
        {
            var q = builder.Query<SernosBuilt>();
            q.ToView("SERNOS_BUILT_VIEW");
            q.Property(e => e.ArticleNumber).HasColumnName("ARTICLE_NUMBER");
            q.Property(e => e.SernosGroup).HasColumnName("SERNOS_GROUP");
            q.Property(e => e.SernosNumber).HasColumnName("SERNOS_NUMBER");
        }

        private void QuerySernosIssuedView(ModelBuilder builder)
        {
            var q = builder.Query<SernosIssued>();
            q.ToView("SERNOS_ISSUED_VIEW");
            q.Property(e => e.DocumentNumber).HasColumnName("DOCUMENT_NUMBER");
            q.Property(e => e.SernosGroup).HasColumnName("SERNOS_GROUP");
            q.Property(e => e.SernosNumber).HasColumnName("SERNOS_NUMBER");
            q.Property(e => e.DocumentType).HasColumnName("DOCUMENT_TYPE");
        }

        private void QueryPurchaseOrdersReceivedView(ModelBuilder builder)
        {
            var q = builder.Query<PurchaseOrdersReceived>();
            q.ToView("PLOD_RECEIVED_VIEW");
            q.Property(e => e.QuantityNetReceived).HasColumnName("QTY_NET_RECEIVED");
            q.Property(e => e.OrderNumber).HasColumnName("ORDER_NUMBER");
            q.Property(e => e.OrderLine).HasColumnName("ORDER_LINE");
        }

        private void QueryWswShortages(ModelBuilder builder)
        {
            var q = builder.Query<WswShortage>();
            q.ToView("WSW_SHORTAGE_VIEW");
            q.Property(s => s.Jobref).HasColumnName("JOBREF").HasMaxLength(6);
            q.Property(s => s.CitCode).HasColumnName("CIT_CODE").HasMaxLength(10);
            q.Property(s => s.PartNumber).HasColumnName("PART_NUMBER").HasMaxLength(14);
            q.Property(s => s.ShortPartNumber).HasColumnName("SHORT_PART_NUMBER").HasMaxLength(14);
            q.Property(s => s.ShortPartDescription).HasColumnName("DESCRIPTION").HasMaxLength(200);
            q.Property(s => s.ShortageCategory).HasColumnName("SHORT_CAT").HasMaxLength(4);
            q.Property(s => s.Required).HasColumnName("REQT");
            q.Property(s => s.Stock).HasColumnName("STOCK");
            q.Property(s => s.AdjustedAvailable).HasColumnName("ADJUSTED_AVAIL");
            q.Property(s => s.QtyReserved).HasColumnName("QTY_RESERVED");
            q.Property(s => s.KittingPriority).HasColumnName("KITTING_PRIORITY");
            q.Property(s => s.CanBuild).HasColumnName("SHORTAGE_CAN_BUILD");
        }
    }
}
