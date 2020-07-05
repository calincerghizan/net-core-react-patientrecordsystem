using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PatientRecordSystem.BLL.Models;

namespace PatientRecordSystem.BLL
{
    public interface IRecordFacade
    {
        /// <summary>
        /// Gets a record for a given id
        /// </summary>
        /// <param name="id">The record id</param>
        /// <returns>The record</returns>
        Task<Record> GetRecordById(int id);

        /// <summary>
        /// Gets all the records
        /// </summary>
        /// <returns>The list of records</returns>
        Task<List<ListedRecord>> GetRecords();

        /// <summary>
        /// Adds a new record in the database
        /// </summary>
        /// <param name="record">The record model containing the data to insert</param>
        /// <returns>The added record</returns>
        Task<Record> CreateRecord(Record record);

        /// <summary>
        /// Updates an existing record
        /// </summary>
        /// <param name="recordToBeUpdated">The record to be updated</param>
        /// <param name="record">The record model containing the new data to update</param>
        /// <returns>The updated record</returns>
        Task UpdateRecord(Record recordToBeUpdated, Record record);
    }
}
