using System.Linq;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Odbojkaska_Liga_Rekreativaca.Core.Modeli;
using Odbojkaska_Liga_Rekreativaca.Repository;

namespace FIT_Api_Examples.Helper.AutentifikacijaAutorizacija
{
    public static class MyAuthTokenExtension
    {
        public class LoginInformacije
        {
            public LoginInformacije(AutentifikacijaToken? autentifikacijaToken)
            {
                this.autentifikacijaToken = autentifikacijaToken;
            }

            [JsonIgnore]
            public Korisnik? korisnickiNalog => autentifikacijaToken?.Korisnik;
            public AutentifikacijaToken? autentifikacijaToken { get; set; }
            
            public bool isLogiran => korisnickiNalog != null;
         
        }


        public static LoginInformacije GetLoginInfo(this HttpContext httpContext)
        {
            var token = httpContext.GetAuthToken();

            return new LoginInformacije(token);
        }

        public static AutentifikacijaToken? GetAuthToken(this HttpContext httpContext)
        {
            string token = httpContext.GetMyAuthToken();
            AppDBContext? db = httpContext.RequestServices.GetService<AppDBContext>();

            AutentifikacijaToken? korisnickiNalog = db?.AutentifikacijaToken
                .Include(s => s.Korisnik)
                .SingleOrDefault(x => x.vrijednost == token);

            return korisnickiNalog;
        }

        public static string GetMyAuthToken(this HttpContext httpContext)
        {
            string token = httpContext.Request.Headers["autentifikacija-token"];
            return token;
        }
    }
}
