using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ZakazivanjeTerminaPodaci.PomocneKlase
{
    public class DBUtils
    {
        private readonly string _connectionString;

        public DBUtils(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' nije pronađen.");
        }

        public SqlConnection KreirajKonekciju()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<SqlConnection> OtvoriKonekciju()
        {
            var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }

        public SqlCommand KreirajKomandu(string sql, SqlConnection connection)
        {
            return new SqlCommand(sql, connection);
        }

        public SqlCommand KreirajStoredProceduru(string nazivProcedure, SqlConnection connection)
        {
            var command = new SqlCommand(nazivProcedure, connection);
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }

        public void DodajParametar(SqlCommand command, string naziv, object? vrednost)
        {
            command.Parameters.AddWithValue(naziv, vrednost ?? DBNull.Value);
        }
    }
}