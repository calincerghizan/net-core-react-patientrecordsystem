using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PatientRecordSystem.BLL.Models;
using PatientRecordSystem.DAL;

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
            throw new NotImplementedException();
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
