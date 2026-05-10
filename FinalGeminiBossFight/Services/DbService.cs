using Dapper;
using FinalGeminiBossFight.Models;
using Microsoft.Data.Sqlite;

namespace FinalGeminiBossFight.Services
{
    public class DbService
    {
        private readonly string _connectionString;
        public DbService(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<int> InitTableAsync()
        {
            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                string sql = @"CREATE TABLE IF NOT EXISTS  Transfer(" +
                            "ID INTEGER NOT NULL PRIMARY KEY," +
                            "NAME TEXT NOT NULL," +
                            "EMAIL TEXT NOT NULL," +
                            "AMMOUNT REAL NOT NULL," +
                            "ACCOUNT TEXT NOT NULL," +
                            "CURRENCYCODE TEXT NOT NULL," +
                            "COUNTY TEXT NOT NULL);";
                return await connection.ExecuteAsync(sql);
            }
        }
        public async Task<int?> InsertTransfareAsync(Transfer model)
        {
            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
              return await  connection.InsertAsync<Transfer>(model);
            }

        }
    }
}
