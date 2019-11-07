namespace Linn.Production.Persistence.LinnApps
{
    using System.Linq;

    using Linn.Common.Configuration;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Domain.LinnApps.BackOrders;
    using Linn.Production.Domain.LinnApps.BoardTests;
    using Linn.Production.Domain.LinnApps.Measures;
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

        public PtlMaster PtlMaster => this.PtlMasterSet.ToList().FirstOrDefault();

        public OsrRunMaster OsrRunMaster => this.OsrRunMasterSet.ToList().FirstOrDefault();

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

        public DbQuery<PartFailLog> PartFailLogs { get; set; }

        public DbQuery<EmployeeDepartmentView> EmployeeDepartmentView { get; set; }
        
        public DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }

        public DbSet<ProductData> ProductData { get; set; }

        public DbSet<BoardTest> BoardTests { get; set; }

        public DbSet<TestMachine> TestMachines { get; set; }

        private DbQuery<OsrRunMaster> OsrRunMasterSet { get; set; }

        private DbQuery<PtlMaster> PtlMasterSet { get; set; } 

        public DbQuery<WwdDetail> WwdDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            this.BuildAte(builder);
            this.BuildSerialNumberReissues(builder);
            this.BuildDepartments(builder);
            this.BuildBuilds(builder);
            this.BuildCits(builder);
            this.BuildProductionMeasures(builder);
            this.QueryWhoBuildWhat(builder);
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
            this.BuildStorageLocations(builder);
            this.BuildPurchaseOrders(builder);
            this.QueryAccountingCompanies(builder);
            this.QueryProductionBackOrders(builder);
            this.QueryProductionTriggerAssemblies(builder);
            this.BuildPurchaseOrderDetails(builder);
            this.QueryOverdueOrderLines(builder);
            this.QueryPartFailLogs(builder);
            this.QueryEmployeeDepartmentView(builder);
            this.BuildProductData(builder);
            this.BuildWorksOrdersLabels(builder);
            this.QueryWwdDetails(builder);
            base.OnModelCreating(builder);
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
            e.Property(p => p.WsName).HasColumnName("WS_NAME").HasMaxLength(16);
            e.Property(p => p.FaZoneType).HasColumnName("FA_ZONE_TYPE").HasMaxLength(20);
            e.Property(p => p.VariableTriggerLevel).HasColumnName("VARIABLE_TRIGGER_LEVEL");
            e.Property(p => p.OverrideTriggerLevel).HasColumnName("OVERRIDE_TRIGGER_LEVEL");
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

        private void QueryPartFailLogs(ModelBuilder builder)
        {
            var q = builder.Query<PartFailLog>();
            q.ToView("PART_FAIL_LOG");
            q.Property(t => t.Id).HasColumnName("ID");
            q.Property(t => t.DateCreated).HasColumnName("DATE_CREATED");
            q.Property(t => t.PartNumber).HasColumnName("PART_NUMBER");
            q.Property(t => t.FaultCode).HasColumnName("FAULT_CODE");
            q.Property(t => t.Story).HasColumnName("STORY");
            q.Property(t => t.Quantity).HasColumnName("QTY");
            q.Property(t => t.MinutesWasted).HasColumnName("MINUTES_WASTED");
            q.Property(t => t.ErrorType).HasColumnName("ERROR_TYPE");
            q.Property(t => t.Batch).HasColumnName("BATCH");
            q.Property(t => t.EnteredBy).HasColumnName("ENTERED_BY");
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
            e.HasKey(s => s.RouteCode);
            e.HasKey(s => s.ManufacturingId);
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
            e.Property(f => f.Batch).HasColumnName("BATCH");
            e.HasOne<WorksOrder>(f => f.WorksOrder).WithMany(o => o.PartFails).HasForeignKey("WORKS_ORDER_NUMBER");
            e.Property(f => f.PurchaseOrderNumber).HasColumnName("PURCHASE_ORDER_NUMBER");
            e.Property(f => f.DateCreated).HasColumnName("DATE_CREATED");
            e.HasOne<Employee>(f => f.EnteredBy).WithMany(m => m.PartFailsEntered).HasForeignKey("ENTERED_BY");
            e.Property(f => f.MinutesWasted).HasColumnName("MINUTES_WASTED");
            e.Property(f => f.Batch).HasColumnName("BATCH");
            e.HasOne<Part>(f => f.Part).WithMany(p => p.Fails).HasForeignKey("PART_NUMBER");
            e.Property(f => f.Quantity).HasColumnName("QTY");
            e.Property(f => f.Story).HasColumnName("STORY");
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
            builder.Entity<PurchaseOrder>().HasMany<PurchaseOrderDetail>(o => o.Details).WithOne(d => d.PurchaseOrder)
                .HasForeignKey(d => d.OrderNumber);
        }
        private void BuildPurchaseOrderDetails(ModelBuilder builder)
        {
            builder.Entity<PurchaseOrderDetail>().ToTable("PL_ORDER_DETAILS");
            builder.Entity<PurchaseOrderDetail>().HasKey(d => new { d.OrderNumber, d.OrderLine });
            builder.Entity<PurchaseOrderDetail>().Property(d => d.OrderNumber).HasColumnName("ORDER_NUMBER");
            builder.Entity<PurchaseOrderDetail>().Property(d => d.OrderLine).HasColumnName("ORDER_LINE");
            builder.Entity<PurchaseOrderDetail>().Property(d => d.PartNumber).HasColumnName("PART_NUMBER");
        }        private void QueryAccountingCompanies(ModelBuilder builder)
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
    }
}
