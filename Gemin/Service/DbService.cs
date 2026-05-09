using Dapper;
using Gemin.Models;
using Microsoft.Data.Sqlite;

namespace Gemin.Service
{
    public class DbService
    {
        private readonly string _connectionString;

        public DbService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> Init()
        {
            using (SqliteConnection conncection = new SqliteConnection(_connectionString))
            {
                await conncection.OpenAsync();
                string sql = @"CREATE TABLE IF NOT EXISTS Company(" +
                             "Id INTEGER NOT NULL PRIMARY KEY," +
                             "Name TEXT NOT NULL," +
                             "DIC TEXT NOT NULL," +
                             "EmployeeCount INTEGER NOT NULL," +
                             "CompanyType INTEGER NOT NULL," + //CompanyType je enum a tudiz musim sem dat integer
                             "Notes TEXT);";
                return await conncection.ExecuteAsync(sql);
            }
        }

        public async Task<int?> InsertCompanyAsync(Company company)
        {
            using (SqliteConnection conncection = new SqliteConnection(_connectionString))
            {
                await conncection.OpenAsync();
                return await conncection.InsertAsync<Company>(company);
            }
        }

        public async Task<List<Company>> GetAllCompanmiesAsync()
        {
            using (SqliteConnection conncection = new SqliteConnection(_connectionString))
            {
                await conncection.OpenAsync();
                var response = await conncection.GetListAsync<Company>();
                return response.ToList();
            }
        }

        public async Task<int> DeleteCompany(int id)
        {
            using (SqliteConnection conncection = new SqliteConnection(_connectionString))
            {
                await conncection.OpenAsync();
                return await conncection.DeleteAsync<Company>(id);
            }
        }
    }
}
