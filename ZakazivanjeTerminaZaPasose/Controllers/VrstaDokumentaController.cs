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
            return await Indeks();
        }

        public async Task<IActionResult> Indeks()
        {
            var dokumenta = await _httpClient
                .GetFromJsonAsync<List<PrikazVrsteDokumentaDTO>>("api/VrstaDokumenta");

            return View("Indeks", dokumenta ?? new List<PrikazVrsteDokumentaDTO>());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return Napravi();
        }

        [HttpGet]
        public IActionResult Napravi()
        {
            return View("Napravi", new VrstaDokumentaDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VrstaDokumentaDTO dto)
        {
            return await Napravi(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Napravi(VrstaDokumentaDTO dto)
        {
            if (!ModelState.IsValid)
                return View("Napravi", dto);

            var response = await _httpClient.PostAsJsonAsync("api/VrstaDokumenta", dto);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Greška prilikom dodavanja vrste dokumenta.");
                return View("Napravi", dto);
            }

            return RedirectToAction(nameof(Indeks));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return await Izmeni(id);
        }

        [HttpGet]
        public async Task<IActionResult> Izmeni(int id)
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

            return View("Izmeni", dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VrstaDokumentaDTO dto)
        {
            return await Izmeni(id, dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Izmeni(int id, VrstaDokumentaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Id = id;
                return View("Izmeni", dto);
            }

            var response = await _httpClient.PutAsJsonAsync($"api/VrstaDokumenta/{id}", dto);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Greška prilikom izmene vrste dokumenta.");
                ViewBag.Id = id;
                return View("Izmeni", dto);
            }

            return RedirectToAction(nameof(Indeks));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            return await Detalji(id);
        }

        [HttpGet]
        public async Task<IActionResult> Detalji(int id)
        {
            var dokument = await _httpClient
                .GetFromJsonAsync<PrikazVrsteDokumentaDTO>($"api/VrstaDokumenta/{id}");

            if (dokument is null)
                return NotFound();

            return View("Detalji", dokument);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            return await Obrisi(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Obrisi(int id)
        {
            await _httpClient.DeleteAsync($"api/VrstaDokumenta/{id}");

            return RedirectToAction(nameof(Indeks));
        }
    }
}