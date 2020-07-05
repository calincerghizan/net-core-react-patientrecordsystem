using PatientRecordSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PatientRecordSystem.DAL
{
    public interface IReportRepository
    {
        Task<MetaReport> GetMetaReport();
    }
}
