using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZakazivanjeTerminaPodaci.Migrations
{
    /// <inheritdoc />
    public partial class DodataNaknadaZaHitanPostupak : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HitanPostupak",
                table: "ZahteviZaPasos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "IznosNaknade",
                table: "ZahteviZaPasos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HitanPostupak",
                table: "ZahteviZaPasos");

            migrationBuilder.DropColumn(
                name: "IznosNaknade",
                table: "ZahteviZaPasos");
        }
    }
}
