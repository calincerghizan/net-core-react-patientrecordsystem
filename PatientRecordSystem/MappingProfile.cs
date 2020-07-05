using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PatientRecordSystem.BLL.Models;

namespace PatientRecordSystem.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //BLL Models to DAL Entities
            CreateMap<Patient, DAL.Entities.Patient>();
            CreateMap<MetaData, DAL.Entities.MetaData>();
            CreateMap<Record, DAL.Entities.Record>();
            CreateMap<ListedPatient, DAL.Models.ListedPatient>();
            CreateMap<PatientReport, DAL.Models.PatientReport>();
            CreateMap<MetaReport, DAL.Models.MetaReport>();
            CreateMap<KeyOccurrence, DAL.Models.KeyOccurrence>();
            CreateMap<ListedRecord, DAL.Models.ListedRecord>();

            //DAL Entities to BLL Models
            CreateMap<DAL.Entities.Patient, Patient>();
            CreateMap<DAL.Entities.MetaData, MetaData>();
            CreateMap<DAL.Entities.Record, Record>();
            CreateMap<DAL.Models.ListedPatient, ListedPatient>();
            CreateMap<DAL.Models.PatientReport, PatientReport>();
            CreateMap<DAL.Models.MetaReport, MetaReport>();
            CreateMap<DAL.Models.KeyOccurrence, KeyOccurrence>();
            CreateMap<DAL.Models.ListedRecord, ListedRecord>();
        }
    }
}
