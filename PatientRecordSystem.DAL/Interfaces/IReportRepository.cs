using System.Threading.Tasks;
using PatientRecordSystem.DAL.Models;

namespace PatientRecordSystem.DAL.Interfaces
{
    public interface IReportRepository
    {
        Task<MetaReport> GetMetaReport();
    }
}
