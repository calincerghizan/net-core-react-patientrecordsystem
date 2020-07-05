using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PatientRecordSystem.BLL.Models;

namespace PatientRecordSystem.BLL
{
    public interface IReportFacade
    {
        /// <summary>
        /// Gets the report for a Patient with a given Id
        /// </summary>
        /// <param name="id">The id of the patient</param>
        /// <returns>The report of the patient</returns>
        Task<PatientReport> GetPatientReport(int id);

        /// <summary>
        /// Gets the MetaData report
        /// </summary>
        /// <returns>The MetaData report</returns>
        Task<MetaReport> GetMetaReport();
    }
}
