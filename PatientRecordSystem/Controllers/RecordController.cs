using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientRecordSystem.BLL;
using PatientRecordSystem.BLL.Models;

namespace PatientRecordSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        private readonly IRecordFacade _recordFacade;

        public RecordController(IRecordFacade recordFacade)
        {
            _recordFacade = recordFacade;
        }

        /// <summary>
        /// Adds a new record in the database
        /// </summary>
        /// <param name="record">The record model containing the data to insert</param>
        /// <returns>The created record</returns>
        [HttpPost("")]
        public async Task<ActionResult<Record>> CreateRecord([FromBody] Record record)
        {
            if (record == null)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            var insertedPatient = await _recordFacade.CreateRecord(record);

            return Ok(insertedPatient);
        }

        [HttpGet("")]
        public async Task<ActionResult<List<ListedRecord>>> GetRecords()
        {
            var recordList = await _recordFacade.GetRecords();

            return Ok(recordList);
        }
    }
}
