using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using Npgsql;

namespace BibleStudyTool.Infrastructure.DAL.Npgsql
{
    public class LanguageQueries : EntityQueries
    {
        public LanguageQueries(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        ///     Gets all recognized languages in the database.
        /// </summary>
        /// <returns>
        ///     The list of application-recognized languages.
        /// </returns>
        public async Task<IEnumerable<Language>> SelectAllLanguages()
        {
            using (var sqlCnx = GetConnection())
            using (var sqlCmd = new NpgsqlCommand(string.Empty, sqlCnx))
            {
                sqlCmd.CommandText = @"
SELECT *
FROM ""Languages"";
";
                var languages = new List<Language>();
                using (var reader = await sqlCmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        languages.Add(DataReaderToEntity.DataReaderToLanguage(reader));
                    }
                }
                return languages;
            }
        }
    }
}
