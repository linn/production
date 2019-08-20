namespace Linn.Production.Persistence.LinnApps
{
    using System.Net;

    using Linn.Common.Configuration;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Domain.LinnApps.Measures;
    using Linn.Production.Domain.LinnApps.SerialNumberReissue;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            this.BuildAte(builder);
            this.BuildSerialNumberReissues(builder);
            this.BuildDepartments(builder);
            this.BuildBuilds(builder);
            this.BuildCits(builder);
            this.BuildProductionMeasures(builder);
            this.QueryWhoBuildWhat(builder);
            this.BuildManufacturingSkills(builder);
            this.BuildBoardFailTypes(builder);
            this.BuildAssemblyFails(builder);
            this.BuildWorkOrders(builder);
            this.BuildParts(builder);
            this.BuildEmployees(builder);
            this.BuildAssemblyFailFaultCodes(builder);
            base.OnModelCreating(builder);
        }

        protected void QueryWhoBuildWhat(ModelBuilder builder)
        {
            builder.Query<WhoBuiltWhat>().ToView("V_WHO_BUILT_WHAT");
            builder.Query<WhoBuiltWhat>().Property(v => v.CitCode).HasColumnName("CIT_CODE");
            builder.Query<WhoBuiltWhat>().Property(t => t.CitName).HasColumnName("CIT_NAME");
            builder.Query<WhoBuiltWhat>().Property(t => t.SernosDate).HasColumnName("SERNOS_DATE");
            builder.Query<WhoBuiltWhat>().Property(t => t.ArticleNumber).HasColumnName("ARTICLE_NUMBER");
            builder.Query<WhoBuiltWhat>().Property(t => t.CreatedBy).HasColumnName("CREATED_BY");
            builder.Query<WhoBuiltWhat>().Property(t => t.UserName).HasColumnName("USER_NAME");
            builder.Query<WhoBuiltWhat>().Property(t => t.QtyBuilt).HasColumnName("QTY_BUILT");
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

        protected void BuildAte(ModelBuilder builder)
        {
            builder.Entity<AteFaultCode>().ToTable("ATE_TEST_FAULT_CODES");
            builder.Entity<AteFaultCode>().HasKey(t => t.FaultCode);
            builder.Entity<AteFaultCode>().Property(t => t.FaultCode).HasColumnName("FAULT_CODE");
            builder.Entity<AteFaultCode>().Property(t => t.Description).HasColumnName("DESCRIPTION");
            builder.Entity<AteFaultCode>().Property(t => t.DateInvalid).HasColumnName("DATE_INVALID");
        }

        protected void BuildAssemblyFails(ModelBuilder builder)
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

        protected void BuildWorkOrders(ModelBuilder builder)
        {
            var e = builder.Entity<WorksOrder>().ToTable("WORKS_ORDERS");
            e.HasKey(o => o.OrderNumber);
            e.Property(o => o.PartNumber).HasColumnName("PART_NUMBER");
            e.Property(o => o.OrderNumber).HasColumnName("ORDER_NUMBER");
            e.HasOne<Part>(o => o.Part).WithMany(w => w.WorksOrders).HasForeignKey(o => o.PartNumber);
        }

        protected void BuildBoardFailTypes(ModelBuilder builder)
        {
            builder.Entity<BoardFailType>().ToTable("BOARD_FAIL_TYPES");
            builder.Entity<BoardFailType>().HasKey(t => t.Type);
            builder.Entity<BoardFailType>().Property(t => t.Type).HasColumnName("FAIL_TYPE");
            builder.Entity<BoardFailType>().Property(t => t.Description).HasColumnName("FAIL_DESCRIPTION");
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
    }
}
