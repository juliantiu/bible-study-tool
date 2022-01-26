using BibleStudyTool.Core.Entities;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleStudyTool.Infrastructure.DAL.Npgsql
{
    public class NoteVerseReferenceQueries : EntityQueries
    {
        public NoteVerseReferenceQueries(string connectionString) : base (connectionString) { }

        public async Task<IEnumerable<NoteVerseReference>> GetNoteVerseReferences(int[] noteIds)
        {
            using (var sqlCnx = GetConnection())
            using (var sqlCmd = new NpgsqlCommand(string.Empty, sqlCnx))
            {
                sqlCmd.CommandText = @"
SELECT * 
FROM ""NoteVerseReferences""
WHERE ""NoteId"" = ANY (@NoteIds)
ORDER BY ""NoteId"";
";
                DbUtilties.AddInt32ArrayParameter(sqlCmd, "@NoteIds", noteIds);

                var noteVerseReferences = new List<NoteVerseReference>();
                using (var reader = await sqlCmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        noteVerseReferences.Add(new NoteVerseReference(DbUtilties.GetInt32OrDefault(reader, "NoteId"),
                                                                       DbUtilties.GetStringOrDefault(reader, "BibleBook"),
                                                                       DbUtilties.GetInt32OrDefault(reader, "BookChapter"),
                                                                       DbUtilties.GetInt32OrDefault(reader, "ChapterVerseNumber")));
                    }
                }
                return noteVerseReferences;
            }
        }
    }
}
