using PatientRecordSystem.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PatientRecordSystem.DAL.Interfaces;
using PatientRecordSystem.DAL.Models;

namespace PatientRecordSystem.DAL
{
    public class RecordRepository : IRecordRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public RecordRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        ///<inheritdoc/>
        public async Task<Record> GetRecordById(int id)
        {
            var record = await _applicationDbContext.Records.FindAsync(id);

            return record;
        }

        ///<inheritdoc/>
        public async Task<List<ListedRecord>> GetRecords()
        {
            var records = await _applicationDbContext.ListedRecords
                .FromSqlRaw(@"SELECT r.Id AS Id, p.Id AS PatientId, p.Name AS PatientName, r.DiseaseName, r.TimeOfEntry
                            FROM Records r
                            LEFT JOIN Patients p
                            ON r.PatientId = p.Id").ToListAsync();

            return records;
        }

        ///<inheritdoc/>
        public async Task<Record> CreateRecord(Record record)
        {
            await _applicationDbContext.Records.AddAsync(record);
            await _applicationDbContext.SaveChangesAsync();

            return record;
        }

        ///<inheritdoc/>
        public async Task<int> UpdateRecord(Record record)
        {
            var currentRecord = _applicationDbContext.Records.FirstOrDefault(x => x.Id == record.Id);

            _applicationDbContext.Entry(currentRecord).CurrentValues.SetValues(record);

            return await _applicationDbContext.SaveChangesAsync();
        }
    }
}
