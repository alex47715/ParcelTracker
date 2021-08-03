using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DapperLibrarie
{
    public interface IDapperrepository
    {
        Task ExecuteAsync(string sql, object param = null);
        Task<string> GetSingleOrDefault(string sql, object param = null);
    }

    public class DapperRepository : IDapperrepository
    {
        private readonly string _connectionString;
        public DapperRepository(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }
            _connectionString = connectionString;
        }
        public async Task ExecuteAsync(string sql, object param = null)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                var result=await db.ExecuteAsync(sql, param);
            }
        }
        public async Task<string> GetSingleOrDefault(string sql, object param = null)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var result=await connection.QueryAsync<string>(sql, param).ConfigureAwait(false);
            return result.SingleOrDefault();
        }
    }
}
