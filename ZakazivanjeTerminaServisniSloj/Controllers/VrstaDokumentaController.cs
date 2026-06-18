using Microsoft.AspNetCore.Mvc;
using ZakazivanjeTerminaDTO;
using ZakazivanjeTerminaModeli;
using ZakazivanjeTerminaServisniSloj.Interfejsi;

namespace ZakazivanjeTerminaServisi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VrstaDokumentaController : ControllerBase
    {
        private readonly IVrstaDokumentaServis _servis;

        public VrstaDokumentaController(IVrstaDokumentaServis servis)
        {
            _servis = servis;
        }

        [HttpGet]
        public async Task<ActionResult<List<VrstaDokumenta>>> VratiSve()
        {
            return Ok(await _servis.VratiSve());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VrstaDokumenta>> VratiPoIdu(int id)
        {
            var vrsta = await _servis.VratiPoIdu(id);

            if (vrsta is null)
                return NotFound();

            return Ok(vrsta);
        }

        [HttpPost]
        public async Task<IActionResult> Dodaj([FromBody] VrstaDokumentaDTO dto)
        {
            await _servis.Dodaj(dto);
            return Ok("Vrsta dokumenta je uspesno dodata.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Izmeni(int id, [FromBody] VrstaDokumentaDTO dto)
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

            return Ok("Vrsta dokumenta je uspesno obrisana.");
        }
    }
}