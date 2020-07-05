using Microsoft.EntityFrameworkCore;
using PatientRecordSystem.DAL.Configurations;
using PatientRecordSystem.DAL.Entities;
using PatientRecordSystem.DAL.Models;

namespace PatientRecordSystem.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }

        public DbSet<MetaData> MetaData { get; set; }

        public DbSet<Record> Records { get; set; }

        public DbSet<ListedPatient> ListedPatients { get; set; }

        public DbSet<KeyOccurrence> KeyOccurrences { get; set; }

        public DbSet<ListedRecord> ListedRecords { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new PatientConfiguration());

            builder.ApplyConfiguration(new MetaDataConfiguration());

            builder.ApplyConfiguration(new RecordConfiguration());

            builder.ApplyConfiguration(new ListedPatientConfiguration());

            builder.ApplyConfiguration(new KeyOccurrenceConfiguration());

            builder.ApplyConfiguration(new ListedRecordConfiguration());
        }
    }
}
