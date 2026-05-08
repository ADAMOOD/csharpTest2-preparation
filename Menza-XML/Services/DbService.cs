using System.Data.Common;
using Dapper;
using Menza_XML.Models;
using Microsoft.Data.Sqlite;

namespace Menza_XML.Services
{
    public class DbService
    {
        private readonly string _connectionString = string.Empty;

        public DbService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> init()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                string sql = @"CREATE TABLE IF NOT EXISTS FoodRecord (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                UserName TEXT NOT NULL,
                                OrderDate TEXT NOT NULL,
                                FoodAltId INTEGER NOT NULL,
                                FoodName TEXT NOT NULL
                            );";
                return await connection.ExecuteAsync(sql);
            }
        }

        public async Task<int?> saveAsync(dbRecordModel record)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.InsertAsync<dbRecordModel>(record);
            }
        }

        public async Task<List<dbRecordModel>> GetAllOrders()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                return (await connection.GetListAsync<dbRecordModel>()).ToList();
            }
        }
    }
}
