using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.JoinEntities;
using Npgsql;

namespace BibleStudyTool.Infrastructure.DAL.Npgsql
{
    public class TagNoteQueries : EntityQueries
    {
        public TagNoteQueries(string connectionString) : base(connectionString)
        {
        }

        public async Task<IDictionary<int, IList<Tag>>> GetTagsForNotesQueryAsync(int[] noteIds)
        {
            using (var sqlCnx = GetConnection())
            using (var sqlCmd = new NpgsqlCommand(string.Empty, sqlCnx))
            {
                sqlCmd.CommandText = @"
SELECT
	""NoteId"",
    ""Tags"".""TagId"",
	""TagColor"",
	""TagLabel"",
	""TagUid""
FROM ""TagNotes""
JOIN ""Tags""
ON ""NoteId"" = ANY(@NoteIds)
AND ""Tags"".""TagId"" = ""TagNotes"".""TagId""
ORDER BY ""NoteId"" ASC
";
                DbUtilties.AddInt32ArrayParameter(sqlCmd, "@NoteIds", noteIds);

                var noteTagsMapping = new Dictionary<int, IList<Tag>>();
                using (var reader = await sqlCmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var noteId = DbUtilties.GetInt32OrDefault(reader, "NoteId");
                        if (!(noteTagsMapping.ContainsKey(noteId)))
                        {
                            noteTagsMapping[noteId] = new List<Tag>();
                        }

                        noteTagsMapping[noteId].Add(new Tag(DbUtilties.GetInt32OrDefault(reader, "TagId"),
                                                            DbUtilties.GetStringOrDefault(reader, "TagUid"),
                                                            DbUtilties.GetStringOrDefault(reader, "TagLabel"),
                                                            DbUtilties.GetStringOrDefault(reader, "TagColor")));
                    }
                }
                return noteTagsMapping;
            }
        }
    }
}
