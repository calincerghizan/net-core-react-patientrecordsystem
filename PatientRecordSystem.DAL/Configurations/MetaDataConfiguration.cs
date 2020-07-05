using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatientRecordSystem.DAL.Entities;

namespace PatientRecordSystem.DAL.Configurations
{
    public class MetaDataConfiguration : IEntityTypeConfiguration<MetaData>
    {
        public void Configure(EntityTypeBuilder<MetaData> builder)
        {
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.HasOne(x => x.Patient).WithMany(i => i.MetaData);

            builder.ToTable("MetaData");
        }
    }
}
