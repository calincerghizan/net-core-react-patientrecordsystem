using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatientRecordSystem.DAL.Entities;

namespace PatientRecordSystem.DAL.Configurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.HasIndex(x => x.OfficialId).IsUnique();

            builder.HasMany(x => x.MetaData).WithOne(i => i.Patient);

            builder.HasMany(x => x.Records).WithOne(i => i.Patient);

            builder.ToTable("Patients");
        }
    }
}
