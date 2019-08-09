﻿namespace Linn.Production.Persistence.LinnApps
{
    using Linn.Common.Configuration;
    using Linn.Production.Domain.LinnApps;
    using Linn.Production.Domain.LinnApps.ATE;
    using Linn.Production.Domain.LinnApps.Measures;
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

        public DbSet<Cit> Cits { get; set; }

        public DbSet<ProductionMeasures> ProductionMeasures { get; set; }

        public DbQuery<WhoBuiltWhat> WhoBuiltWhat { get; set; }

        public DbSet<ManufacturingSkill> ManufacturingSkills { get; set; }


        public DbSet<ManufacturingResource> ManufacturingResources { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            this.BuildAte(builder);

            this.BuildDepartments(builder);
            this.BuildBuilds(builder);

            this.BuildCits(builder);
            this.BuildProductionMeasures(builder);
            this.QueryWhoBuildWhat(builder);
            this.BuildManufacturingResources(builder);

            this.BuildManufacturingSkills(builder);
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

        protected void BuildAte(ModelBuilder builder)
        {
            builder.Entity<AteFaultCode>().ToTable("ATE_TEST_FAULT_CODES");
            builder.Entity<AteFaultCode>().HasKey(t => t.FaultCode);
            builder.Entity<AteFaultCode>().Property(t => t.FaultCode).HasColumnName("FAULT_CODE");
            builder.Entity<AteFaultCode>().Property(t => t.Description).HasColumnName("DESCRIPTION");
            builder.Entity<AteFaultCode>().Property(t => t.DateInvalid).HasColumnName("DATE_INVALID");
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
            // readonly! 
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

        private void BuildManufacturingResources(ModelBuilder builder)
        {
            var e = builder.Entity<ManufacturingResource>();
            e.ToTable("MFG_RESOURCES");
            e.HasKey(c => c.ResourceCode);
            e.Property(c => c.ResourceCode).HasColumnName("MFG_RESOURCE_CODE").HasMaxLength(10);
            e.Property(c => c.Description).HasColumnName("DESCRIPTION").HasMaxLength(50);
            e.Property(c => c.Cost).HasColumnName("COST_POUNDS_PER_HOUR").HasMaxLength(14);
        }
    }
}
