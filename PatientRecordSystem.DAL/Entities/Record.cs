using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientRecordSystem.DAL.Entities
{
    public class Record
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string DiseaseName { get; set; }

        public string Description { get; set; }

        public DateTime TimeOfEntry { get; set; }

        [Column(TypeName = "decimal(8,2)")]
        public decimal Bill { get; set; }

        [Required]
        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }
    }
}
