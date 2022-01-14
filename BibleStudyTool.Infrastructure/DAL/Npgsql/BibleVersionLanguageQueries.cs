using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.JoinEntities;
using Npgsql;

namespace BibleStudyTool.Infrastructure.DAL.Npgsql
{
    public class BibleVersionLanguageQueries : EntityQueries
    {
        public BibleVersionLanguageQueries(string connectionString)
            : base(connectionString)
        {
        }

        /// <summary>
        ///     Selects all BibleVersion with the given language.
        /// </summary>
        /// <param name="languageCode"></param>
        /// <returns>
        /// The bible versions in the given language.
        /// </returns>
        public async Task<IEnumerable<(BibleVersion, BibleVersionLanguage)>>
            SelectBibleVersionInLanguag
            (string languageCode)
        {
            using (var sqlCnx = GetConnection())
            using (var sqlCmd = new NpgsqlCommand(string.Empty, sqlCnx))
            {
                sqlCmd.CommandText = @"
SELECT *
FROM ""BibleVersions"" bv
JOIN ""BibleVersionLanguage"" bvl
ON bv.BibleVersionId = bvl.BibleVersionId
AND ""LanguageCode"" = @LanguageCode;
";
                DbUtilties.AddNonEmptyVarcharParameter
                    (sqlCmd, "@LanguageCode", languageCode);

                IList<(BibleVersion, BibleVersionLanguage)> bibleVersions =
                    new List<(BibleVersion, BibleVersionLanguage)>();

                using (var reader = await sqlCmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var bibleVersionId =
                            DbUtilties.GetInt32OrDefault
                                (reader, "BibleVersionId");

                        bibleVersions.Add
                            ((new BibleVersion
                                (bibleVersionId,
                                DbUtilties.GetStringOrDefault
                                    (reader, "DefaultName"),
                                DbUtilties.GetStringOrDefault
                                    (reader, "DefaultAbbreviation")),
                            (new BibleVersionLanguage
                                (bibleVersionId,
                                DbUtilties.GetStringOrDefault
                                    (reader, "LanguageCode"),
                                DbUtilties.GetStringOrDefault
                                    (reader, "BibleVersionName"),
                                DbUtilties.GetStringOrDefault
                                    (reader, "BibleVersionAbbreviation")))));
                    }
                }
                return bibleVersions;
            }
        }
    }
}
