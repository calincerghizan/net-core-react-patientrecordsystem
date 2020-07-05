using System;
using System.Collections.Generic;
using System.Text;

namespace PatientRecordSystem.DAL.Models
{
    public class ListedRecord
    {
        public int PatientId { get; set; }

        public string PatientName { get; set; }

        public string DiseaseName { get; set; }

        public DateTime TimeOfEntry { get; set; }
    }
}
