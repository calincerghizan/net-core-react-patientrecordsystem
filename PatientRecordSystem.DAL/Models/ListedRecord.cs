using System;

namespace PatientRecordSystem.DAL.Models
{
    public class ListedRecord
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public string PatientName { get; set; }

        public string DiseaseName { get; set; }

        public DateTime TimeOfEntry { get; set; }
    }
}
