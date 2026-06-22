using Microsoft.AspNetCore.Mvc;
using ZakazivanjeTerminaDTO;

namespace ZakazivanjeTerminaZaPasose.Controllers
{
    public class PodnosilacZahtevaController : Controller
    {
        private readonly HttpClient _httpClient;

        public PodnosilacZahtevaController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<IActionResult> Index()
        {
            return await Indeks();
        }

        public async Task<IActionResult> Indeks()
        {
            var podnosioci = await _httpClient
                .GetFromJsonAsync<List<PodnosilacZahtevaDTO>>("api/PodnosilacZahteva");

            return View("Indeks", podnosioci ?? new List<PodnosilacZahtevaDTO>());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return Napravi();
        }

        [HttpGet]
        public IActionResult Napravi()
        {
            return View("Napravi", new PodnosilacZahtevaDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PodnosilacZahtevaDTO dto)
        {
            return await Napravi(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Napravi(PodnosilacZahtevaDTO dto)
        {
            if (!ModelState.IsValid)
                return View("Napravi", dto);

            var response = await _httpClient.PostAsJsonAsync("api/PodnosilacZahteva", dto);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Greška prilikom dodavanja podnosioca zahteva.");
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
            var podnosilac = await _httpClient
                .GetFromJsonAsync<PodnosilacZahtevaDTO>($"api/PodnosilacZahteva/{id}");

            if (podnosilac is null)
                return NotFound();

            ViewBag.Id = id;

            return View("Izmeni", podnosilac);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PodnosilacZahtevaDTO dto)
        {
            return await Izmeni(id, dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Izmeni(int id, PodnosilacZahtevaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Id = id;
                return View("Izmeni", dto);
            }

            var response = await _httpClient.PutAsJsonAsync($"api/PodnosilacZahteva/{id}", dto);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Greška prilikom izmene podnosioca zahteva.");
                ViewBag.Id = id;
                return View("Izmeni", dto);
            }

            return RedirectToAction(nameof(Indeks));
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
            await _httpClient.DeleteAsync($"api/PodnosilacZahteva/{id}");

            return RedirectToAction(nameof(Indeks));
        }
    }
}