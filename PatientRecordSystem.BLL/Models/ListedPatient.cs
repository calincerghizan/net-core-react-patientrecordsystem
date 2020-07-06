using System;
using System.Text.Json.Serialization;

namespace PatientRecordSystem.BLL.Models
{
    [Serializable]
    public class ListedPatient
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public DateTime? LastEntry { get; set; }

        public int MetaDataCount { get; set; }
    }
}
