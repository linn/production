namespace Linn.Production.Persistence.LinnApps
{
    using System.Linq;

    using Linn.Common.Configuration;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.SerialNumberReissue;
    using Linn.Production.Domain.LinnApps.Triggers;
    using Linn.Production.Domain.LinnApps.ViewModels;

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

        public DbSet<ManufacturingSkill> ManufacturingSkills { get; set; }

        public DbSet<BoardFailType> BoardFailTypes { get; set; }

        public DbSet<AssemblyFail> AssemblyFails { get; set; }

        public DbSet<WorksOrder> WorksOrders { get; set; }

        public DbSet<Part> Parts { get; set; }

        public DbSet<AssemblyFailFaultCode> AssemblyFailFaultCodes { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<ProductionTriggerLevel> ProductionTriggerLevels { get; set; }

        public DbQuery<PcasRevision> PcasRevisions { get; set; }

        public DbSet<ManufacturingResource> ManufacturingResources { get; set; }

        public PtlMaster PtlMaster => this.PtlMasterSet.ToList().FirstOrDefault();

        public OsrRunMaster OsrRunMaster => this.OsrRunMasterSet.ToList().FirstOrDefault();

        public DbQuery<ProductionTrigger> ProductionTriggers { get; set; }

        public DbSet<LinnWeek> LinnWeeks { get; set; }

        private DbQuery<OsrRunMaster> OsrRunMasterSet { get; set; }

        private DbQuery<PtlMaster> PtlMasterSet { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            this.BuildAte(builder);
            this.BuildSerialNumberReissues(builder);
            this.BuildDepartments(builder);
            this.BuildBuilds(builder);
            this.BuildCits(builder);
            this.BuildProductionMeasures(builder);
            this.QueryWhoBuildWhat(builder);
            this.BuildManufacturingResources(builder);

            this.BuildManufacturingSkills(builder);
            this.BuildBoardFailTypes(builder);
            this.BuildAssemblyFails(builder);
            this.BuildWorkOrders(builder);
            this.BuildParts(builder);
            this.BuildEmployees(builder);
            this.BuildAssemblyFailFaultCodes(builder);
            this.BuildProductionTriggerLevels(builder);
            this.QueryPcasRevisions(builder);
            this.BuildAssemblyFailFaultCodes(builder);
            this.QueryPtlMaster(builder);
            this.QueryOsrRunMaster(builder);
            this.QueryProductionTriggers(builder);
            this.BuildLinnWeeks(builder);
            base.OnModelCreating(builder);
        }

        protected void QueryPcasRevisions(ModelBuilder builder)
        {
            builder.Query<PcasRevision>().ToView("PCAS_REVISION_COMP_VIEW");
            builder.Query<PcasRevision>().Property(r => r.Cref).HasColumnName("CREF");
            builder.Query<PcasRevision>().Property(r => r.PartNumber).HasColumnName("PART_NUMBER");
            builder.Query<PcasRevision>().Property(r => r.PcasPartNumber).HasColumnName("PCAS_PART_NUMBER");
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

        private void BuildWorkOrders(ModelBuilder builder)
        {
            var e = builder.Entity<WorksOrder>().ToTable("WORKS_ORDERS");
            e.HasKey(o => o.OrderNumber);
            e.Property(o => o.PartNumber).HasColumnName("PART_NUMBER");
            e.Property(o => o.OrderNumber).HasColumnName("ORDER_NUMBER");
            e.HasOne<Part>(o => o.Part).WithMany(w => w.WorksOrders).HasForeignKey(o => o.PartNumber);
        }

        private void BuildBoardFailTypes(ModelBuilder builder)
        {
            builder.Entity<BoardFailType>().ToTable("BOARD_FAIL_TYPES");
            builder.Entity<BoardFailType>().HasKey(t => t.Type);
            builder.Entity<BoardFailType>().Property(t => t.Type).HasColumnName("FAIL_TYPE");
            builder.Entity<BoardFailType>().Property(t => t.Description).HasColumnName("FAIL_DESCRIPTION");
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

        private void BuildProductionTriggerLevels(ModelBuilder builder)
        {
            var e = builder.Entity<ProductionTriggerLevel>();
            e.ToTable("PRODUCTION_TRIGGER_LEVELS");
            e.HasKey(l => l.PartNumber);
            e.Property(l => l.PartNumber).HasColumnName("PART_NUMBER");
            e.Property(l => l.Description).HasColumnName("DESCRIPTION");
        }

        private void BuildCits(ModelBuilder builder)
        {
            var e = builder.Entity<Cit>();
            e.ToTable("CITS");
            e.HasKey(c => c.Code);
            e.Property(c => c.Code).HasColumnName("CODE").HasMaxLength(10);
            e.Property(c => c.Name).HasColumnName("NAME").HasMaxLength(50);
            e.Property(c => c.BuildGroup).HasColumnName("BUILD_GROUP").HasMaxLength(2);
            e.Property(c => c.SortOrder).HasColumnName("SORT_ORDER");
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
            e.Property(p => p.PartNumber).HasColumnName("PART_NUMBER");
            e.Property(p => p.Description).HasColumnName("DESCRIPTION");
        }

        private void BuildAssemblyFailFaultCodes(ModelBuilder builder)
        {
            var e = builder.Entity<AssemblyFailFaultCode>();
            e.ToTable("ASSEMBLY_FAIL_FAULT_CODES");
            e.HasKey(c => c.FaultCode);
            e.Property(c => c.FaultCode).HasColumnName("FAULT_CODE");
            e.Property(c => c.Description).HasColumnName("DESCRIPTION");
        }

        private void BuildEmployees(ModelBuilder builder)
        {
            var q = builder.Entity<Employee>();
            q.HasKey(e => e.Id);
            q.ToTable("AUTH_USER_NAME_VIEW");
            q.Property(e => e.Id).HasColumnName("USER_NUMBER");
            q.Property(e => e.FullName).HasColumnName("USER_NAME");
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
            q.Property(e => e.KanbanSize).HasColumnName("KANBAN_SIZE");
            q.Property(e => e.EffectiveKanbanSize).HasColumnName("EFFECTIVE_KANBAN_SIZE");
            q.Property(e => e.MaximumKanbans).HasColumnName("MAXIMUM_KANBANS");
            q.Property(e => e.MfgRouteCode).HasColumnName("MFG_ROUTE_CODE").HasMaxLength(20);
            q.Property(e => e.DaysToBuildKanban).HasColumnName("DAYS_TO_BUILD_KANBAN");
            q.Property(e => e.NettSalesOrders).HasColumnName("NETT_SALES_ORDERS");
            q.Property(e => e.QtyFree).HasColumnName("QTY_FREE");
            q.Property(e => e.RemainingBuild).HasColumnName("REMAINING_BUILD");
            q.Property(e => e.QtyBeingBuilt).HasColumnName("QTY_BEING_BUILT");
            q.Property(e => e.ReqtForSalesOrdersBE).HasColumnName("ReqtForSalesOrdersBE");
            q.Property(e => e.ReqtForInternalCustomersBI).HasColumnName("ReqtForInternalCustomersBI");
            q.Property(e => e.ReqtForInternalAndTriggerLevelBT).HasColumnName("ReqtForInternalAndTriggerLevelBT");
            q.Property(e => e.ReqtForSalesOrdersGBE).HasColumnName("ReqtForSalesOrdersGBE");
            q.Property(e => e.ReqtForInternalCustomersGBI).HasColumnName("ReqtForInternalCustomersGBI");
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
    }
}
