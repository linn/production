namespace Linn.Production.Persistence.LinnApps
{
    using Domain.LinnApps;

    using Linn.Common.Configuration;
    using Linn.Production.Domain.LinnApps.ATE;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class ServiceDbContext : DbContext
    {
        public static readonly LoggerFactory MyLoggerFactory =
            new LoggerFactory(new[] { new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider() });

        public DbSet<Department> Departments { get; set; }

        public DbQuery<Build> Builds { get; set; }

        public DbSet<AteFaultCode> AteFaultCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            this.BuildAte(builder);

            this.BuildDepartments(builder);
            this.BuildBuilds(builder);
            base.OnModelCreating(builder);
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
    }
}