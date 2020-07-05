using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PatientRecordSystem.DAL.Entities
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string OfficialId { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Email { get; set; }

        public virtual List<MetaData> MetaData { get; set; }

        public virtual List<Record> Records { get; set; }
    }
}
