using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using IC_WPF.Models;
using Microsoft.Data.Sqlite;

namespace IC_WPF.Services
{
    public class DbService
    {
        private readonly string _connectionString = "Data Source=company.db";

        public async Task<int> InitAsync()
        {
            using (SqliteConnection conncection = new SqliteConnection(_connectionString))
            {
                await conncection.OpenAsync();
                string sql = @"CREATE TABLE IF NOT EXISTS Company(" +
                                "Id INTEGER NOT NULL PRIMARY KEY," +
                                "Name Text NOT NULL," +
                                "DIC TEXT NOT NULL," +
                                "Town TEXT NOT NULL," +
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

        public async Task<List<Company>> GetAllCompaniesAsync()
        {
            using (SqliteConnection conncection = new SqliteConnection(_connectionString))
            {
                await conncection.OpenAsync();
                var companies =  await conncection.GetListAsync<Company>();
                return companies.ToList();
            }
        }
    }
}
