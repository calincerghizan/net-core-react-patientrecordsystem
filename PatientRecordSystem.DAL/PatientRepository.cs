using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using PatientRecordSystem.DAL.Entities;
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
            var currentPatient = _applicationDbContext.Patients.FirstOrDefault(x => x.Id == patient.Id);
            var currentMetaDataList = _applicationDbContext.MetaData.Where(x => x.PatientId == patient.Id).ToList();


            _applicationDbContext.Entry(currentPatient).CurrentValues.SetValues(patient);

            for (var i = 0; i < currentMetaDataList.Count; i++)
            {
                _applicationDbContext.Entry(currentMetaDataList[i]).CurrentValues.SetValues(patient.MetaData[i]);
            }

            var auxCurrentMetaDataListCount = currentMetaDataList.Count;

            if (currentMetaDataList.Count < patient.MetaData.Count)
            {
                _applicationDbContext.MetaData.AddRange(patient.MetaData.Skip(currentMetaDataList.Count));
            }

            return await _applicationDbContext.SaveChangesAsync();
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
    }
}
