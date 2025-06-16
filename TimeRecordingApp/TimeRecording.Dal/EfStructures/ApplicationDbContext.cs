using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TimeRecording.Dal.Entities;
using Microsoft.Extensions.Configuration;

namespace TimeRecording.Dal.EfStructures
{
    public partial class ApplicationDbContext : DbContext
    {
        /*  Microsoft.EntityFrameworkCore
            Microsoft.EntityFrameworkCore.SqlServer
            Microsoft.EntityFrameworkCore.Tools
         * Paket Manager Console:
         Add-Migration InitialMigration
         Update-Database
         
         
         */
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Activities> Activities { get; set; }
        public virtual DbSet<TimeRecordings> TimeRecordings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.

                //optionsBuilder.UseSqlServer("server=.,61770;Database=TimeRecording;User Id=sa;Password=panther;");
                // optionsBuilder.UseSqlServer("server=DESKTOP-LN64940\\MSSQLSERVER01;Database=TimeRecording;User Id=sa;Password=panther;Encrypt=False");
                /*
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();*/

                var config = new ConfigurationBuilder ()
                .AddJsonFile ("appsettings.json")
                .Build ();

                var connectionString = config.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activities>(entity =>
            {
                entity.Property(e => e.Priority).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TimeRecordings>(entity =>
            {
                entity.HasOne(d => d.Activity)
                    .WithMany(p => p.TimeRecordings)
                    .HasForeignKey(d => d.ActivityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TimeRecordingss_Activities");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
