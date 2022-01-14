using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;

namespace BibleStudyTool.Infrastructure.DAL.Npgsql
{
    public class TagNoteQueries : EntityQueries
    {
        public TagNoteQueries(string connectionString) : base(connectionString)
        {
        }

        public async Task DeleteTagNotes
            (int noteId, IEnumerable<int> tagsIds)
        {
            using (var sqlCnx = GetConnection())
            using (var sqlCmd = new NpgsqlCommand(string.Empty, sqlCnx))
            {
                sqlCmd.CommandText = @"
DELETE FROM ""TagNotes""
WHERE ""NoteId"" = @NoteId
AND ""TagId"" = ANY(@TagIds)
";
                DbUtilties.AddInt32Parameter(sqlCmd, "@NoteId", noteId);
                DbUtilties.AddInt32ArrayParameter(sqlCmd, "@TagIds", tagsIds.ToArray());

                await sqlCmd.ExecuteNonQueryAsync();
            }
        }
    }
}
