using CurrencyTransfere2.Models;
using Dapper;
using Microsoft.Data.Sqlite;

namespace CurrencyTransfere2.Services
{
    public class DatabaseService
    {
        private readonly string _connnectionString;
        public DatabaseService(string connnectionString)
        {
            _connnectionString = connnectionString;

            InitializeAsync();

        }

        public int InitializeAsync()//nepise se void ale task u async metod
        {
            using (var conncection = new SqliteConnection(_connnectionString))
            {
                conncection.Open();
                string sql = "CREATE TABLE IF NOT EXISTS Exchange (" +
                             "Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
                             "Name TEXT NOT NULL," +
                             "Email TEXT," +
                             "Ammount REAL NOT NULL," +
                             "SelectedCurrency TEXT NOT NULL," +
                             "Result REAL NOT NULL)";
                var response = conncection.Execute(sql);
                return Convert.ToInt32(response);
            }
        }

        public async Task<List<Exchange>> GetAllExchangesAsync()
        {
            using (var connection = new SqliteConnection(_connnectionString))
            {
                await connection.OpenAsync();
                var exchanges = await connection.GetListAsync<Exchange>();
                return exchanges.ToList();
            }
        }

        public async Task<int?> InsertExchangeAsync(Exchange exchange)
        {
            using (var connection = new SqliteConnection(_connnectionString))
            {
                await connection.OpenAsync();
                return await connection.InsertAsync<Exchange>(exchange);
            }
        }
    }
}
