using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using Npgsql;

namespace BibleStudyTool.Infrastructure.DAL.Npgsql
{
    public class TagGroupQueries : EntityQueries
    {
        public TagGroupQueries(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        ///     Gets all user tag groups.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        ///     The list of tag groups that belong to the user.
        /// </returns>
        public async Task<IEnumerable<TagGroupWithTags>>
            GetAllUserTagGroupsAsync(string userId)
        {
            using (var sqlCnx = GetConnection())
            using (var sqlCmd = new NpgsqlCommand(string.Empty, sqlCnx))
            {
                sqlCmd.CommandText = @"
SELECT
    ""Tags"".*,
    ""TagGroupTags"".*
FROM ""TagGroups""
JOIN ""TagGroupTags""
ON ""TagGroups"".""TagGroupId"" = ""TagGroupTags"".""TagGroupId""
JOIN ""Tags""
ON ""TagGroupTags"".""TagId"" = ""Tags"".""TagId""
AND ""TagGroupUid"" = @TagGroupUid
ORDER BY ""Tags"".TagLabel ASC;
";
                DbUtilties.AddNonEmptyVarcharParameter
                    (sqlCmd, "@TagGroupUid", userId);

                var tagGroupLookup = new Dictionary<int, TagGroupWithTags>();
                using (var reader = await sqlCmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var tagGroupId = DbUtilties.GetInt32OrDefault
                            (reader, "TagGroupId");

                        var newTag = DataReaderToEntity.DataReaderToTag(reader);
                        if
                            (tagGroupLookup.TryGetValue
                                (tagGroupId, out var tagGroup))
                        {
                            tagGroup.Tags.Add(newTag);
                        }
                        else
                        {
                            tagGroupLookup[tagGroupId] =
                                new TagGroupWithTags
                                    (DataReaderToEntity.DataReaderToTagGroup
                                        (reader),
                                new List<Tag>());
                        }
                    }
                }
                return tagGroupLookup.Values;
            }
        }
    }
}
