using Microsoft.AspNetCore.Mvc;
using ZakazivanjeTerminaDTO;
using ZakazivanjeTerminaServisniSloj.Interfejsi;

namespace ZakazivanjeTerminaServisi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginServis _loginServis;

        public LoginController(ILoginServis loginServis)
        {
            _loginServis = loginServis;
        }

        [HttpPost]
        public async Task<IActionResult> Prijavi([FromBody] LoginDTO dto)
        {
            var korisnikId = await _loginServis.Prijavi(dto);

            if (korisnikId is null)
                return Unauthorized("Pogrešno korisničko ime ili lozinka.");

            return Ok(korisnikId);
        }
    }
}