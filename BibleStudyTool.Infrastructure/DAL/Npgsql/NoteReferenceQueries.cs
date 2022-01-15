using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
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
                                                             DbUtilties.GetInt32OrDefault(reader, "ReferencedNoteId")));
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
                                                             DbUtilties.GetInt32OrDefault(reader, "ReferencedNoteId")));
                    }
                }
                return noteReferences;
            }
        }

        public async Task DeleteNoteReferences
            (int noteId, IEnumerable<int> noteReferenceIds)
        {
            using (var sqlCnx = GetConnection())
            using (var sqlCmd = new NpgsqlCommand(string.Empty, sqlCnx))
            {
                sqlCmd.CommandText = @"
DELETE FROM ""NoteReferences""
WHERE ""NoteId"" = @NoteId
AND (""ReferencedNoteId"" = ANY(@ReferencedNoteId))
";
                DbUtilties.AddInt32Parameter(sqlCmd, "@NoteId", noteId);
                DbUtilties.AddInt32ArrayParameter(sqlCmd, "@ReferencedNoteId", noteReferenceIds.ToArray());

                await sqlCmd.ExecuteNonQueryAsync();
            }
        }
    }
}
