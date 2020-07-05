using System;
using System.Collections.Generic;
using System.Text;
using PatientRecordSystem.DAL.Entities;

namespace PatientRecordSystem.DAL.Models
{
    public class PatientReport
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public decimal BillsAverage { get; set; }

        public decimal BillsAverageNoOutliner { get; set; }

        public Record FifthRecord { get; set; }

        public List<Patient> PatientWithSimilarDiseases { get; set; }

        public string MonthWithMaxVisits { get; set; }

    }
}
