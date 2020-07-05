using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatientRecordSystem.DAL.Models;

namespace PatientRecordSystem.DAL.Configurations
{
    public class ListedPatientConfiguration : IEntityTypeConfiguration<ListedPatient>
    {
        public void Configure(EntityTypeBuilder<ListedPatient> builder)
        {
            builder.HasNoKey();
        }
    }
}
