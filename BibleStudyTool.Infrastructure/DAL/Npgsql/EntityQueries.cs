using System;
using Npgsql;

namespace BibleStudyTool.Infrastructure.DAL.Npgsql
{
    public abstract class EntityQueries
    {
        protected readonly string _connectionString;

        public EntityQueries(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected NpgsqlConnection GetConnection()
        {
            var sqlCnx = new NpgsqlConnection(_connectionString);
            sqlCnx.Open();
            return sqlCnx;
        }
    }
}
