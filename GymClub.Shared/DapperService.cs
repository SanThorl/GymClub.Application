using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymClub.Shared
{
    public class DapperService
    {
        private readonly string _connectionString;

        public DapperService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<T> Query<T>(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            List<T> lst = db.Query<T>(query, param).ToList();
            return lst;
        }

        public T QuerySingle<T>(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            T obj = db.QuerySingle<T>(query, param);
            return obj;
        }
        public int Execute(string query, object? parameters = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            int result = db.Execute(query, parameters);
            return result;
        }
    }
}
