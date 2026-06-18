using Microsoft.AspNetCore.Mvc;
using ZakazivanjeTerminaDTO;
using ZakazivanjeTerminaServisniSloj.Interfejsi;

namespace ZakazivanjeTerminaServisi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DokumentacijaController : ControllerBase
    {
        private readonly IDokumentacijaServis _servis;

        public DokumentacijaController(IDokumentacijaServis servis)
        {
            _servis = servis;
        }

        [HttpGet]
        public async Task<ActionResult<List<PrikazDokumentacijaDTO>>> VratiSve()
        {
            return Ok(await _servis.VratiSve());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PrikazDokumentacijaDTO>> VratiPoIdu(int id)
        {
            var dokumentacija = await _servis.VratiPoIdu(id);

            if (dokumentacija is null)
                return NotFound();

            return Ok(dokumentacija);
        }

        [HttpGet("zahtev/{zahtevZaPasosId}")]
        public async Task<ActionResult<List<PrikazDokumentacijaDTO>>> VratiPoZahtevu(int zahtevZaPasosId)
        {
            return Ok(await _servis.VratiPoZahtevu(zahtevZaPasosId));
        }

        [HttpPost]
        public async Task<IActionResult> Dodaj([FromBody] DokumentacijaDTO dto)
        {
            await _servis.Dodaj(dto);
            return Ok("Dokumentacija je uspešno dodata.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Izmeni(int id, [FromBody] DokumentacijaDTO dto)
        {
            var izmenjena = await _servis.Izmeni(dto, id);

            if (izmenjena is null)
                return NotFound();

            return Ok(izmenjena);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Obrisi(int id)
        {
            var obrisana = await _servis.Obrisi(id);

            if (!obrisana)
                return NotFound();

            return Ok("Dokumentacija je uspešno obrisana.");
        }
    }
}