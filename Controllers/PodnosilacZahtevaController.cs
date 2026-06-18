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
            var podnosioci = await _httpClient
                .GetFromJsonAsync<List<PodnosilacZahtevaDTO>>("api/PodnosilacZahteva");

            return View(podnosioci ?? new List<PodnosilacZahtevaDTO>());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new PodnosilacZahtevaDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create(PodnosilacZahtevaDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var response = await _httpClient.PostAsJsonAsync("api/PodnosilacZahteva", dto);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Greška prilikom dodavanja podnosioca zahteva.");
                return View(dto);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var podnosilac = await _httpClient
                .GetFromJsonAsync<PodnosilacZahtevaDTO>($"api/PodnosilacZahteva/{id}");

            if (podnosilac is null)
                return NotFound();

            ViewBag.Id = id;
            return View(podnosilac);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, PodnosilacZahtevaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Id = id;
                return View(dto);
            }

            var response = await _httpClient.PutAsJsonAsync($"api/PodnosilacZahteva/{id}", dto);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Greška prilikom izmene podnosioca zahteva.");
                ViewBag.Id = id;
                return View(dto);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _httpClient.DeleteAsync($"api/PodnosilacZahteva/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}