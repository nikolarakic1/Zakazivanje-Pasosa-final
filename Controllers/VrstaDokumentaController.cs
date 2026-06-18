using Microsoft.AspNetCore.Mvc;
using ZakazivanjeTerminaDTO;

namespace ZakazivanjeTerminaZaPasose.Controllers
{
    public class VrstaDokumentaController : Controller
    {
        private readonly HttpClient _httpClient;

        public VrstaDokumentaController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<IActionResult> Index()
        {
            var dokumenta = await _httpClient
                .GetFromJsonAsync<List<PrikazVrsteDokumentaDTO>>("api/VrstaDokumenta");

            return View(dokumenta ?? new List<PrikazVrsteDokumentaDTO>());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new VrstaDokumentaDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create(VrstaDokumentaDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var response = await _httpClient.PostAsJsonAsync("api/VrstaDokumenta", dto);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Greška prilikom dodavanja vrste dokumenta.");
                return View(dto);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var dokument = await _httpClient
                .GetFromJsonAsync<PrikazVrsteDokumentaDTO>($"api/VrstaDokumenta/{id}");

            if (dokument is null)
                return NotFound();

            var dto = new VrstaDokumentaDTO
            {
                Naziv = dokument.Naziv,
                Opis = dokument.Opis,
                Obavezno = dokument.Obavezno
            };

            ViewBag.Id = id;

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, VrstaDokumentaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Id = id;
                return View(dto);
            }

            var response = await _httpClient.PutAsJsonAsync($"api/VrstaDokumenta/{id}", dto);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Greška prilikom izmene vrste dokumenta.");
                ViewBag.Id = id;
                return View(dto);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/VrstaDokumenta/{id}");

            return RedirectToAction(nameof(Index));
        }
    }
}