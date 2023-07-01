using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odbojkaska_Liga_Rekreativaca.Core.Modeli
{
    public class EmailLog
    {
        public static void uspjesnoLogiranKorisnik(AutentifikacijaToken token, HttpContext httpContext)
        {
            var logiraniKorisnik = token.Korisnik;
            if (logiraniKorisnik.isZapisnicar || logiraniKorisnik.isAdmin || logiraniKorisnik.isSudija)
            {
                var poruka = $"Postovani {logiraniKorisnik.korisnickoIme}, <br> " +
                              $"Code za 2F je <br>" +
                              $"{token.twoFCode}<br>" +
                              $"Login info {DateTime.Now}";


                EmailSender.Posalji("nejra.zlotrg@edu.fit.ba", "Code za 2F autorizaciju", poruka, true);
            }
        }

        public static void noviKorisnik(Korisnik nastavnik, HttpContext httpContext)
        {
            if (!nastavnik.isAktiviran)
            {
                var Request = httpContext.Request;
                var location = $"{Request.Scheme}://{Request.Host}";


                string url = location + "/nastavnik/Aktivacija/" + nastavnik.aktivacijaGUID;
                string poruka = $"Postovani/a {nastavnik.korisnickoIme}, <br> Link za aktivaciju <a href='{url}'>{url}</a>... {DateTime.Now}";
                EmailSender.Posalji("nejra.zlotrg@edu.fit.ba", "Aktivacija korisnika", poruka, true);

            }
        }
    }
}

