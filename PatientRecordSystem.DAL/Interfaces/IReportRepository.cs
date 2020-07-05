using System.Threading.Tasks;
using PatientRecordSystem.DAL.Models;

namespace PatientRecordSystem.DAL.Interfaces
{
    public interface IReportRepository
    {
        /// <summary>
        /// Gets the report for a patient with a given id
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <returns>The patient report</returns>
        Task<PatientReport> GetPatientReport(int id);

        /// <summary>
        /// Gets the meta report
        /// </summary>
        /// <returns>The meta report</returns>
        Task<MetaReport> GetMetaReport();
    }
}
