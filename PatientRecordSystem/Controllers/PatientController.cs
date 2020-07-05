using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientRecordSystem.BLL;
using PatientRecordSystem.BLL.Interfaces;
using PatientRecordSystem.BLL.Models;

namespace PatientRecordSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientFacade _patientFacade;

        public PatientController(IPatientFacade patientFacade)
        {
            _patientFacade = patientFacade;
        }

        /// <summary>
        /// Gets all the patients
        /// </summary>
        /// <returns>The list of patients</returns>
        [HttpGet("")]
        public async Task<ActionResult<List<ListedPatient>>> GetPatients()
        {
            var patientList = await _patientFacade.GetPatients();

            return Ok(patientList);
        }

        /// <summary>
        /// Gets the patient with the given {id}
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <returns>The patient with the given {id}</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatientById(int id)
        {
            var patient = await _patientFacade.GetPatientById(id);

            if (patient == null)
            {
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }

            return Ok(patient);
        }

        /// <summary>
        /// Adds a new patient in the database
        /// </summary>
        /// <param name="patient">The patient model containing the data to insert</param>
        /// <returns>The created patient</returns>
        [HttpPost("")]
        public async Task<ActionResult<Patient>> CreatePatient([FromBody] Patient patient)
        {
            if (patient == null)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            var insertedPatient = await _patientFacade.CreatePatient(patient);

            return Ok(insertedPatient);
        }

        /// <summary>
        /// Updates an existing patient with a given id
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="patient">The patient model containing the data to update</param>
        /// <returns>The updated patient</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<Patient>> UpdatePatient(int id, [FromBody] Patient patient)
        {
            var patientToBeUpdated = await _patientFacade.GetPatientById(id);

            if (patientToBeUpdated == null)
                return new StatusCodeResult(StatusCodes.Status404NotFound);

            await _patientFacade.UpdatePatient(patientToBeUpdated, patient);

            var updatedPatient =  _patientFacade.GetPatientById(id);

            if (updatedPatient == null)
                return new StatusCodeResult(StatusCodes.Status404NotFound);

            return Ok(updatedPatient);
        }
    }
}
