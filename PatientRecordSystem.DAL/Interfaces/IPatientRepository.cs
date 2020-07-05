using System.Collections.Generic;
using System.Threading.Tasks;
using PatientRecordSystem.DAL.Entities;
using PatientRecordSystem.DAL.Models;

namespace PatientRecordSystem.DAL.Interfaces
{
    public interface IPatientRepository
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
        /// Commits the changes
        /// </summary>
        Task<int> UpdatePatient(Patient patient);
    }
}
