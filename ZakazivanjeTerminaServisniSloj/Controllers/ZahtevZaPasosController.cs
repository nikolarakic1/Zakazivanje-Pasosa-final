using Microsoft.AspNetCore.Mvc;
using ZakazivanjeTerminaDTO;
using ZakazivanjeTerminaServisniSloj.Interfejsi;

namespace ZakazivanjeTerminaServisi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZahtevZaPasosController : ControllerBase
    {
        private readonly IZahtevZaPasosServis _servis;

        public ZahtevZaPasosController(IZahtevZaPasosServis servis)
        {
            _servis = servis;
        }

        [HttpGet]
        public async Task<ActionResult<List<PrikazZahtevaZaPasosDTO>>> VratiSve()
        {
            return Ok(await _servis.VratiSve());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PrikazZahtevaZaPasosDTO>> VratiPoIdu(int id)
        {
            var zahtev = await _servis.VratiPoIdu(id);

            if (zahtev is null)
                return NotFound();

            return Ok(zahtev);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<List<PrikazZahtevaZaPasosDTO>>> Filtriraj(
            [FromQuery] string? status,
            [FromQuery] string? jmbg)
        {
            return Ok(await _servis.Filtriraj(status, jmbg));
        }

        [HttpPost]
        public async Task<IActionResult> Dodaj([FromBody] ZahtevZaPasosDTO dto)
        {
            await _servis.Dodaj(dto);
            return Ok("Zahtev za pasos je uspesno dodat.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Izmeni(int id, [FromBody] ZahtevZaPasosDTO dto)
        {
            var izmenjen = await _servis.Izmeni(dto, id);

            if (izmenjen is null)
                return NotFound();

            return Ok(izmenjen);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Obrisi(int id)
        {
            var obrisan = await _servis.Obrisi(id);

            if (!obrisan)
                return NotFound();

            return Ok("Zahtev za pasos je uspesno obrisan.");
        }
    }
}