using System;
using System.Collections.Generic;
using System.Text;

namespace PatientRecordSystem.DAL.Models
{
    public class ListedPatient
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public DateTime? LastEntry { get; set; }

        public int MetaDataCount { get; set; }
    }
}
