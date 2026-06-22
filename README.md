# Zakazivanje termina za pasoše

Seminarski rad iz predmeta **Razvoj višeslojnog softvera**.

Aplikacija predstavlja sistem za zakazivanje termina i obradu zahteva za izdavanje pasoša. Sistem omogućava evidenciju podnosilaca zahteva, kreiranje i obradu zahteva, vođenje dokumentacije, proveru kompletiranosti dokumentacije, primenu poslovnih pravila i pregled podataka kroz MVC korisnički interfejs.

## Tehnologije

Projekat je izrađen korišćenjem sledećih tehnologija:

- ASP.NET Core MVC
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- ADO.NET
- Repository Pattern
- Dependency Injection
- DTO objekti
- Bootstrap
- JavaScript validacija

## Arhitektura projekta

Projekat je realizovan kao višeslojna aplikacija. Rešenje je podeljeno na više projekata radi jasnog razdvajanja odgovornosti.

```text
ZakazivanjeTerminaZaPasose
├── ZakazivanjeTerminaZaPasose
├── ZakazivanjeTerminaServisniSloj
├── ZakazivanjeTerminaPodaci
├── ZakazivanjeTerminaModeli
├── ZakazivanjeTerminaDTO
└── ZakazivanjeTerminaPoslovnaLogika
ZakazivanjeTerminaZaPasose

MVC prezentacioni sloj aplikacije.
Sadrži kontrolere, Razor View stranice, Bootstrap stilizaciju i korisnički interfejs. MVC sloj ne pristupa direktno bazi podataka, već komunicira sa Web API slojem preko HttpClient.

## ZakazivanjeTerminaServisniSloj

Web API sloj aplikacije.
Sadrži API kontrolere koji primaju zahteve iz MVC sloja i pozivaju odgovarajuće servise. Ovaj sloj predstavlja vezu između korisničkog interfejsa i poslovne logike aplikacije.

## ZakazivanjeTerminaPodaci

Sloj pristupa podacima.
Sadrži AppDbContext, repozitorijume, interfejse repozitorijuma, migracije i pomoćnu klasu DBUtils za ADO.NET pristup bazi.

ZakazivanjeTerminaModeli

Sloj domenskih modela.
Sadrži entitete aplikacije koji predstavljaju tabele u bazi podataka.

## ZakazivanjeTerminaDTO

Sloj objekata za prenos podataka.
DTO klase se koriste za prenos podataka između MVC sloja, Web API sloja i servisnog sloja.

## ZakazivanjeTerminaPoslovnaLogika

Sloj poslovne logike.
Sadrži poslovna pravila, uključujući pravilo za obračun dodatne naknade u slučaju hitnog postupka.

## Glavni entiteti

Aplikacija koristi sledeće glavne entitete:

Korisnik
PodnosilacZahteva
ZahtevZaPasos
Dokumentacija
VrstaDokumenta
Funkcionalnosti

## Aplikacija omogućava:

prijavu korisnika
evidenciju podnosilaca zahteva
kreiranje zahteva za izdavanje pasoša
izmenu, pregled i brisanje zahteva
zakazivanje termina za obradu zahteva
evidenciju potrebne dokumentacije
automatsko kreiranje dokumentacije prilikom unosa zahteva
označavanje dostavljenih dokumenata
pregled statusa dokumentacije po zahtevu
obradu statusa zahteva
primenu poslovnog pravila za hitan postupak
pregled i štampu zahteva
Poslovno pravilo

U aplikaciji je implementirano poslovno pravilo za hitan postupak.
Ako je za zahtev označen hitan postupak, sistem automatski obračunava dodatnu naknadu. Iznos naknade se učitava iz JSON konfiguracije u fajlu appsettings.json.

## Primer konfiguracije:

"PoslovnaPravila": {
  "NaknadaZaHitanPostupak": 3500
}
Validacija podataka

Validacija je realizovana na više nivoa:

HTML validacija u Razor View stranicama
JavaScript validacija na korisničkom interfejsu
provera modela u MVC kontrolerima pomoću ModelState.IsValid
serverska validacija u Web API kontrolerima
dodatne ručne provere u servisnom sloju

Na ovaj način se sprečava unos nepotpunih i neispravnih podataka.

Rad sa bazom podataka

Za rad sa bazom koristi se SQL Server.
Entity Framework Core je korišćen za rad sa entitetima, repozitorijumima i migracijama.

Pored Entity Framework Core pristupa, u aplikaciji je prikazana i upotreba ADO.NET tehnologije kroz klasu DBUtils, koja se koristi za kreiranje konekcije, komandi i parametara prilikom prijave korisnika.

## Repository Pattern

U sloju podataka koristi se Repository Pattern.
Svaki važan entitet ima odgovarajući repozitorijum i interfejs repozitorijuma. Na taj način se odvaja logika pristupa podacima od servisnog sloja i ostatka aplikacije.

Primeri repozitorijuma:

PodnosilacZahtevaRepo
ZahtevZaPasosRepo
DokumentacijaRepo
VrstaDokumentaRepo
Pokretanje aplikacije

Za pokretanje aplikacije potrebno je:

Otvoriti solution fajl:
ZakazivanjeTerminaZaPasose.sln
Podesiti connection string u appsettings.json.

Primer:

"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=ZakazivanjeTerminaZaPasoseDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
Pokrenuti migracije i kreirati bazu:
Update-Database

ili pomoću .NET CLI alata:

dotnet ef database update
Pokrenuti Web API projekat:
ZakazivanjeTerminaServisniSloj
Pokrenuti MVC projekat:
ZakazivanjeTerminaZaPasose

MVC aplikacija komunicira sa Web API slojem preko podešenog HttpClient klijenta.

## Napomena

Projekat je izrađen kao seminarski rad i demonstrira primenu višeslojne arhitekture, MVC obrasca, Web API sloja, DTO objekata, Repository Pattern-a, Entity Framework Core-a, ADO.NET pristupa i osnovnih poslovnih pravila.
