using Dapper;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class SqlDataAccessRepository : ISqlDataAccess
    {
        private readonly IConfiguration _configuration;

        public SqlDataAccessRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<T> LoadData<T, U>(string sql, U parameters, CommandType type = CommandType.Text)
        {
            using (IDbConnection connection = new SQLiteConnection(GetConnectionString()))
            {
                var data = connection.Query<T>(sql, parameters, commandType: type);
                return data.ToList();
            }
        }

        public void SaveData<T>(string sql, T parameters, CommandType type = CommandType.Text)
        {
            using (IDbConnection connection = new SQLiteConnection(GetConnectionString()))
            {
                connection.Execute(sql, parameters, commandType: type);
            }
        }

        private string GetConnectionString()
        {
            return _configuration.GetConnectionString("master");
        }
    }
}
