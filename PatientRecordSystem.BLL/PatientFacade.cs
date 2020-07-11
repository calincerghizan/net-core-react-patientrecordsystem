﻿using PatientRecordSystem.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using PatientRecordSystem.BLL.Interfaces;
using PatientRecordSystem.DAL.Interfaces;

namespace PatientRecordSystem.BLL
{
    public class PatientFacade : IPatientFacade
    {
        private readonly IPatientRepository _patientRepository;

        private readonly IMapper _mapper;

        public PatientFacade(IPatientRepository patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        ///<inheritdoc/>
        public async Task<Patient> GetPatientById(int id)
        {
            var dalPatient = await _patientRepository.GetPatientById(id);

            var patient = _mapper.Map<DAL.Entities.Patient, Patient>(dalPatient);

            return patient;
        }

        ///<inheritdoc/>
        public async Task<List<ListedPatient>> GetPatients()
        {
            // TODO: Add pagination with size of the page, page number (if not provided, default values), sorting by all columns

            var dalPatientList = await _patientRepository.GetPatients();

            var patientList = _mapper.Map<List<DAL.Models.ListedPatient>, List<ListedPatient>>(dalPatientList);

            return patientList;
        }

        ///<inheritdoc/>
        public async Task<Patient> CreatePatient(Patient patient)
        {
            var dalPatient = _mapper.Map<Patient, DAL.Entities.Patient>(patient);

            var insertPatient = await _patientRepository.CreatePatient(dalPatient);

            var insertedPatient = await _patientRepository.GetPatientById(insertPatient.Id);

            var bllPatient = _mapper.Map<DAL.Entities.Patient, Patient>(insertedPatient);

            return bllPatient;
        }

        ///<inheritdoc/>
        public async Task UpdatePatient(Patient patient)
        {
            var dalPatient = _mapper.Map<Patient, DAL.Entities.Patient>(patient);

            await _patientRepository.UpdatePatient(dalPatient);
        }
    }
}
