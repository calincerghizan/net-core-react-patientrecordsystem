using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientRecordSystem.BLL.Models
{
    public class Record
    {
        public int Id { get; set; }

        [Required]
        public string DiseaseName { get; set; }

        public string Description { get; set; }

        public DateTime? TimeOfEntry { get; set; }

        public decimal Bill { get; set; }

        public int PatientId { get; set; }
    }
}
