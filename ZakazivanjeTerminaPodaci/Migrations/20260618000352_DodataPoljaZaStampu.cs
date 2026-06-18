using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZakazivanjeTerminaPodaci.Migrations
{
    /// <inheritdoc />
    public partial class DodataPoljaZaStampu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dokumentacija_VrsteDokumenata_VrstaDokumentaId",
                table: "Dokumentacija");

            migrationBuilder.DropForeignKey(
                name: "FK_ZahteviZaPasos_PodnosiociZahteva_PodnosilacZahtevaId",
                table: "ZahteviZaPasos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VrsteDokumenata",
                table: "VrsteDokumenata");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PodnosiociZahteva",
                table: "PodnosiociZahteva");

            migrationBuilder.RenameTable(
                name: "VrsteDokumenata",
                newName: "VrstaDokumenata");

            migrationBuilder.RenameTable(
                name: "PodnosiociZahteva",
                newName: "PodnosilacZahteva");

            migrationBuilder.AddColumn<string>(
                name: "MestoPodnosenja",
                table: "ZahteviZaPasos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RazlogIzdavanja",
                table: "ZahteviZaPasos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VrstaPasosa",
                table: "ZahteviZaPasos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VrstaDokumenata",
                table: "VrstaDokumenata",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PodnosilacZahteva",
                table: "PodnosilacZahteva",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dokumentacija_VrstaDokumenata_VrstaDokumentaId",
                table: "Dokumentacija",
                column: "VrstaDokumentaId",
                principalTable: "VrstaDokumenata",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ZahteviZaPasos_PodnosilacZahteva_PodnosilacZahtevaId",
                table: "ZahteviZaPasos",
                column: "PodnosilacZahtevaId",
                principalTable: "PodnosilacZahteva",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dokumentacija_VrstaDokumenata_VrstaDokumentaId",
                table: "Dokumentacija");

            migrationBuilder.DropForeignKey(
                name: "FK_ZahteviZaPasos_PodnosilacZahteva_PodnosilacZahtevaId",
                table: "ZahteviZaPasos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VrstaDokumenata",
                table: "VrstaDokumenata");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PodnosilacZahteva",
                table: "PodnosilacZahteva");

            migrationBuilder.DropColumn(
                name: "MestoPodnosenja",
                table: "ZahteviZaPasos");

            migrationBuilder.DropColumn(
                name: "RazlogIzdavanja",
                table: "ZahteviZaPasos");

            migrationBuilder.DropColumn(
                name: "VrstaPasosa",
                table: "ZahteviZaPasos");

            migrationBuilder.RenameTable(
                name: "VrstaDokumenata",
                newName: "VrsteDokumenata");

            migrationBuilder.RenameTable(
                name: "PodnosilacZahteva",
                newName: "PodnosiociZahteva");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VrsteDokumenata",
                table: "VrsteDokumenata",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PodnosiociZahteva",
                table: "PodnosiociZahteva",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dokumentacija_VrsteDokumenata_VrstaDokumentaId",
                table: "Dokumentacija",
                column: "VrstaDokumentaId",
                principalTable: "VrsteDokumenata",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ZahteviZaPasos_PodnosiociZahteva_PodnosilacZahtevaId",
                table: "ZahteviZaPasos",
                column: "PodnosilacZahtevaId",
                principalTable: "PodnosiociZahteva",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
