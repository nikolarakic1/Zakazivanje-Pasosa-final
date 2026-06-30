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

        protected SqlConnection KreirajKonekciju()
        {
            return new SqlConnection(_connectionString);
        }

        protected async Task<SqlConnection> OtvoriKonekciju()
        {
            var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }

        protected SqlCommand KreirajKomandu(string sql, SqlConnection connection)
        {
            return new SqlCommand(sql, connection);
        }

        protected SqlCommand KreirajStoredProceduru(string nazivProcedure, SqlConnection connection)
        {
            var command = new SqlCommand(nazivProcedure, connection);
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }

        protected void DodajParametar(SqlCommand command, string naziv, object? vrednost)
        {
            command.Parameters.AddWithValue(naziv, vrednost ?? DBNull.Value);
        }
    }
}