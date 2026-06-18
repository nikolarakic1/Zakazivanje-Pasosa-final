using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZakazivanjeTerminaPodaci.Migrations
{
    /// <inheritdoc />
    public partial class PocetnaMigracija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KorisnickoIme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LozinkaHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Uloga = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumKreiranja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumIzmene = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Obrisan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PodnosiociZahteva",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JMBG = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumRodjenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MestoRodjenja = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Drzavljanstvo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Grad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrojLicneKarte = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumVazenjaLicneKarte = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumKreiranja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumIzmene = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Obrisan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PodnosiociZahteva", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VrsteDokumenata",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Obavezno = table.Column<bool>(type: "bit", nullable: false),
                    DatumKreiranja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumIzmene = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Obrisan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VrsteDokumenata", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZahteviZaPasos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojZahteva = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumPodnosenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumTermina = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VremeTermina = table.Column<TimeSpan>(type: "time", nullable: false),
                    StatusZahteva = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImaVazecuLicnuKartu = table.Column<bool>(type: "bit", nullable: false),
                    DokumentacijaKompletna = table.Column<bool>(type: "bit", nullable: false),
                    Napomena = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RazlogOdbijanja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PodnosilacZahtevaId = table.Column<int>(type: "int", nullable: false),
                    KorisnikId = table.Column<int>(type: "int", nullable: false),
                    DatumKreiranja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumIzmene = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Obrisan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZahteviZaPasos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZahteviZaPasos_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ZahteviZaPasos_PodnosiociZahteva_PodnosilacZahtevaId",
                        column: x => x.PodnosilacZahtevaId,
                        principalTable: "PodnosiociZahteva",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dokumentacija",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dostavljeno = table.Column<bool>(type: "bit", nullable: false),
                    DatumDostavljanja = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Napomena = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZahtevZaPasosId = table.Column<int>(type: "int", nullable: false),
                    VrstaDokumentaId = table.Column<int>(type: "int", nullable: false),
                    DatumKreiranja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumIzmene = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Obrisan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dokumentacija", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dokumentacija_VrsteDokumenata_VrstaDokumentaId",
                        column: x => x.VrstaDokumentaId,
                        principalTable: "VrsteDokumenata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dokumentacija_ZahteviZaPasos_ZahtevZaPasosId",
                        column: x => x.ZahtevZaPasosId,
                        principalTable: "ZahteviZaPasos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dokumentacija_VrstaDokumentaId",
                table: "Dokumentacija",
                column: "VrstaDokumentaId");

            migrationBuilder.CreateIndex(
                name: "IX_Dokumentacija_ZahtevZaPasosId",
                table: "Dokumentacija",
                column: "ZahtevZaPasosId");

            migrationBuilder.CreateIndex(
                name: "IX_ZahteviZaPasos_KorisnikId",
                table: "ZahteviZaPasos",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_ZahteviZaPasos_PodnosilacZahtevaId",
                table: "ZahteviZaPasos",
                column: "PodnosilacZahtevaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dokumentacija");

            migrationBuilder.DropTable(
                name: "VrsteDokumenata");

            migrationBuilder.DropTable(
                name: "ZahteviZaPasos");

            migrationBuilder.DropTable(
                name: "Korisnici");

            migrationBuilder.DropTable(
                name: "PodnosiociZahteva");
        }
    }
}
