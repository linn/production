using Linn.Production.Domain.LinnApps.SerialNumberIssue;

namespace Linn.Production.Persistence.LinnApps
{
    using Common.Configuration;
    using Linn.Production.Domain.LinnApps.ATE;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class ServiceDbContext : DbContext
    {
        public static readonly LoggerFactory MyLoggerFactory =
            new LoggerFactory(new[] { new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider() });

        public DbSet<AteFaultCode> AteFaultCodes { get; set; }

        public DbSet<SerialNumberIssue> SerialNumberIssues { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            this.BuildAte(builder);
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

        private void BuildAte(ModelBuilder builder)
        {
            builder.Entity<AteFaultCode>().ToTable("ATE_TEST_FAULT_CODES");
            builder.Entity<AteFaultCode>().HasKey(t => t.FaultCode);
            builder.Entity<AteFaultCode>().Property(t => t.FaultCode).HasColumnName("FAULT_CODE");
            builder.Entity<AteFaultCode>().Property(t => t.Description).HasColumnName("DESCRIPTION");
            builder.Entity<AteFaultCode>().Property(t => t.DateInvalid).HasColumnName("DATE_INVALID");
        }

        private void BuildSerialNumberIssues(ModelBuilder builder)
        {
            builder.Entity<SerialNumberIssue>().ToTable("SERNOS_RENUM");
            builder.Entity<SerialNumberIssue>().HasKey(s => s.Id);
            builder.Entity<SerialNumberIssue>().Property(s => s.Id).HasColumnName("SNRENUM_ID");
            builder.Entity<SerialNumberIssue>().Property(s => s.SernosGroup).HasColumnName("SERNOS_GROUP").HasMaxLength(10); ;
            builder.Entity<SerialNumberIssue>().Property(s => s.SerialNumber).HasColumnName("SERNOS_NUMBER");
            builder.Entity<SerialNumberIssue>().Property(s => s.NewSerialNumber).HasColumnName("NEW_SERNOS_NUMBER");
            builder.Entity<SerialNumberIssue>().Property(s => s.Comments).HasColumnName("COMMENTS").HasMaxLength(200);
            builder.Entity<SerialNumberIssue>().Property(s => s.CreatedBy).HasColumnName("CREATED_BY");
            builder.Entity<SerialNumberIssue>().Property(s => s.ArticleNumber).HasColumnName("ARTICLE_NUMBER").HasMaxLength(14);
            builder.Entity<SerialNumberIssue>().Property(s => s.NewArticleNumber).HasColumnName("NEW_ARTICLE_NUMBER").HasMaxLength(14);
        }
    }
}