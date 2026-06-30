using Microsoft.Extensions.Configuration;
using ZakazivanjeTerminaDTO;
using ZakazivanjeTerminaPodaci.PomocneKlase;
using ZakazivanjeTerminaServisniSloj.Interfejsi;

namespace ZakazivanjeTerminaServisniSloj.Servisi
{
    public class LoginServis : DBUtils, ILoginServis
    {
        public LoginServis(IConfiguration configuration)
            : base(configuration)
        {
        }

        public async Task<int?> Prijavi(LoginDTO dto)
        {
            if (dto is null)
                throw new ArgumentException("Podaci za prijavu ne smeju biti prazni.");

            await using var connection = await OtvoriKonekciju();

            var sql = @"
                SELECT Id
                FROM Korisnici
                WHERE KorisnickoIme = @KorisnickoIme
                AND LozinkaHash = @Lozinka
                AND Obrisan = 0";

            await using var command = KreirajKomandu(sql, connection);

            DodajParametar(command, "@KorisnickoIme", dto.KorisnickoIme);
            DodajParametar(command, "@Lozinka", dto.Lozinka);

            var rezultat = await command.ExecuteScalarAsync();

            if (rezultat is null)
                return null;

            return Convert.ToInt32(rezultat);
        }
    }
}