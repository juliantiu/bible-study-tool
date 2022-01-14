using System;
using System.Collections.Generic;
using Npgsql;
using NpgsqlTypes;

namespace BibleStudyTool.Infrastructure.DAL.Npgsql
{
    internal static class DbUtilties
    {
        internal static void AddNonEmptyVarcharParameter(NpgsqlCommand sqlCmd, string parameterName, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                sqlCmd.Parameters.Add(parameterName, NpgsqlDbType.Varchar).Value = value;
            }
        }

        internal static void AddInt32Parameter(NpgsqlCommand sqlCmd, string parameterName, int value)
        {
            if (value > 0)
            {
                sqlCmd.Parameters.Add(parameterName, NpgsqlDbType.Integer).Value = value;
            }
        }

        internal static void AddInt32ArrayParameter(NpgsqlCommand sqlCmd, string parameterName, int[] value)
        {
            if (value.Length > 0)
            {
                sqlCmd.Parameters.Add(parameterName, NpgsqlDbType.Array | NpgsqlDbType.Integer).Value = value;
            }
            else
            {
                throw new Exception("List of integers cannot be empty.");
            }    
        }

        internal static int GetOrdinal(NpgsqlDataReader reader, string columnName)
        {
            if (!string.IsNullOrWhiteSpace(columnName))
            {
                return reader.GetOrdinal(columnName);
            }

            throw new Exception("Column name cannot be null, empty, or whitespaces.");
        }

        internal static int GetInt32OrDefault(NpgsqlDataReader reader, string columnName)
        {
            var ordinal = GetOrdinal(reader, columnName);

            if (!reader.IsDBNull(ordinal))
            {
                return reader.GetInt32(ordinal);
            }

            return 0;
        }

        internal static string GetStringOrDefault(NpgsqlDataReader reader, string columnName)
        {
            var ordinal = GetOrdinal(reader, columnName);

            if (!reader.IsDBNull(ordinal))
            {
                return reader.GetString(ordinal);
            }

            return string.Empty;
        }

        internal static bool GetBool(NpgsqlDataReader reader, string columnName)
        {
            var ordinal = GetOrdinal(reader, columnName);

            if (!reader.IsDBNull(ordinal))
            {
                return reader.GetBoolean(ordinal);
            }

            return false;
        }
    }
}
