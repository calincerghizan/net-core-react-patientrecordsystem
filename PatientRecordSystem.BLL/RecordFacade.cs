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

        public async Task<Record> CreateRecord(Record record)
        {
            var dalRecord = _mapper.Map<Record, DAL.Entities.Record>(record);

            var insertRecord = await _recordRepository.CreateRecord(dalRecord);

            var insertedRecord = await _recordRepository.GetRecordById(insertRecord.Id);

            var bllRecord = _mapper.Map<DAL.Entities.Record, Record>(insertedRecord);

            return bllRecord;
        }

        public async Task UpdateRecord(Record recordToBeUpdated, Record record)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ListedRecord>> GetRecords()
        {
            var dalRecordList = await _recordRepository.GetRecords();

            var recordList = _mapper.Map<List<DAL.Models.ListedRecord>, List<ListedRecord>>(dalRecordList);

            return recordList;
        }
    }
}
