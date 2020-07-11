using System.Threading.Tasks;
using AutoMapper;
using PatientRecordSystem.BLL.Interfaces;
using PatientRecordSystem.BLL.Models;
using PatientRecordSystem.DAL.Interfaces;

namespace PatientRecordSystem.BLL
{
    public class ReportFacade : IReportFacade
    {
        private readonly IReportRepository _reportRepository;

        private readonly IMapper _mapper;

        public ReportFacade(IReportRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }

        ///<inheritdoc/>
        public async Task<PatientReport> GetPatientReport(int id)
        {
            var dalPatientReport = await _reportRepository.GetPatientReport(id);

            var patientReport = _mapper.Map<DAL.Models.PatientReport, PatientReport>(dalPatientReport);

            return patientReport;
        }

        ///<inheritdoc/>
        public async Task<MetaReport> GetMetaReport()
        {
            var dalMetaReport = await _reportRepository.GetMetaReport();

            var metaReport = _mapper.Map<DAL.Models.MetaReport, MetaReport>(dalMetaReport);

            return metaReport;
        }
    }
}
