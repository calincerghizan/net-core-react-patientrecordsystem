using System;
using System.Collections.Generic;
using System.Text;

namespace PatientRecordSystem.BLL.Models
{
    public class MetaReport
    {
        public decimal MetaUsedAverage { get; set; }

        public int MetaUsedMax { get; set; }

        public List<KeyOccurrence> TopThreeUsedKeys { get; set; }
    }
}
