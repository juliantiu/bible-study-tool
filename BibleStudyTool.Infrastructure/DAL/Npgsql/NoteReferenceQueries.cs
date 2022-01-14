using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities.JoinEntities;
using Npgsql;

namespace BibleStudyTool.Infrastructure.DAL.Npgsql
{
    public class NoteReferenceQueries : EntityQueries
    {
        public NoteReferenceQueries(string connectionString) : base (connectionString)
        {
        }

        public async Task<IEnumerable<NoteReference>> GetNoteReferences(int[] noteIds)
        {
            using (var sqlCnx = GetConnection())
            using (var sqlCmd = new NpgsqlCommand(string.Empty, sqlCnx))
            {
                sqlCmd.CommandText = @"
SELECT * 
FROM ""NoteReferences""
WHERE ""NoteId"" = ANY (@NoteIds)
ORDER BY ""NoteId"";
";
                DbUtilties.AddInt32ArrayParameter(sqlCmd, "@NoteIds", noteIds);

                var noteReferences = new List<NoteReference>();
                using (var reader = await sqlCmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        noteReferences.Add(new NoteReference(DbUtilties.GetInt32OrDefault(reader, "NoteId"),
                                                             DbUtilties.GetInt32OrDefault(reader, "ReferencedNoteId"),
                                                             DbUtilties.GetInt32OrDefault(reader, "ReferencedBibleVerseId")));
                    }
                }
                return noteReferences;
            }
        }

        public async Task<IEnumerable<NoteReference>> GetParentNoteReferencesQueryAsync(int[] noteIds)
        {
            using (var sqlCnx = GetConnection())
            using (var sqlCmd = new NpgsqlCommand(string.Empty, sqlCnx))
            {
                sqlCmd.CommandText = @"
SELECT * 
FROM ""NoteReferences""
WHERE ""ReferencedNoteId"" = ANY(@NoteIds)
ORDER BY ""ReferencedNoteId""
";
                DbUtilties.AddInt32ArrayParameter(sqlCmd, "@NoteIds", noteIds);

                var noteReferences = new List<NoteReference>();
                using (var reader = await sqlCmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        noteReferences.Add(new NoteReference(DbUtilties.GetInt32OrDefault(reader, "NoteId"),
                                                             DbUtilties.GetInt32OrDefault(reader, "ReferencedNoteId"),
                                                             DbUtilties.GetInt32OrDefault(reader, "ReferencedBibleVerseId")));
                    }
                }
                return noteReferences;
            }
        }

        public async Task DeleteNoteReferences
            (int noteId, IEnumerable<int> noteReferenceIds, IEnumerable<int> bibleVerseReferenceIds)
        {
            using (var sqlCnx = GetConnection())
            using (var sqlCmd = new NpgsqlCommand(string.Empty, sqlCnx))
            {
                sqlCmd.CommandText = @"
DELETE FROM ""NoteReferences""
WHERE ""NoteId"" = @NoteId
AND (""ReferencedNoteId"" = ANY(@ReferencedNoteId) OR ""ReferencedBibleVerseId"" = ANY(@ReferencedBibleVerseId))
";
                DbUtilties.AddInt32Parameter(sqlCmd, "@NoteId", noteId);
                DbUtilties.AddInt32ArrayParameter(sqlCmd, "@ReferencedNoteId", noteReferenceIds.ToArray());
                DbUtilties.AddInt32ArrayParameter(sqlCmd, "@ReferencedBibleVerseId", bibleVerseReferenceIds.ToArray());

                await sqlCmd.ExecuteNonQueryAsync();
            }
        }
    }
}
