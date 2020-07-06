using System;
using System.Collections.Generic;
using System.Text;

namespace PatientRecordSystem.BLL.Models
{
    public class PatientReport
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? Age { get; set; }

        public decimal BillsAverage { get; set; }

        public decimal? BillsAverageNoOutlier { get; set; }

        public Record FifthRecord { get; set; }

        public List<Patient> PatientsWithSimilarDiseases { get; set; }

        public string MonthWithMaxVisits { get; set; }

    }
}
