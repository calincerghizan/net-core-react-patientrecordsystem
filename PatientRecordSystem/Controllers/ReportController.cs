using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PatientRecordSystem.BLL;
using PatientRecordSystem.BLL.Interfaces;
using PatientRecordSystem.BLL.Models;

namespace PatientRecordSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportFacade _reportFacade;

        public ReportController(IReportFacade reportFacade)
        {
            _reportFacade = reportFacade;
        }

        [HttpGet("Patient/{id}")]
        public async Task<ActionResult<PatientReport>> GetPatientReport(int id)
        {
            var patientReport = await _reportFacade.GetPatientReport(id);

            return Ok(patientReport);
        }

        [HttpGet("Meta")]
        public async Task<ActionResult<MetaReport>> GetMetaReport()
        {
            var metaReport = await _reportFacade.GetMetaReport();

            return Ok(metaReport);
        }
    }
}
