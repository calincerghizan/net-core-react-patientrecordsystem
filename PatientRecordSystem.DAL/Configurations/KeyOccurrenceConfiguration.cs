using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatientRecordSystem.DAL.Models;

namespace PatientRecordSystem.DAL.Configurations
{
    public class KeyOccurrenceConfiguration : IEntityTypeConfiguration<KeyOccurrence>
    {
        public void Configure(EntityTypeBuilder<KeyOccurrence> builder)
        {
            builder.HasNoKey();
        }
    }
}
