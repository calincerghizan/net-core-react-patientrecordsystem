using System;
using System.Collections.Generic;

namespace PatientRecordSystem.BLL.Models
{
    public class Patient
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string OfficialId { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Email { get; set; }

        public List<MetaData> MetaData { get; set; }

        public List<Record> Records { get; set; }
    }
}
