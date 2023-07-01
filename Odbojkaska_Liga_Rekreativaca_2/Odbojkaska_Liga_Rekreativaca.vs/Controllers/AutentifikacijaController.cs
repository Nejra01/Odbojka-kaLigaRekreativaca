using System;
using System.Collections.Generic;
using System.Linq;

using FIT_Api_Examples.Helper;
using FIT_Api_Examples.Helper.AutentifikacijaAutorizacija;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Odbojkaska_Liga_Rekreativaca.Core.Modeli;
using Odbojkaska_Liga_Rekreativaca.Repository;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli;
using static FIT_Api_Examples.Helper.AutentifikacijaAutorizacija.MyAuthTokenExtension;

namespace FIT_Api_Examples.Modul0_Autentifikacija.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class AutentifikacijaController : ControllerBase
    {
        private readonly AppDBContext _dbContext;

        public AutentifikacijaController(AppDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet("{code}")]
        public ActionResult Otkljucaj(string code)
        {
            var korisnickiNalog = HttpContext.GetLoginInfo().korisnickiNalog;

            if (korisnickiNalog == null)
            {
                return BadRequest("korisnik nije logiran");
            }

            var token = _dbContext.AutentifikacijaToken.FirstOrDefault(s => s.twoFCode == code && s.KorisnikID == korisnickiNalog.KorisnikID);
            if (token != null)
            {
                token.twoFJelOtkljucano = true;
                _dbContext.SaveChanges();
                return Ok();
            }

            return BadRequest("pogresan URL");
        }


        [HttpPost]
        public ActionResult<LoginInformacije> Login([FromBody] LoginVM x)
        {
            //1- provjera logina
            Korisnik? logiraniKorisnik = _dbContext.korisnik
                .FirstOrDefault(k =>
                 k.korisnickoIme == x.korisnickoIme && k.lozinka == x.lozinka);

            if (logiraniKorisnik == null)
            {
                //pogresan username i password
                return new LoginInformacije(null);
            }

            //2- generisati random string
            string randomString = TokenGenerator.Generate(10);
            string twoFCode = TokenGenerator.Generate(4);

            //3- dodati novi zapis u tabelu AutentifikacijaToken za logiraniKorisnikId i randomString
            var noviToken = new AutentifikacijaToken()
            {
                ipAdresa = Request.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "",
                vrijednost = randomString,
                Korisnik = logiraniKorisnik,
                vrijemeEvidentiranja = DateTime.Now,
                twoFCode = twoFCode
            };

            _dbContext.Add(noviToken);
            _dbContext.SaveChanges();

            EmailLog.uspjesnoLogiranKorisnik(noviToken, Request.HttpContext);

            //4- vratiti token string
            return new LoginInformacije(noviToken);
        }

        [HttpPost]
        public ActionResult Logout()
        {
            AutentifikacijaToken autentifikacijaToken = HttpContext.GetAuthToken();

            if (autentifikacijaToken == null)
                return Ok();

            _dbContext.Remove(autentifikacijaToken);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public ActionResult<AutentifikacijaToken> Get()
        {
            AutentifikacijaToken autentifikacijaToken = HttpContext.GetAuthToken();

            return autentifikacijaToken;
        }
    }
}