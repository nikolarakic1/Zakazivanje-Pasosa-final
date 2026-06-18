# Zakazivanje termina za pasoše

Seminarski rad iz predmeta **Razvoj višeslojnog softvera**.

Aplikacija predstavlja poslovni sistem za evidenciju i obradu zahteva za izdavanje pasoša. Sistem omogućava prijavu referenta, unos podnosilaca zahteva, kreiranje zahteva za pasoš, evidenciju potrebne dokumentacije, obradu statusa zahteva i štampu pojedinačnog dokumenta.

## Tema projekta

**Naziv poslovnog procesa:** Zakazivanje termina i obrada zahteva za izdavanje pasoša

**Naziv dokumenta:** Zahtev za izdavanje pasoša

**Opis procesa:**  
Referent se prijavljuje u sistem, unosi podatke o podnosiocu zahteva, kreira zahtev za pasoš, zakazuje termin, evidentira dokumentaciju i menja status zahteva u skladu sa ispunjenim uslovima. Na kraju se može odštampati pojedinačni zahtev.

## Tehnologije

- ASP.NET Core MVC
- ASP.NET Core Web API
- MS SQL Server
- Entity Framework Core
- ADO.NET
- Bootstrap
- C#
- Višeslojna arhitektura

## Arhitektura projekta

Rešenje je podeljeno u više slojeva:

```text
ZakazivanjeTerminaZaPasose        - MVC prezentacioni sloj
ZakazivanjeTerminaServisi         - REST Web API sloj
ZakazivanjeTerminaServisniSloj    - servisni sloj
ZakazivanjeTerminaPodaci          - sloj za rad sa bazom / repository pattern
ZakazivanjeTerminaModeli          - domenski modeli
ZakazivanjeTerminaDTO             - DTO klase za prenos podataka
ZakazivanjeTerminaPoslovnaLogika  - poslovna pravila
```

## Glavni entiteti

### Korisnik

Predstavlja referenta koji koristi aplikaciju. Korisnik se prijavljuje preko login forme i njegov ID se čuva u sesiji. Taj korisnik se vezuje za zahtev kao referent koji je obradio ili uneo zahtev.

### PodnosilacZahteva

Predstavlja građanina za kog se pravi zahtev za pasoš. Sadrži podatke kao što su ime, prezime, JMBG, adresa, broj lične karte, kontakt podaci i datum važenja lične karte.

### ZahtevZaPasos

Glavna tabela aplikacije. Predstavlja zahtev za izdavanje pasoša. Povezuje podnosioca zahteva i korisnika/referenta. Sadrži broj zahteva, datum termina, vreme termina, status, vrstu pasoša, razlog izdavanja, mesto podnošenja i napomene.

### VrstaDokumenta

Šifarnik dokumenata. Predstavlja spisak dokumenata koji mogu biti potrebni za izdavanje pasoša.

Primeri:

```text
Lična karta
Fotografija
Dokaz o uplati
Stari pasoš
Izvod iz matične knjige rođenih
```

### Dokumentacija

Predstavlja konkretnu dokumentaciju za konkretan zahtev. Nije isto što i `VrstaDokumenta`.

Primer:

```text
Zahtev ZP-001:
- Lična karta: dostavljeno
- Fotografija: dostavljeno
- Dokaz o uplati: nije dostavljeno
```

Na taj način aplikacija ostvaruje master-detail odnos:

```text
ZahtevZaPasos 1 - N Dokumentacija
VrstaDokumenta 1 - N Dokumentacija
```

## Funkcionalnosti

## Login

Aplikacija ima login za korisnika/referenta. Login je implementiran preko ADO.NET-a, čime je ispunjen zahtev za korišćenje ADO.NET tehnologije.

Nakon uspešne prijave, ID korisnika se čuva u sesiji:

```text
KorisnikId = referent koji je prijavljen
```

Taj ID se automatski upisuje u zahtev prilikom kreiranja zahteva za pasoš.

## Podnosioci zahteva

Omogućene su CRUD operacije nad podnosiocima zahteva:

- pregled svih podnosilaca
- unos novog podnosioca
- izmena podataka
- brisanje podnosioca

## Šifarnik dokumenata

Stranica `Vrste dokumenata` predstavlja šifarnik potrebnih dokumenata. Tu se definiše koji dokumenti postoje u sistemu i da li su obavezni.

Primer:

```text
Lična karta - obavezno
Dokaz o uplati - obavezno
Fotografija - obavezno
Stari pasoš - opciono
```

## Zahtevi za pasoš

Glavni deo aplikacije. Omogućeno je:

- kreiranje zahteva
- pregled svih zahteva
- filtriranje po statusu i JMBG-u
- prikaz detalja zahteva
- obrada zahteva
- promena statusa
- brisanje zahteva
- štampa pojedinačnog zahteva

Kod kreiranja zahteva bira se podnosilac zahteva iz padajuće liste. Referent se ne bira ručno, već se uzima iz login sesije.

## Automatsko kreiranje dokumentacije

Kada se napravi novi zahtev, sistem automatski uzima sve stavke iz šifarnika `VrstaDokumenta` i kreira checklistu dokumentacije za taj zahtev.

Primer:

Ako u šifarniku postoje:

```text
Lična karta
Fotografija
Dokaz o uplati
```

nakon kreiranja zahteva automatski nastaje:

```text
Zahtev ZP-001:
- Lična karta: nije dostavljeno
- Fotografija: nije dostavljeno
- Dokaz o uplati: nije dostavljeno
```

