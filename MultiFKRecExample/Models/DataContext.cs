using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MultiFKRecExample.Models
{
    public class DataContext : DbContext
    {
        public DataContext()
        {

        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=alpha\sql_db;Database=TestDB;User Id=user;Password=r]PysfhzuzDiLdbsH_An4#=hXmECcQ;MultipleActiveResultSets=true");
            }
        }

        public virtual DbSet<Immunization> Immunizations { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }

        public virtual DbSet<ImmunizationRecord> ImmunizationRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Patients

            modelBuilder.Entity<Patient>(entity =>
            {
                // Foreign keys
                entity.HasMany(patient => patient.ImmunizationRecords).WithOne(immunization => immunization.PatientData).HasConstraintName($"FK_{nameof(Patient)}_{nameof(ImmunizationRecord)}");

            });

            // ImmunizationRecord

            modelBuilder.Entity<ImmunizationRecord>(entity =>
            {
                // Foreign keys ** Replace HasDatabaseName with HasName for older SDK **
                entity.HasIndex(x => x.PatientID).HasDatabaseName($"FK_{nameof(ImmunizationRecord)}_{nameof(Patient)}");
                entity.HasIndex(x => x.ImmunizationID).HasDatabaseName($"FK_{nameof(ImmunizationRecord)}_{nameof(Immunization)}");
                //entity.HasIndex(x => x.ImmunizationID).HasDatabaseName($"FK_{nameof(ImmunizationRecord)}_{nameof(Immunization)}");


                entity.HasOne(patientImmunization => patientImmunization.PatientData).WithMany(patient => patient.ImmunizationRecords).HasConstraintName($"FK_{nameof(ImmunizationRecord)}_{nameof(Patient)}");
                
                //entity.HasOne(patientImmunization => patientImmunization.ImmunizationDesc).WithOne(immunization => immunization.Immuniza).HasConstraintName($"FK_{nameof(ImmunizationRecord)}_{nameof(Immunization)}");

            });

        }
    }
}
