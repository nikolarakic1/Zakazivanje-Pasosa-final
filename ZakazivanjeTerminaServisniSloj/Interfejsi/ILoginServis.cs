using ZakazivanjeTerminaDTO;

namespace ZakazivanjeTerminaServisniSloj.Interfejsi
{
    public interface ILoginServis
    {
        Task<int?> Prijavi(LoginDTO dto);
    }
}