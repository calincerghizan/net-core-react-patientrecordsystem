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

        [HttpGet("")]
        public async Task<ActionResult<List<ListedRecord>>> GetRecords()
        {
            var recordList = await _recordFacade.GetRecords();

            return Ok(recordList);
        }

        /// <summary>
        /// Gets the record with the given {id}
        /// </summary>
        /// <param name="id">The record id</param>
        /// <returns>The record with the given {id}</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Record>> GetRecordById(int id)
        {
            var record = await _recordFacade.GetRecordById(id);

            if (record == null)
            {
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }

            return Ok(record);
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

            var insertedRecord = await _recordFacade.CreateRecord(record);

            return Ok(insertedRecord);
        }

        /// <summary>
        /// Updates an existing record in the database
        /// </summary>
        /// <param name="id">The record id</param>
        /// <param name="record">The record model containing the data to update</param>
        /// <returns>The updated record</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<Record>> UpdateRecord(int id, [FromBody] Record record)
        {
            var recordToBeUpdated = await _recordFacade.GetRecordById(id);

            if (recordToBeUpdated == null)
                return new StatusCodeResult(StatusCodes.Status404NotFound);

            await _recordFacade.UpdateRecord(recordToBeUpdated, record);

            var updatedRecord = _recordFacade.GetRecordById(id);

            if (updatedRecord == null)
                return new StatusCodeResult(StatusCodes.Status404NotFound);

            return Ok(updatedRecord);
        }
    }
}
