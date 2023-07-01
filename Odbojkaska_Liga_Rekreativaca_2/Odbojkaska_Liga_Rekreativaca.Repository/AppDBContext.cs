using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Odbojkaska_Liga_Rekreativaca.Core.Modeli;

namespace Odbojkaska_Liga_Rekreativaca.Repository
{
    public class AppDBContext: DbContext
    {

        public AppDBContext(DbContextOptions<AppDBContext>opcije):base(opcije)
        {
                
        }
        public DbSet<Liga> liga { get; set; }
        public DbSet<Status> status { get; set; }
        public DbSet<Setovi> setovi { get; set; }
        public DbSet<Dvorana> dvorana { get; set; }
        public DbSet<Grad> grad { get; set; }
        public DbSet<Igrac> igrac { get; set; }
        public DbSet<Kanton> kanton { get; set; }
        public DbSet<Kolo> kolo { get; set; }
        public DbSet<Korisnik> korisnik { get; set; }
        public DbSet<KorisnikUloga> korisnikUloga { get; set; }
        public DbSet<LigaDvorana> ligaDvorana{ get; set; }
        public DbSet<Pozicija> pozicija { get; set; }
        public DbSet<Rezultati> rezultati { get; set; }
        public DbSet<Spol> spol { get; set; }
        public DbSet<Tim> tim { get; set; }
        public DbSet<TimIgrac> timIgrac { get; set; }
        public DbSet<TimLiga> timLiga { get; set; }
        public DbSet<Uloga> uloga { get; set; }
        public DbSet<Utakmica> utakmica { get; set; }
        public DbSet<UtakmicaTimLiga> UtakmicaTimLiga { get; set; }
        public DbSet<UtakmicaKorisnik> utakmicaKorisnik { get; set; }
        public DbSet<UtakmicaTimLigaIgrac> utakmicaTimLigaIgrac { get; set; }
        public DbSet<AutentifikacijaToken> autentifikacijaToken { get; set; }

        public DbSet<AutentifikacijaToken> AutentifikacijaToken { get; set; } = null!;







    }
}