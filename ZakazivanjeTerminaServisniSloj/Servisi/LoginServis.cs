using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ZakazivanjeTerminaDTO;
using ZakazivanjeTerminaServisniSloj.Interfejsi;

namespace ZakazivanjeTerminaServisniSloj.Servisi
{
    public class LoginServis : ILoginServis
    {
        private readonly IConfiguration _configuration;

        public LoginServis(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int?> Prijavi(LoginDTO dto)
        {
            var konekcija = _configuration.GetConnectionString("DefaultConnection");

            using var connection = new SqlConnection(konekcija);

            var command = new SqlCommand(
                @"SELECT Id 
                  FROM Korisnici 
                  WHERE KorisnickoIme = @KorisnickoIme 
                  AND LozinkaHash = @Lozinka", connection);

            command.Parameters.AddWithValue("@KorisnickoIme", dto.KorisnickoIme);
            command.Parameters.AddWithValue("@Lozinka", dto.Lozinka);

            await connection.OpenAsync();

            var rezultat = await command.ExecuteScalarAsync();

            if (rezultat is null)
                return null;

            return Convert.ToInt32(rezultat);
        }
    }
}