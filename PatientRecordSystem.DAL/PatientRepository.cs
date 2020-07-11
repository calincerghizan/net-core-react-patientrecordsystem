using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PatientRecordSystem.DAL.Entities;
using PatientRecordSystem.DAL.Interfaces;
using PatientRecordSystem.DAL.Models;

namespace PatientRecordSystem.DAL
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PatientRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        ///<inheritdoc/>
        public async Task<Patient> GetPatientById(int id)
        {
            var patient = await _applicationDbContext.Patients.Where(x => x.Id == id).Include("MetaData").FirstOrDefaultAsync();

            return patient;
        }

        public async Task<List<ListedPatient>> GetPatients()
        {
            var patients = await _applicationDbContext.ListedPatients
                .FromSqlRaw(@"SELECT p.Id, p.Name, p.DateOfBirth, MAX(r.TimeOfEntry) AS LastEntry, COUNT(m.Id) AS MetaDataCount
                            FROM Patients p
                            LEFT JOIN MetaData m
                            ON p.Id = m.PatientId
                            LEFT JOIN Records r
                            ON p.Id = r.PatientId
                            GROUP BY p.Id, p.Name, p.DateOfBirth").ToListAsync();

            return patients;
        }

        ///<inheritdoc/>
        public async Task<Patient> CreatePatient(Patient patient)
        {
            await _applicationDbContext.Patients.AddAsync(patient);
            await _applicationDbContext.SaveChangesAsync();

            return patient;
        }

        ///<inheritdoc/>
        public async Task<int> UpdatePatient(Patient patient)
        {
            var existingPatient = await _applicationDbContext.Patients
                .Where(p => p.Id == patient.Id)
                .Include(p => p.MetaData)
                .SingleOrDefaultAsync();

            if (existingPatient != null)
            {
                // Update patient
                _applicationDbContext.Entry(existingPatient).CurrentValues.SetValues(patient);

                // Delete meta data
                foreach (var existingMetaData in existingPatient.MetaData)
                {
                    if (patient.MetaData.All(mt => mt.Id != existingMetaData.Id))
                    {
                        _applicationDbContext.MetaData.Remove(existingMetaData);
                    }
                }

                var auxMetaData = new List<MetaData>();

                // Update and insert meta data
                foreach (var metaData in patient.MetaData)
                {
                    metaData.PatientId = existingPatient.Id;

                    var existingMetaData = existingPatient.MetaData.SingleOrDefault(mt => mt.Id == metaData.Id);

                    if (existingMetaData != null)
                    {
                        // Update meta data
                        _applicationDbContext.Entry(existingMetaData).CurrentValues.SetValues(metaData);
                    }
                    else
                    {
                        // Insert meta data
                        var newMetaData = new MetaData
                        {
                            Key = metaData.Key,
                            Value = metaData.Value,
                            PatientId = metaData.PatientId
                        };

                        auxMetaData.Add(newMetaData);
                    }
                }

                if (auxMetaData.Count > 0)
                {
                    existingPatient.MetaData.AddRange(auxMetaData);
                }
            }

            return await _applicationDbContext.SaveChangesAsync();
        }
    }
}
