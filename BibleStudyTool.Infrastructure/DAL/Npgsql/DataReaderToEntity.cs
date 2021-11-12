using System;
using BibleStudyTool.Core.Entities;
using Npgsql;

namespace BibleStudyTool.Infrastructure.DAL.Npgsql
{
    internal static class DataReaderToEntity
    {
        public static Tag DataReaderToTag(NpgsqlDataReader reader)
        {
            return new Tag(DbUtilties.GetInt32OrDefault(reader, "TagId"),
                           DbUtilties.GetStringOrDefault(reader, "TagUid"),
                           DbUtilties.GetStringOrDefault(reader, "TagLabel"),
                           DbUtilties.GetStringOrDefault(reader, "TagColor"));
        }

        public static TagGroup DataReaderToTagGroup(NpgsqlDataReader reader)
        {
            return new TagGroup
                (DbUtilties.GetInt32OrDefault(reader, "TagGroupId"),
                DbUtilties.GetStringOrDefault(reader, "TagGroupUid"));
        }

        public static Language DataReaderToLanguage(NpgsqlDataReader reader)
        {
            return new Language
                (DbUtilties.GetStringOrDefault(reader, "Code"),
                DbUtilties.GetStringOrDefault(reader, "LanguageName"),
                DbUtilties.GetStringOrDefault(reader, "Endonym"));
        }
    }
}
