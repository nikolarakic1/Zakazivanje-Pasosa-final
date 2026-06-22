using Microsoft.Data.SqlClient;
using ZakazivanjeTerminaDTO;
using ZakazivanjeTerminaPodaci.PomocneKlase;
using ZakazivanjeTerminaServisniSloj.Interfejsi;

namespace ZakazivanjeTerminaServisniSloj.Servisi
{
    public class LoginServis : ILoginServis
    {
        private readonly DBUtils _dbUtils;

        public LoginServis(DBUtils dbUtils)
        {
            _dbUtils = dbUtils;
        }

        public async Task<int?> Prijavi(LoginDTO dto)
        {
            if (dto is null)
                throw new ArgumentException("Podaci za prijavu ne smeju biti prazni.");

            await using var connection = await _dbUtils.OtvoriKonekciju();

            var sql = @"
                SELECT Id
                FROM Korisnici
                WHERE KorisnickoIme = @KorisnickoIme
                AND LozinkaHash = @Lozinka";

            await using var command = _dbUtils.KreirajKomandu(sql, connection);

            _dbUtils.DodajParametar(command, "@KorisnickoIme", dto.KorisnickoIme);
            _dbUtils.DodajParametar(command, "@Lozinka", dto.Lozinka);

            var rezultat = await command.ExecuteScalarAsync();

            if (rezultat is null)
                return null;

            return Convert.ToInt32(rezultat);
        }
    }
}