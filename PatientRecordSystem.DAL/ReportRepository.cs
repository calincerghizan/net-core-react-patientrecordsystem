using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PatientRecordSystem.DAL.Entities;
using PatientRecordSystem.DAL.Interfaces;
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
        public async Task<PatientReport> GetPatientReport(int id)
        {
            var patientReport = new PatientReport();

            // Id, Name, Age, BillsAverage
            var patientReportInit = await _applicationDbContext.PatientReports
                .FromSqlRaw(@"SELECT p.Id, p.Name, DATEDIFF(yy, 
                                CONVERT(DATETIME, p.DateOfBirth), GETDATE()) AS Age, 
                                CAST(AVG(r.Bill) AS DECIMAL(8, 2)) AS BillsAverage, 
                                null AS FifthRecordId, null AS MonthWithMaxVisits
                            FROM Patients p
                            JOIN Records r
                            ON p.Id = r.PatientId
                            GROUP BY p.Id, p.Name, p.DateOfBirth
                            HAVING p.Id = {0}", id)
                .FirstOrDefaultAsync();

            patientReport.Id = patientReportInit.Id;
            patientReport.Name = patientReportInit.Name;
            patientReport.Age = patientReportInit.Age;
            patientReport.BillsAverage = patientReportInit.BillsAverage;

            //TODO: BillsAverageNoOutlier

            // FifthRecord
            if (_applicationDbContext.Records.Count(x => x.PatientId == id) > 4)
            {
                patientReport.FifthRecord = await _applicationDbContext.Records.Where(x => x.PatientId == id).Skip(4)
                    .FirstOrDefaultAsync();
            }

            // MonthWithMaxVisits
            patientReport.MonthWithMaxVisits = await MonthWithMaxVisits(id);

            // PatientsWithSimilarDiseases
            var patientIds = await PatientsWithSimilarDiseases(id);
            if (patientIds.Any())
            {
                patientReport.PatientsWithSimilarDiseases = await _applicationDbContext.Patients.Where(x => patientIds.Contains(x.Id)).ToListAsync();
            }

            return patientReport;
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

        #region Private methods

        private async Task<string> MonthWithMaxVisits(int id)
        {
            await using var command = this._applicationDbContext.Database.GetDbConnection().CreateCommand();
            command.CommandText = @"WITH MOnthOccur_CTE ([Month])
                                        AS
                                        (SELECT FORMAT(TimeOfEntry, 'MMMM') AS [Month] 
                                        FROM Records 
                                        WHERE PatientId = @id)
                                        SELECT TOP 1 [Month] FROM MOnthOccur_CTE
                                        GROUP BY [Month] 
                                        ORDER BY COUNT([Month]) DESC";

            command.CommandType = CommandType.Text;

            var param = new SqlParameter { ParameterName = "id", Value = id };
            command.Parameters.Add(param);

            this._applicationDbContext.Database.OpenConnection();

            return command.ExecuteScalar().ToString();
        }

        private async Task<List<int>> PatientsWithSimilarDiseases(int id)
        {
            var patientIds = new List<int>();
            await using (var command = this._applicationDbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = @"WITH CommonDiseases_CTE (DiseaseName, PatientId)
                                    AS
                                    (SELECT r1.DiseaseName, r2.PatientId 
                                    FROM Records r1
                                    JOIN Records r2
                                    ON r1.DiseaseName = r2.DiseaseName
                                    GROUP BY r1.DiseaseName, r1.PatientId, r2.PatientId
                                    HAVING r1.PatientId = @id and r1.PatientId <> r2.PatientId)
                                    SELECT PatientId 
                                    FROM CommonDiseases_CTE
                                    GROUP BY PatientId
                                    HAVING COUNT(PatientId) >= 2";

                command.CommandType = CommandType.Text;

                var param = new SqlParameter { ParameterName = "id", Value = id };
                command.Parameters.Add(param);

                this._applicationDbContext.Database.OpenConnection();

                await using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    patientIds.Add(reader.GetInt32(default(int)));
                }
            }

            return patientIds;
        }

        #endregion Private methods
    }
}
