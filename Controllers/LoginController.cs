using Microsoft.AspNetCore.Mvc;
using ZakazivanjeTerminaDTO;

namespace ZakazivanjeTerminaZaPasose.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;

        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["SakrijNavigaciju"] = true;
            return View(new LoginDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginDTO dto)
        {
            ViewData["SakrijNavigaciju"] = true;

            if (string.IsNullOrWhiteSpace(dto.KorisnickoIme) || string.IsNullOrWhiteSpace(dto.Lozinka))
            {
                ModelState.AddModelError("", "Unesite korisničko ime i lozinku.");
                return View(dto);
            }

            var response = await _httpClient.PostAsJsonAsync("api/Login", dto);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Pogrešno korisničko ime ili lozinka.");
                return View(dto);
            }

            var korisnikId = await response.Content.ReadFromJsonAsync<int>();

            HttpContext.Session.SetInt32("KorisnikId", korisnikId);
            HttpContext.Session.SetString("KorisnickoIme", dto.KorisnickoIme);

            return RedirectToAction("Index", "ZahtevZaPasos");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}