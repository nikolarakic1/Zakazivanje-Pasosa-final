using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ZakazivanjeTerminaDTO;

namespace ZakazivanjeTerminaZaPasose.Controllers
{
    public class ZahtevZaPasosController : Controller
    {
        private readonly HttpClient _httpClient;

        public ZahtevZaPasosController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<IActionResult> Index(string? status, string? jmbg)
        {
            return await Indeks(status, jmbg);
        }

        public async Task<IActionResult> Indeks(string? status, string? jmbg)
        {
            var korisnikId = HttpContext.Session.GetInt32("KorisnikId");

            if (korisnikId is null)
                return RedirectToAction("Index", "Login");

            string ruta = "api/ZahtevZaPasos";

            if (!string.IsNullOrWhiteSpace(status) || !string.IsNullOrWhiteSpace(jmbg))
            {
                ruta = $"api/ZahtevZaPasos/filter?status={Uri.EscapeDataString(status ?? "")}&jmbg={Uri.EscapeDataString(jmbg ?? "")}";
            }

            var zahtevi = await _httpClient
                .GetFromJsonAsync<List<PrikazZahtevaZaPasosDTO>>(ruta);

            ViewBag.Status = status;
            ViewBag.Jmbg = jmbg;

            return View("Indeks", zahtevi ?? new List<PrikazZahtevaZaPasosDTO>());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return await Napravi();
        }

        [HttpGet]
        public async Task<IActionResult> Napravi()
        {
            var korisnikId = HttpContext.Session.GetInt32("KorisnikId");

            if (korisnikId is null)
                return RedirectToAction("Index", "Login");

            await NapuniPodnosioce();

            return View("Napravi", new ZahtevZaPasosDTO
            {
                StatusZahteva = "Na čekanju",
                DatumTermina = DateTime.Today,
                VremeTermina = new TimeSpan(9, 0, 0),
                MestoPodnosenja = "Zrenjanin",
                KorisnikId = korisnikId.Value,
                IznosNaknade = 0
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ZahtevZaPasosDTO dto)
        {
            return await Napravi(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Napravi(ZahtevZaPasosDTO dto)
        {
            var korisnikId = HttpContext.Session.GetInt32("KorisnikId");

            if (korisnikId is null)
                return RedirectToAction("Index", "Login");

            dto.KorisnikId = korisnikId.Value;

            if (dto.PodnosilacZahtevaId == 0)
                ModelState.AddModelError("PodnosilacZahtevaId", "Morate izabrati podnosioca zahteva.");

            if (!ModelState.IsValid)
            {
                await NapuniPodnosioce();
                return View("Napravi", dto);
            }

            var response = await _httpClient.PostAsJsonAsync("api/ZahtevZaPasos", dto);

            if (!response.IsSuccessStatusCode)
            {
                var greska = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", $"Greška prilikom čuvanja zahteva: {greska}");

                await NapuniPodnosioce();
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
            var korisnikId = HttpContext.Session.GetInt32("KorisnikId");

            if (korisnikId is null)
                return RedirectToAction("Index", "Login");

            var zahtev = await _httpClient
                .GetFromJsonAsync<PrikazZahtevaZaPasosDTO>($"api/ZahtevZaPasos/{id}");

            if (zahtev is null)
                return NotFound();

            await NapuniPodnosioce();

            var dto = new ZahtevZaPasosDTO
            {
                BrojZahteva = zahtev.BrojZahteva,
                DatumTermina = zahtev.DatumTermina,
                VremeTermina = zahtev.VremeTermina,
                StatusZahteva = zahtev.StatusZahteva,
                VrstaPasosa = zahtev.VrstaPasosa,
                RazlogIzdavanja = zahtev.RazlogIzdavanja,
                MestoPodnosenja = zahtev.MestoPodnosenja,
                ImaVazecuLicnuKartu = zahtev.ImaVazecuLicnuKartu,
                DokumentacijaKompletna = zahtev.DokumentacijaKompletna,
                HitanPostupak = zahtev.HitanPostupak,
                IznosNaknade = zahtev.IznosNaknade,
                Napomena = zahtev.Napomena,
                RazlogOdbijanja = zahtev.RazlogOdbijanja,
                PodnosilacZahtevaId = zahtev.PodnosilacZahtevaId,
                KorisnikId = korisnikId.Value
            };

            ViewBag.Id = id;

            return View("Izmeni", dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ZahtevZaPasosDTO dto)
        {
            return await Izmeni(id, dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Izmeni(int id, ZahtevZaPasosDTO dto)
        {
            var korisnikId = HttpContext.Session.GetInt32("KorisnikId");

            if (korisnikId is null)
                return RedirectToAction("Index", "Login");

            dto.KorisnikId = korisnikId.Value;

            if (dto.PodnosilacZahtevaId == 0)
                ModelState.AddModelError("PodnosilacZahtevaId", "Morate izabrati podnosioca zahteva.");

            if (!ModelState.IsValid)
            {
                ViewBag.Id = id;
                await NapuniPodnosioce();
                return View("Izmeni", dto);
            }

            var response = await _httpClient.PutAsJsonAsync($"api/ZahtevZaPasos/{id}", dto);

            if (!response.IsSuccessStatusCode)
            {
                var greska = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", $"Greška prilikom izmene zahteva: {greska}");

                ViewBag.Id = id;
                await NapuniPodnosioce();
                return View("Izmeni", dto);
            }

            return RedirectToAction(nameof(Indeks));
        }

        public async Task<IActionResult> Details(int id)
        {
            return await Detalji(id);
        }

        public async Task<IActionResult> Detalji(int id)
        {
            var korisnikId = HttpContext.Session.GetInt32("KorisnikId");

            if (korisnikId is null)
                return RedirectToAction("Index", "Login");

            var zahtev = await _httpClient
                .GetFromJsonAsync<PrikazZahtevaZaPasosDTO>($"api/ZahtevZaPasos/{id}");

            if (zahtev is null)
                return NotFound();

            return View("Detalji", zahtev);
        }

        public async Task<IActionResult> Stampa(int id)
        {
            var korisnikId = HttpContext.Session.GetInt32("KorisnikId");

            if (korisnikId is null)
                return RedirectToAction("Index", "Login");

            var zahtev = await _httpClient
                .GetFromJsonAsync<PrikazZahtevaZaPasosDTO>($"api/ZahtevZaPasos/{id}");

            if (zahtev is null)
                return NotFound();

            return View("Stampa", zahtev);
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
            var korisnikId = HttpContext.Session.GetInt32("KorisnikId");

            if (korisnikId is null)
                return RedirectToAction("Index", "Login");

            await _httpClient.DeleteAsync($"api/ZahtevZaPasos/{id}");

            return RedirectToAction(nameof(Indeks));
        }

        private async Task NapuniPodnosioce()
        {
            var podnosioci = await _httpClient
                .GetFromJsonAsync<List<PodnosilacZahtevaDTO>>("api/PodnosilacZahteva");

            ViewBag.Podnosioci = podnosioci?
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = $"{x.Ime} {x.Prezime} - {x.JMBG}"
                })
                .ToList() ?? new List<SelectListItem>();
        }
    }
}