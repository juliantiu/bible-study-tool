using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using Npgsql;

namespace BibleStudyTool.Infrastructure.DAL.Npgsql
{
    public class TagQueries : EntityQueries
    {
        public TagQueries(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        ///     Gets the tags that belong to a tag group.
        /// </summary>
        /// <param name="tagGroupId"></param>
        /// <returns>
        ///     A list of all the tags that belong to a tag group.
        /// </returns>
        public async Task<IEnumerable<Tag>> GetTagsInTagGroupAsync
            (int tagGroupId)
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
FROM ""TagGroupTags""
JOIN ""Tags""
ON ""Tags"".""TagId"" = ""TagGroupTags"".""TagId""
AND ""TagGroupTags"".""TagGroupId"" = @TagGroupId
ORDER BY ""Tags"".""TagId"" ASC
";
                DbUtilties.AddInt32Parameter
                    (sqlCmd, "@TagGroupId", tagGroupId);

                IList<Tag> tags = new List<Tag>();
                using (var reader = await sqlCmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var noteId = DbUtilties.GetInt32OrDefault
                            (reader, "NoteId");

                        tags.Add(new Tag(DbUtilties.GetInt32OrDefault
                                            (reader, "TagId"),
                                         DbUtilties.GetStringOrDefault
                                            (reader, "TagUid"),
                                         DbUtilties.GetStringOrDefault
                                            (reader, "TagLabel"),
                                         DbUtilties.GetStringOrDefault
                                            (reader, "TagColor")));
                    }
                }
                return tags;
            }
        }

        /// <summary>
        ///     Gets all tag IDs associated with the notes.
        /// </summary>
        /// <param name="noteIds"></param>
        /// <returns>
        ///     A dictionary with note ID as the key and the note's tags.
        /// </returns>
        public async Task<IDictionary<int, IList<Tag>>>
            GetTagIdsForNotesQueryAsync(int[] noteIds)
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
                        var noteId = DbUtilties.GetInt32OrDefault
                            (reader, "NoteId");

                        if (!(noteTagsMapping.ContainsKey(noteId)))
                        {
                            noteTagsMapping[noteId] = new List<Tag>();
                        }

                        noteTagsMapping[noteId].Add
                            (new Tag(DbUtilties.GetInt32OrDefault
                                        (reader, "TagId"),
                                    DbUtilties.GetStringOrDefault
                                        (reader, "TagUid"),
                                    DbUtilties.GetStringOrDefault
                                        (reader, "TagLabel"),
                                    DbUtilties.GetStringOrDefault
                                        (reader, "TagColor")));
                    }
                }
                return noteTagsMapping;
            }
        }

        /// <summary>
        ///     Gets all user tags.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        ///     The list of tags that belong to the user.
        /// </returns>
        public async Task<IEnumerable<Tag>> GetAllUserTagsAsync(string userId)
        {
            using (var sqlCnx = GetConnection())
            using (var sqlCmd = new NpgsqlCommand(string.Empty, sqlCnx))
            {
                sqlCmd.CommandText = @"
SELECT *
FROM ""Tags""
WHERE ""TagUid"" = @TagUid;
";
                DbUtilties.AddNonEmptyVarcharParameter
                    (sqlCmd, "@TagUid", userId);

                var tags = new List<Tag>();
                using (var reader = await sqlCmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        tags.Add
                            (new Tag(DbUtilties.GetInt32OrDefault
                                        (reader, "TagId"),
                                    DbUtilties.GetStringOrDefault
                                        (reader, "TagUid"),
                                    DbUtilties.GetStringOrDefault
                                        (reader, "TagLabel"),
                                    DbUtilties.GetStringOrDefault
                                        (reader, "TagColor")));
                    }
                }
                return tags;
            }
        }
    }
}
