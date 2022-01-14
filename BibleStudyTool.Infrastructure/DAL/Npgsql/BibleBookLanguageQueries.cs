using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.JoinEntities;
using Npgsql;

namespace BibleStudyTool.Infrastructure.DAL.Npgsql
{
    public class BibleBookLanguageQueries : EntityQueries
    {
        public BibleBookLanguageQueries(string connectionString)
            : base(connectionString)
        {
        }

        /// <summary>
        ///     Selects all BibleBooks with the given language and formatting
        ///     style.
        /// </summary>
        /// <param name="languageCode"></param>
        /// <returns>
        ///     The bible versions in the given language and formatting style.
        /// </returns>
        public async Task<IEnumerable
            <(BibleBook, BibleBookLanguage, BibleBookAbbreviationLanguage)>>
            SelectBibleBooks
                (string languageCode, string style)
        {
            using (var sqlCnx = GetConnection())
            using (var sqlCmd = new NpgsqlCommand(string.Empty, sqlCnx))
            {
                sqlCmd.CommandText = @"
SELECT *
FROM ""BibleBooks"" bb
JOIN ""BibleBookLanguage"" bbl
ON bb.BibleBookId = bbl.BibleBookId
AND bbl.LanguageCode = @LanguageCode
AND bbl.BibleBookNameStyle = @Style
JOIN ""BibleBookAbbreviationLanguage"" bbal
ON bb.BibleBookId = bball.BibleBookId
AND bbal.LanguageCode = @LanguageCode
AND bbal.BibleBookAbbreviationStyle = @Style;
";
                DbUtilties.AddNonEmptyVarcharParameter
                    (sqlCmd, "@LanguageCode", languageCode);

                DbUtilties.AddNonEmptyVarcharParameter
                    (sqlCmd, "@Style", style);

                IList
                    <(BibleBook, BibleBookLanguage,
                    BibleBookAbbreviationLanguage)>
                        bibleBooks =
                            new List
                                <(BibleBook,
                                BibleBookLanguage,
                                BibleBookAbbreviationLanguage)>();

                using (var reader = await sqlCmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var bibleVersionId =
                            DbUtilties.GetInt32OrDefault
                                (reader, "BibleVersionId");

                        bibleBooks.Add
                            ((new BibleBook
                                (DbUtilties.GetInt32OrDefault
                                    (reader, "BibleBookId"),
                                 DbUtilties.GetStringOrDefault
                                    (reader, "BibleBookDefautAbbreviation"),
                                DbUtilties.GetStringOrDefault
                                    (reader, "BibleBookDefaultName"),
                                DbUtilties.GetInt32OrDefault
                                    (reader, "BibleBookOrder"),
                                DbUtilties.GetBool
                                    (reader, "IsNewTestament")),
                            new BibleBookLanguage
                                (DbUtilties.GetInt32OrDefault
                                    (reader, "BibleBookId"),
                                DbUtilties.GetStringOrDefault
                                    (reader, "LanguageCode"),
                                DbUtilties.GetStringOrDefault
                                    (reader, "BibleBookName"),
                                DbUtilties.GetStringOrDefault
                                    (reader, "BibleBookNameStyle")),
                            new BibleBookAbbreviationLanguage
                                (DbUtilties.GetInt32OrDefault
                                    (reader, "BibleBookId"),
                                DbUtilties.GetStringOrDefault
                                    (reader, "LanguageCide"),
                                DbUtilties.GetStringOrDefault
                                    (reader, "BibleBookAbbreviation"),
                                DbUtilties.GetStringOrDefault
                                    (reader, "BibleBookAbbreviationStyle"))));
                    }
                }
                return bibleBooks;
            }
        }
    }
}
