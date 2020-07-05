using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PatientRecordSystem.BLL.Models;
using PatientRecordSystem.DAL;

namespace PatientRecordSystem.BLL
{
    public class RecordFacade : IRecordFacade
    {
        private readonly IRecordRepository _recordRepository;

        private readonly IMapper _mapper;

        public RecordFacade(IRecordRepository recordRepository, IMapper mapper)
        {
            _recordRepository = recordRepository;
            _mapper = mapper;
        }

        ///<inheritdoc/>
        public async Task<Record> GetRecordById(int id)
        {
            var dalRecord = await _recordRepository.GetRecordById(id);

            var record = _mapper.Map<DAL.Entities.Record, Record>(dalRecord);

            return record;
        }

        ///<inheritdoc/>
        public async Task<List<ListedRecord>> GetRecords()
        {
            var dalRecordList = await _recordRepository.GetRecords();

            var recordList = _mapper.Map<List<DAL.Models.ListedRecord>, List<ListedRecord>>(dalRecordList);

            return recordList;
        }

        ///<inheritdoc/>
        public async Task<Record> CreateRecord(Record record)
        {
            var dalRecord = _mapper.Map<Record, DAL.Entities.Record>(record);

            var insertRecord = await _recordRepository.CreateRecord(dalRecord);

            var insertedRecord = await _recordRepository.GetRecordById(insertRecord.Id);

            var bllRecord = _mapper.Map<DAL.Entities.Record, Record>(insertedRecord);

            return bllRecord;
        }

        ///<inheritdoc/>
        public async Task UpdateRecord(Record recordToBeUpdated, Record record)
        {
            var dalRecordToBeUpdated = _mapper.Map<Record, DAL.Entities.Record>(recordToBeUpdated);

            dalRecordToBeUpdated.DiseaseName = record.DiseaseName;
            dalRecordToBeUpdated.TimeOfEntry = record.TimeOfEntry ?? DateTime.Now;
            dalRecordToBeUpdated.Description = record.Description;
            dalRecordToBeUpdated.Bill = record.Bill;

            await _recordRepository.UpdateRecord(dalRecordToBeUpdated);
        }
    }
}
