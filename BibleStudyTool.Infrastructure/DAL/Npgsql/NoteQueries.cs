using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using Npgsql;

namespace BibleStudyTool.Infrastructure.DAL.Npgsql
{
    public class NoteQueries : EntityQueries
    {
        public NoteQueries(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<Note>> GetChapterNotesQueryAsync
            (string uid, int bibleBookId, int chapterNumber)
        {
            using (var sqlCnx = GetConnection())
            using (var sqlCmd = new NpgsqlCommand(string.Empty, sqlCnx))
            {
                sqlCmd.CommandText = @"
SELECT
	DISTINCT ""Notes"".""NoteId"" as ""NoteId"",
    ""NoteUid"",
	""NoteSummary"",
	""NoteText""
FROM ""Notes""
JOIN ""NoteReferences""
ON ""Notes"".""NoteId"" = ""NoteReferences"".""NoteId""
AND ""Notes"".""NoteUid"" = @NoteUid
JOIN(
    SELECT ""BibleVerseId""

    FROM ""BibleVerses""

    WHERE ""BibleBookId"" = @BibleBookId AND ""ChapterNumber"" = @ChapterNumber
) bible_verse_ids
ON ""NoteReferences"".""ReferencedBibleVerseId"" = bible_verse_ids.""BibleVerseId""
";

                DbUtilties.AddNonEmptyVarcharParameter(sqlCmd, "@NoteUid", uid);
                DbUtilties.AddInt32Parameter(sqlCmd, "@BibleBookId", bibleBookId);
                DbUtilties.AddInt32Parameter(sqlCmd, "@ChapterNumber", chapterNumber);

                var notes = new List<Note>();
                using (var reader = await sqlCmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        notes.Add(new Note(DbUtilties.GetInt32OrDefault(reader, "NoteId"),
                                           DbUtilties.GetStringOrDefault(reader, "NoteUid"),
                                           DbUtilties.GetStringOrDefault(reader, "NoteSummary"),
                                           DbUtilties.GetStringOrDefault(reader, "NoteText")));
                    }
                }
                return notes;
            }
        }
    }
}
