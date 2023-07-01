using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OdbojkaskaLigaRekreativaca.Repository.Migrations
{
    /// <inheritdoc />
    public partial class mih : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "kanton",
                columns: table => new
                {
                    KantonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivKantona = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    obrisan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kanton", x => x.KantonID);
                });

            migrationBuilder.CreateTable(
                name: "liga",
                columns: table => new
                {
                    LigaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GodinaLige = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    obrisan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_liga", x => x.LigaID);
                });

            migrationBuilder.CreateTable(
                name: "pozicija",
                columns: table => new
                {
                    PozicijaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivPozicije = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    obrisan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pozicija", x => x.PozicijaID);
                });

            migrationBuilder.CreateTable(
                name: "setovi",
                columns: table => new
                {
                    SetoviID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojSeta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    obrisan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_setovi", x => x.SetoviID);
                });

            migrationBuilder.CreateTable(
                name: "spol",
                columns: table => new
                {
                    SpolID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    obrisan = table.Column<bool>(type: "bit", nullable: false),
                    NazivSpola = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spol", x => x.SpolID);
                });

            migrationBuilder.CreateTable(
                name: "status",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivStatusa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    obrisan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_status", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "tim",
                columns: table => new
                {
                    TimID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImeTima = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    obrisan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tim", x => x.TimID);
                });

            migrationBuilder.CreateTable(
                name: "uloga",
                columns: table => new
                {
                    UlogaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivUloge = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    obrisan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_uloga", x => x.UlogaID);
                });

            migrationBuilder.CreateTable(
                name: "grad",
                columns: table => new
                {
                    GradID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImeGrada = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KantonID = table.Column<int>(type: "int", nullable: false),
                    obrisan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grad", x => x.GradID);
                    table.ForeignKey(
                        name: "FK_grad_kanton_KantonID",
                        column: x => x.KantonID,
                        principalTable: "kanton",
                        principalColumn: "KantonID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "kolo",
                columns: table => new
                {
                    KoloID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojKola = table.Column<int>(type: "int", nullable: false),
                    LigaID = table.Column<int>(type: "int", nullable: false),
                    obrisan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kolo", x => x.KoloID);
                    table.ForeignKey(
                        name: "FK_kolo_liga_LigaID",
                        column: x => x.LigaID,
                        principalTable: "liga",
                        principalColumn: "LigaID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "dvorana",
                columns: table => new
                {
                    DvoranaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GradID = table.Column<int>(type: "int", nullable: false),
                    ImeDvorane = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    obrisan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dvorana", x => x.DvoranaID);
                    table.ForeignKey(
                        name: "FK_dvorana_grad_GradID",
                        column: x => x.GradID,
                        principalTable: "grad",
                        principalColumn: "GradID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "igrac",
                columns: table => new
                {
                    IgraciD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumRodjenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BrojTelefona = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAdresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpolID = table.Column<int>(type: "int", nullable: false),
                    PozicijaID = table.Column<int>(type: "int", nullable: false),
                    GradID = table.Column<int>(type: "int", nullable: false),
                    obrisan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_igrac", x => x.IgraciD);
                    table.ForeignKey(
                        name: "FK_igrac_grad_GradID",
                        column: x => x.GradID,
                        principalTable: "grad",
                        principalColumn: "GradID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_igrac_pozicija_PozicijaID",
                        column: x => x.PozicijaID,
                        principalTable: "pozicija",
                        principalColumn: "PozicijaID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_igrac_spol_SpolID",
                        column: x => x.SpolID,
                        principalTable: "spol",
                        principalColumn: "SpolID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "korisnik",
                columns: table => new
                {
                    KorisnikID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumRodjenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BrojTelefona = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    korisnickoIme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lozinka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isAdmin = table.Column<bool>(type: "bit", nullable: false),
                    isSudija = table.Column<bool>(type: "bit", nullable: false),
                    isZapisnicar = table.Column<bool>(type: "bit", nullable: false),
                    GradID = table.Column<int>(type: "int", nullable: false),
                    SpolID = table.Column<int>(type: "int", nullable: false),
                    obrisan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_korisnik", x => x.KorisnikID);
                    table.ForeignKey(
                        name: "FK_korisnik_grad_GradID",
                        column: x => x.GradID,
                        principalTable: "grad",
                        principalColumn: "GradID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_korisnik_spol_SpolID",
                        column: x => x.SpolID,
                        principalTable: "spol",
                        principalColumn: "SpolID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "utakmica",
                columns: table => new
                {
                    UtakmicaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivUtakmice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumIgranja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VrijemePocetka = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false),
                    KoloID = table.Column<int>(type: "int", nullable: false),
                    obrisan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_utakmica", x => x.UtakmicaID);
                    table.ForeignKey(
                        name: "FK_utakmica_kolo_KoloID",
                        column: x => x.KoloID,
                        principalTable: "kolo",
                        principalColumn: "KoloID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_utakmica_status_StatusID",
                        column: x => x.StatusID,
                        principalTable: "status",
                        principalColumn: "StatusID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ligaDvorana",
                columns: table => new
                {
                    LigaDvoranaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DvoranaID = table.Column<int>(type: "int", nullable: false),
                    LigaID = table.Column<int>(type: "int", nullable: false),
                    obrisan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ligaDvorana", x => x.LigaDvoranaID);
                    table.ForeignKey(
                        name: "FK_ligaDvorana_dvorana_DvoranaID",
                        column: x => x.DvoranaID,
                        principalTable: "dvorana",
                        principalColumn: "DvoranaID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ligaDvorana_liga_LigaID",
                        column: x => x.LigaID,
                        principalTable: "liga",
                        principalColumn: "LigaID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "timIgrac",
                columns: table => new
                {
                    TimIgracID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimID = table.Column<int>(type: "int", nullable: false),
                    IgracID = table.Column<int>(type: "int", nullable: false),
                    obrisan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_timIgrac", x => x.TimIgracID);
                    table.ForeignKey(
                        name: "FK_timIgrac_igrac_IgracID",
                        column: x => x.IgracID,
                        principalTable: "igrac",
                        principalColumn: "IgraciD",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_timIgrac_tim_TimID",
                        column: x => x.TimID,
                        principalTable: "tim",
                        principalColumn: "TimID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "autentifikacijaToken",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vrijednost = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KorisnikID = table.Column<int>(type: "int", nullable: false),
                    vrijemeEvidentiranja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ipAdresa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_autentifikacijaToken", x => x.id);
                    table.ForeignKey(
                        name: "FK_autentifikacijaToken_korisnik_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "korisnik",
                        principalColumn: "KorisnikID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "korisnikUloga",
                columns: table => new
                {
                    KorisnikUlogaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikID = table.Column<int>(type: "int", nullable: false),
                    UlogaID = table.Column<int>(type: "int", nullable: false),
                    obrisan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_korisnikUloga", x => x.KorisnikUlogaID);
                    table.ForeignKey(
                        name: "FK_korisnikUloga_korisnik_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "korisnik",
                        principalColumn: "KorisnikID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_korisnikUloga_uloga_UlogaID",
                        column: x => x.UlogaID,
                        principalTable: "uloga",
                        principalColumn: "UlogaID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "rezultati",
                columns: table => new
                {
                    RezultatiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UtakmicaID = table.Column<int>(type: "int", nullable: false),
                    TimID = table.Column<int>(type: "int", nullable: false),
                    SetoviID = table.Column<int>(type: "int", nullable: false),
                    OsvojeniBodovi = table.Column<int>(type: "int", nullable: false),
                    IzgubljeniBodovi = table.Column<int>(type: "int", nullable: false),
                    obrisan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rezultati", x => x.RezultatiID);
                    table.ForeignKey(
                        name: "FK_rezultati_setovi_SetoviID",
                        column: x => x.SetoviID,
                        principalTable: "setovi",
                        principalColumn: "SetoviID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_rezultati_tim_TimID",
                        column: x => x.TimID,
                        principalTable: "tim",
                        principalColumn: "TimID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_rezultati_utakmica_UtakmicaID",
                        column: x => x.UtakmicaID,
                        principalTable: "utakmica",
                        principalColumn: "UtakmicaID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "timLiga",
                columns: table => new
                {
                    TimLigaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumPrijave = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimIgracID = table.Column<int>(type: "int", nullable: false),
                    LigaID = table.Column<int>(type: "int", nullable: false),
                    obrisan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_timLiga", x => x.TimLigaID);
                    table.ForeignKey(
                        name: "FK_timLiga_liga_LigaID",
                        column: x => x.LigaID,
                        principalTable: "liga",
                        principalColumn: "LigaID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_timLiga_timIgrac_TimIgracID",
                        column: x => x.TimIgracID,
                        principalTable: "timIgrac",
                        principalColumn: "TimIgracID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "utakmicaKorisnik",
                columns: table => new
                {
                    UtakmicaKorisnikID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UtakmicaID = table.Column<int>(type: "int", nullable: false),
                    KorisnikUlogaID = table.Column<int>(type: "int", nullable: false),
                    obrisan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_utakmicaKorisnik", x => x.UtakmicaKorisnikID);
                    table.ForeignKey(
                        name: "FK_utakmicaKorisnik_korisnikUloga_KorisnikUlogaID",
                        column: x => x.KorisnikUlogaID,
                        principalTable: "korisnikUloga",
                        principalColumn: "KorisnikUlogaID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_utakmicaKorisnik_utakmica_UtakmicaID",
                        column: x => x.UtakmicaID,
                        principalTable: "utakmica",
                        principalColumn: "UtakmicaID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "UtakmicaTimLiga",
                columns: table => new
                {
                    UtakmicaTimLigaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UtakmicaID = table.Column<int>(type: "int", nullable: false),
                    TimLigaID = table.Column<int>(type: "int", nullable: false),
                    obrisan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UtakmicaTimLiga", x => x.UtakmicaTimLigaID);
                    table.ForeignKey(
                        name: "FK_UtakmicaTimLiga_timLiga_TimLigaID",
                        column: x => x.TimLigaID,
                        principalTable: "timLiga",
                        principalColumn: "TimLigaID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UtakmicaTimLiga_utakmica_UtakmicaID",
                        column: x => x.UtakmicaID,
                        principalTable: "utakmica",
                        principalColumn: "UtakmicaID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "utakmicaTimLigaIgrac",
                columns: table => new
                {
                    UtakmicaTimLigaIgracID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UtakmicaTimLigaID = table.Column<int>(type: "int", nullable: false),
                    IgracID = table.Column<int>(type: "int", nullable: false),
                    obrisan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_utakmicaTimLigaIgrac", x => x.UtakmicaTimLigaIgracID);
                    table.ForeignKey(
                        name: "FK_utakmicaTimLigaIgrac_UtakmicaTimLiga_UtakmicaTimLigaID",
                        column: x => x.UtakmicaTimLigaID,
                        principalTable: "UtakmicaTimLiga",
                        principalColumn: "UtakmicaTimLigaID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_utakmicaTimLigaIgrac_igrac_IgracID",
                        column: x => x.IgracID,
                        principalTable: "igrac",
                        principalColumn: "IgraciD",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_autentifikacijaToken_KorisnikID",
                table: "autentifikacijaToken",
                column: "KorisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_dvorana_GradID",
                table: "dvorana",
                column: "GradID");

            migrationBuilder.CreateIndex(
                name: "IX_grad_KantonID",
                table: "grad",
                column: "KantonID");

            migrationBuilder.CreateIndex(
                name: "IX_igrac_GradID",
                table: "igrac",
                column: "GradID");

            migrationBuilder.CreateIndex(
                name: "IX_igrac_PozicijaID",
                table: "igrac",
                column: "PozicijaID");

            migrationBuilder.CreateIndex(
                name: "IX_igrac_SpolID",
                table: "igrac",
                column: "SpolID");

            migrationBuilder.CreateIndex(
                name: "IX_kolo_LigaID",
                table: "kolo",
                column: "LigaID");

            migrationBuilder.CreateIndex(
                name: "IX_korisnik_GradID",
                table: "korisnik",
                column: "GradID");

            migrationBuilder.CreateIndex(
                name: "IX_korisnik_SpolID",
                table: "korisnik",
                column: "SpolID");

            migrationBuilder.CreateIndex(
                name: "IX_korisnikUloga_KorisnikID",
                table: "korisnikUloga",
                column: "KorisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_korisnikUloga_UlogaID",
                table: "korisnikUloga",
                column: "UlogaID");

            migrationBuilder.CreateIndex(
                name: "IX_ligaDvorana_DvoranaID",
                table: "ligaDvorana",
                column: "DvoranaID");

            migrationBuilder.CreateIndex(
                name: "IX_ligaDvorana_LigaID",
                table: "ligaDvorana",
                column: "LigaID");

            migrationBuilder.CreateIndex(
                name: "IX_rezultati_SetoviID",
                table: "rezultati",
                column: "SetoviID");

            migrationBuilder.CreateIndex(
                name: "IX_rezultati_TimID",
                table: "rezultati",
                column: "TimID");

            migrationBuilder.CreateIndex(
                name: "IX_rezultati_UtakmicaID",
                table: "rezultati",
                column: "UtakmicaID");

            migrationBuilder.CreateIndex(
                name: "IX_timIgrac_IgracID",
                table: "timIgrac",
                column: "IgracID");

            migrationBuilder.CreateIndex(
                name: "IX_timIgrac_TimID",
                table: "timIgrac",
                column: "TimID");

            migrationBuilder.CreateIndex(
                name: "IX_timLiga_LigaID",
                table: "timLiga",
                column: "LigaID");

            migrationBuilder.CreateIndex(
                name: "IX_timLiga_TimIgracID",
                table: "timLiga",
                column: "TimIgracID");

            migrationBuilder.CreateIndex(
                name: "IX_utakmica_KoloID",
                table: "utakmica",
                column: "KoloID");

            migrationBuilder.CreateIndex(
                name: "IX_utakmica_StatusID",
                table: "utakmica",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_utakmicaKorisnik_KorisnikUlogaID",
                table: "utakmicaKorisnik",
                column: "KorisnikUlogaID");

            migrationBuilder.CreateIndex(
                name: "IX_utakmicaKorisnik_UtakmicaID",
                table: "utakmicaKorisnik",
                column: "UtakmicaID");

            migrationBuilder.CreateIndex(
                name: "IX_UtakmicaTimLiga_TimLigaID",
                table: "UtakmicaTimLiga",
                column: "TimLigaID");

            migrationBuilder.CreateIndex(
                name: "IX_UtakmicaTimLiga_UtakmicaID",
                table: "UtakmicaTimLiga",
                column: "UtakmicaID");

            migrationBuilder.CreateIndex(
                name: "IX_utakmicaTimLigaIgrac_IgracID",
                table: "utakmicaTimLigaIgrac",
                column: "IgracID");

            migrationBuilder.CreateIndex(
                name: "IX_utakmicaTimLigaIgrac_UtakmicaTimLigaID",
                table: "utakmicaTimLigaIgrac",
                column: "UtakmicaTimLigaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "autentifikacijaToken");

            migrationBuilder.DropTable(
                name: "ligaDvorana");

            migrationBuilder.DropTable(
                name: "rezultati");

            migrationBuilder.DropTable(
                name: "utakmicaKorisnik");

            migrationBuilder.DropTable(
                name: "utakmicaTimLigaIgrac");

            migrationBuilder.DropTable(
                name: "dvorana");

            migrationBuilder.DropTable(
                name: "setovi");

            migrationBuilder.DropTable(
                name: "korisnikUloga");

            migrationBuilder.DropTable(
                name: "UtakmicaTimLiga");

            migrationBuilder.DropTable(
                name: "korisnik");

            migrationBuilder.DropTable(
                name: "uloga");

            migrationBuilder.DropTable(
                name: "timLiga");

            migrationBuilder.DropTable(
                name: "utakmica");

            migrationBuilder.DropTable(
                name: "timIgrac");

            migrationBuilder.DropTable(
                name: "kolo");

            migrationBuilder.DropTable(
                name: "status");

            migrationBuilder.DropTable(
                name: "igrac");

            migrationBuilder.DropTable(
                name: "tim");

            migrationBuilder.DropTable(
                name: "liga");

            migrationBuilder.DropTable(
                name: "grad");

            migrationBuilder.DropTable(
                name: "pozicija");

            migrationBuilder.DropTable(
                name: "spol");

            migrationBuilder.DropTable(
                name: "kanton");
        }
    }
}
