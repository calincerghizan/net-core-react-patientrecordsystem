using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PatientRecordSystem.DAL.Models;

namespace PatientRecordSystem.DAL
{
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ReportRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        ///<inheritdoc/>
        public async Task<MetaReport> GetMetaReport()
        {
            var metaReport = new MetaReport();

            // MetaUsedAverage
            await using (var command = this._applicationDbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = @"WITH MetaUsed_CTE (MetaDataCount)
                                        AS
                                        (SELECT COUNT(m.Id) AS MetaDataCount
                                        FROM Patients p
                                        LEFT JOIN MetaData m
                                        ON p.Id = m.PatientId
                                        GROUP by p.Id)
                                        SELECT CAST(AVG(MetaDataCount) AS DECIMAL (8,2))
                                        FROM MetaUsed_CTE";

                command.CommandType = CommandType.Text;

                this._applicationDbContext.Database.OpenConnection();

                metaReport.MetaUsedAverage = (decimal) command.ExecuteScalar();
            }

            // MetaUsedMax
            await using (var command = this._applicationDbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = @"WITH MetaUsed_CTE (MetaDataCount)
                                        AS
                                        (SELECT COUNT(m.Id) AS MetaDataCount
                                        FROM Patients p
                                        LEFT JOIN MetaData m
                                        ON p.Id = m.PatientId
                                        GROUP by p.Id)
                                        SELECT MAX(MetaDataCount)
                                        FROM MetaUsed_CTE";

                command.CommandType = CommandType.Text;

                this._applicationDbContext.Database.OpenConnection();

                metaReport.MetaUsedMax = (int) command.ExecuteScalar();
            }

            // TopThreeUsedKeys
            var keyOccurrences = await _applicationDbContext.KeyOccurrences
                .FromSqlRaw(
                    @"SELECT TOP 3 UPPER(LEFT(LOWER([Key]), 1)) + RIGHT(LOWER([Key]), LEN(LOWER([Key]))-1) AS [Key], COUNT([Key]) AS Occurrence
                            FROM MetaData
                            GROUP BY [Key]
                            ORDER BY Occurrence Desc").ToListAsync();

            metaReport.TopThreeUsedKeys = keyOccurrences;

            return metaReport;
        }
    }
}
