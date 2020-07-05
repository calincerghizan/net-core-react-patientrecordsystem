using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatientRecordSystem.DAL.Entities;

namespace PatientRecordSystem.DAL.Configurations
{
    public class RecordConfiguration : IEntityTypeConfiguration<Record>
    {
        public void Configure(EntityTypeBuilder<Record> builder)
        {
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.TimeOfEntry).HasDefaultValueSql("getdate()");

            builder.HasOne(x => x.Patient).WithMany(i => i.Records);

            builder.ToTable("Records");
        }
    }
}
