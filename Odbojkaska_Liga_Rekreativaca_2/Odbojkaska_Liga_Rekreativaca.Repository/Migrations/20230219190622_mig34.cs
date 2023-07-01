using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OdbojkaskaLigaRekreativaca.Repository.Migrations
{
    /// <inheritdoc />
    public partial class mig34 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_autentifikacijaToken_korisnik_KorisnikID",
                table: "autentifikacijaToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_autentifikacijaToken",
                table: "autentifikacijaToken");

            migrationBuilder.RenameTable(
                name: "autentifikacijaToken",
                newName: "AutentifikacijaToken");

            migrationBuilder.RenameIndex(
                name: "IX_autentifikacijaToken_KorisnikID",
                table: "AutentifikacijaToken",
                newName: "IX_AutentifikacijaToken_KorisnikID");

            migrationBuilder.AddColumn<string>(
                name: "aktivacijaGUID",
                table: "korisnik",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isAktiviran",
                table: "korisnik",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "twoFCode",
                table: "AutentifikacijaToken",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "twoFJelOtkljucano",
                table: "AutentifikacijaToken",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AutentifikacijaToken",
                table: "AutentifikacijaToken",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_AutentifikacijaToken_korisnik_KorisnikID",
                table: "AutentifikacijaToken",
                column: "KorisnikID",
                principalTable: "korisnik",
                principalColumn: "KorisnikID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AutentifikacijaToken_korisnik_KorisnikID",
                table: "AutentifikacijaToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AutentifikacijaToken",
                table: "AutentifikacijaToken");

            migrationBuilder.DropColumn(
                name: "aktivacijaGUID",
                table: "korisnik");

            migrationBuilder.DropColumn(
                name: "isAktiviran",
                table: "korisnik");

            migrationBuilder.DropColumn(
                name: "twoFCode",
                table: "AutentifikacijaToken");

            migrationBuilder.DropColumn(
                name: "twoFJelOtkljucano",
                table: "AutentifikacijaToken");

            migrationBuilder.RenameTable(
                name: "AutentifikacijaToken",
                newName: "autentifikacijaToken");

            migrationBuilder.RenameIndex(
                name: "IX_AutentifikacijaToken_KorisnikID",
                table: "autentifikacijaToken",
                newName: "IX_autentifikacijaToken_KorisnikID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_autentifikacijaToken",
                table: "autentifikacijaToken",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_autentifikacijaToken_korisnik_KorisnikID",
                table: "autentifikacijaToken",
                column: "KorisnikID",
                principalTable: "korisnik",
                principalColumn: "KorisnikID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
