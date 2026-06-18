using Microsoft.AspNetCore.Mvc;
using ZakazivanjeTerminaDTO;
using ZakazivanjeTerminaModeli;
using ZakazivanjeTerminaServisniSloj.Interfejsi;

namespace ZakazivanjeTerminaServisi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PodnosilacZahtevaController : ControllerBase
    {
        private readonly IPodnosilacZahtevaServis _servis;

        public PodnosilacZahtevaController(IPodnosilacZahtevaServis servis)
        {
            _servis = servis;
        }

        [HttpGet]
        public async Task<ActionResult<List<PodnosilacZahteva>>> VratiSve()
        {
            return Ok(await _servis.VratiSve());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PodnosilacZahteva>> VratiPoIdu(int id)
        {
            var podnosilac = await _servis.VratiPoIdu(id);

            if (podnosilac is null)
                return NotFound();

            return Ok(podnosilac);
        }

        [HttpPost]
        public async Task<IActionResult> Dodaj([FromBody] PodnosilacZahtevaDTO dto)
        {
            await _servis.Dodaj(dto);
            return Ok("Podnosilac zahteva je uspesno dodat.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Izmeni(int id, [FromBody] PodnosilacZahtevaDTO dto)
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

            return Ok("Podnosilac zahteva je uspesno obrisan.");
        }
    }
}