Referent zatim uređuje dokumentaciju i označava šta je podnosilac dostavio.

## Dokumentacija zahteva

Stranica `Dokumentacija` prikazuje dokumentaciju grupisanu po zahtevu. Za svaki zahtev može se otvoriti uređivanje dokumentacije i označiti više dokumenata odjednom.

Funkcionalnosti:

- pregled dokumentacije po zahtevu
- grupisanje dokumenata
- označavanje više dokumenata kao dostavljeno
- unos datuma dostavljanja
- unos napomene

## Obrada zahteva

Na stranici za obradu zahteva referent može menjati:

- status zahteva
- datum i vreme termina
- vrstu pasoša
- razlog izdavanja
- mesto podnošenja
- napomenu
- razlog odbijanja
- proveru da li postoji važeća lična karta
- proveru da li je dokumentacija kompletna

Statusi zahteva:

```text
Na čekanju
U obradi
Dopuna dokumentacije
Odobren
Odbijen
Završen
```

## Poslovno pravilo

Poslovno pravilo je izdvojeno u poseban assembly:

```text
ZakazivanjeTerminaPoslovnaLogika
```

Pravilo se parametrizuje kroz `appsettings.json`:

 "PoslovnaPravila": {
   "NaknadaZaHitanPostupak": 3500
 },
```

Logika pravila:

```text
Ako se zahteva hitan postupak naplacuje se 3500.
```

Ovim je ispunjen zahtev da poslovna logika bude parametrizovana i izdvojena u poseban sloj.

## Štampa

Aplikacija ima parametarsku štampu pojedinačnog zahteva.

Putanja:

```text
/ZahtevZaPasos/Stampa/{id}
```

Štampa prikazuje:

- podatke o podnosiocu
- podatke o zahtevu
- status zahteva
- proveru dokumentacije
- napomenu
- razlog odbijanja
- mesto za potpis podnosioca
- mesto za potpis službenog lica

Ovo predstavlja dokument `Zahtev za izdavanje pasoša`.

## Validacije

U aplikaciji su predviđene osnovne validacije:

- obavezna polja
- izbor podnosioca zahteva
- izbor zahteva
- izbor vrste dokumenta
- validacija login podataka
- validacija statusa i poslovnih uslova

Na korisničkom interfejsu koriste se MVC validacije i Bootstrap prikaz grešaka.

## Repository pattern

Sloj `ZakazivanjeTerminaPodaci` koristi repository pattern.

Primeri repozitorijuma:

```text
PodnosilacZahtevaRepo
ZahtevZaPasosRepo
DokumentacijaRepo
VrstaDokumentaRepo
```

Repository klase rade sa Entity Framework Core i bazom podataka.

## ADO.NET

ADO.NET se koristi za login korisnika. Login servis direktno koristi `SqlConnection`, `SqlCommand` i parametrizovane SQL upite.

Time je ispunjen zahtev za korišćenje ADO.NET-a i parametrizovanog pristupa bazi.

## Entity Framework

Entity Framework Core se koristi za rad sa glavnim entitetima aplikacije, migracije i komunikaciju sa SQL Server bazom.

## Pokretanje projekta

Potrebno je pokrenuti dva projekta istovremeno:

```text
ZakazivanjeTerminaServisi      - Web API
ZakazivanjeTerminaZaPasose     - MVC aplikacija
```

U Visual Studio:

```text
Solution → Properties → Startup Project → Multiple startup projects
```

Postaviti:

```text
ZakazivanjeTerminaServisi      Start
ZakazivanjeTerminaZaPasose     Start
```

## Connection string

U Web API projektu `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=DESKTOP-72I79G6\\SQLEXPRESS;Database=ZakazivanjeTerminaZaPasoseDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

## Test korisnik

Primer test korisnika:

```text
Korisničko ime: admin
Lozinka: admin123
```

Primer SQL unosa:

```sql
INSERT INTO Korisnici
(
    DatumKreiranja,
    DatumIzmene,
    Obrisan,
    Ime,
    Prezime,
    KorisnickoIme,
    Email,
    LozinkaHash,
    Uloga
)
VALUES
(
    GETDATE(),
    NULL,
    0,
    'Admin',
    'Korisnik',
    'admin',
    'admin@test.com',
    'admin123',
    'Referent'
);
```

## Ispunjeni zahtevi seminarskog rada

- ASP.NET MVC prezentacioni sloj
- REST Web API servis
- MS SQL Server baza
- Entity Framework Core
- ADO.NET primer za login
- Repository pattern
- DTO klase
- Višeslojna arhitektura
- Srpski jezik u bazi, kodu i interfejsu
- Minimum 3 povezane tabele
- Nezavisna tabela korisnik
- Nasleđivanje kroz baznu klasu `Entitet`
- Multi-page aplikacija
- CRUD operacije
- Filter zahteva
- Master-detail odnos: `ZahtevZaPasos` i `Dokumentacija`
- Parametarska štampa dokumenta
- Poslovno pravilo u posebnom assembly-ju
- Parametrizacija poslovnog pravila kroz JSON

## Zaključak

Aplikacija prikazuje poslovni proces obrade zahteva za izdavanje pasoša kroz višeslojnu arhitekturu. Glavna tabela je `ZahtevZaPasos`, dok `Dokumentacija` predstavlja detail deo zahteva. Sistem omogućava prijavu referenta, kreiranje zahteva, obradu dokumentacije, promenu statusa i štampu pojedinačnog dokumenta.
