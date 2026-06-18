using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ZakazivanjeTerminaDTO;

namespace ZakazivanjeTerminaZaPasose.Controllers
{
    public class DokumentacijaController : Controller
    {
        private readonly HttpClient _httpClient;

        public DokumentacijaController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<IActionResult> Index()
        {
            var dokumentacija = await _httpClient
                .GetFromJsonAsync<List<PrikazDokumentacijaDTO>>("api/Dokumentacija");

            return View(dokumentacija ?? new List<PrikazDokumentacijaDTO>());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await NapuniListe();

            return View(new DokumentacijaDTO
            {
                DatumDostavljanja = DateTime.Today
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(DokumentacijaDTO dto)
        {
            if (dto.ZahtevZaPasosId == 0)
                ModelState.AddModelError("ZahtevZaPasosId", "Morate izabrati zahtev.");

            if (dto.VrstaDokumentaId == 0)
                ModelState.AddModelError("VrstaDokumentaId", "Morate izabrati vrstu dokumenta.");

            if (!dto.Dostavljeno)
                dto.DatumDostavljanja = null;

            if (!ModelState.IsValid)
            {
                await NapuniListe();
                return View(dto);
            }

            var response = await _httpClient.PostAsJsonAsync("api/Dokumentacija", dto);

            if (!response.IsSuccessStatusCode)
            {
                var greska = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", $"Greška prilikom dodavanja dokumentacije: {greska}");

                await NapuniListe();
                return View(dto);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var prikaz = await _httpClient
                .GetFromJsonAsync<PrikazDokumentacijaDTO>($"api/Dokumentacija/{id}");

            if (prikaz is null)
                return NotFound();

            var dto = new DokumentacijaDTO
            {
                Id = prikaz.Id,
                ZahtevZaPasosId = prikaz.ZahtevZaPasosId,
                VrstaDokumentaId = prikaz.VrstaDokumentaId,
                Dostavljeno = prikaz.Dostavljeno,
                DatumDostavljanja = prikaz.DatumDostavljanja,
                Napomena = prikaz.Napomena
            };

            ViewBag.Id = id;
            await NapuniListe();

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, DokumentacijaDTO dto)
        {
            if (dto.ZahtevZaPasosId == 0)
                ModelState.AddModelError("ZahtevZaPasosId", "Morate izabrati zahtev.");

            if (dto.VrstaDokumentaId == 0)
                ModelState.AddModelError("VrstaDokumentaId", "Morate izabrati vrstu dokumenta.");

            if (!dto.Dostavljeno)
                dto.DatumDostavljanja = null;

            if (!ModelState.IsValid)
            {
                ViewBag.Id = id;
                await NapuniListe();
                return View(dto);
            }

            var response = await _httpClient.PutAsJsonAsync($"api/Dokumentacija/{id}", dto);

            if (!response.IsSuccessStatusCode)
            {
                var greska = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", $"Greška prilikom izmene dokumentacije: {greska}");

                ViewBag.Id = id;
                await NapuniListe();
                return View(dto);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _httpClient.DeleteAsync($"api/Dokumentacija/{id}");
            return RedirectToAction(nameof(Index));
        }

        private async Task NapuniListe()
        {
            var zahtevi = await _httpClient
                .GetFromJsonAsync<List<PrikazZahtevaZaPasosDTO>>("api/ZahtevZaPasos");

            var vrste = await _httpClient
                .GetFromJsonAsync<List<PrikazVrsteDokumentaDTO>>("api/VrstaDokumenta");

            ViewBag.Zahtevi = zahtevi?
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = $"{x.BrojZahteva} - {x.ImePodnosioca} {x.PrezimePodnosioca}"
                })
                .ToList() ?? new List<SelectListItem>();

            ViewBag.VrsteDokumenata = vrste?
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Obavezno ? $"{x.Naziv} (obavezno)" : x.Naziv
                })
                .ToList() ?? new List<SelectListItem>();
        }
        [HttpGet]
        public async Task<IActionResult> UrediZaZahtev(int zahtevId)
        {
            var dokumentacija = await _httpClient
                .GetFromJsonAsync<List<PrikazDokumentacijaDTO>>($"api/Dokumentacija/zahtev/{zahtevId}");

            if (dokumentacija is null)
                return NotFound();

            var model = dokumentacija.Select(x => new IzmenaDokumentacijeDTO
            {
                Id = x.Id,
                ZahtevZaPasosId = x.ZahtevZaPasosId,
                VrstaDokumentaId = x.VrstaDokumentaId,
                NazivDokumenta = x.NazivDokumenta,
                Obavezno = x.Obavezno,
                Dostavljeno = x.Dostavljeno,
                DatumDostavljanja = x.DatumDostavljanja,
                Napomena = x.Napomena
            }).ToList();

            ViewBag.ZahtevId = zahtevId;
            ViewBag.BrojZahteva = dokumentacija.FirstOrDefault()?.BrojZahteva;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UrediZaZahtev(List<IzmenaDokumentacijeDTO> model)
        {
            if (model is null || !model.Any())
                return RedirectToAction(nameof(Index));

            foreach (var stavka in model)
            {
                var dto = new DokumentacijaDTO
                {
                    Id = stavka.Id,
                    ZahtevZaPasosId = stavka.ZahtevZaPasosId,
                    VrstaDokumentaId = stavka.VrstaDokumentaId,
                    Dostavljeno = stavka.Dostavljeno,
                    DatumDostavljanja = stavka.Dostavljeno ? (stavka.DatumDostavljanja ?? DateTime.Today) : null,
                    Napomena = stavka.Napomena
                };

                await _httpClient.PutAsJsonAsync($"api/Dokumentacija/{stavka.Id}", dto);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}