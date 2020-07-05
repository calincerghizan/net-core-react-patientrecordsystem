using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PatientRecordSystem.BLL.Models;

namespace PatientRecordSystem.BLL
{
    public interface IPatientFacade
    {
        /// <summary>
        /// Gets a patient for a given id
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <returns>The patient</returns>
        Task<Patient> GetPatientById(int id);

        /// <summary>
        /// Gets all the patients
        /// </summary>
        /// <returns>The list of patients</returns>
        Task<List<ListedPatient>> GetPatients();

        /// <summary>
        /// Adds a new patient in the database
        /// </summary>
        /// <param name="patient">The patient model containing the data to insert</param>
        /// <returns>The added patient</returns>
        Task<Patient> CreatePatient(Patient patient);

        /// <summary>
        /// Updates an existing patient
        /// </summary>
        /// <param name="patientToBeUpdated">The patient to be updated</param>
        /// <param name="patient">The patient model containing the new data to update</param>
        /// <returns>The updated patient</returns>
        Task UpdatePatient(Patient patientToBeUpdated, Patient patient);
    }
}
