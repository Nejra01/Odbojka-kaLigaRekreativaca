using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Odbojkaska_Liga_Rekreativaca.Core.Modeli;
using Odbojkaska_Liga_Rekreativaca.Repository;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Dvorana;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Kanton;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.KorisnikUloga;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.UtakmicaKorisnik;
using System.Runtime.CompilerServices;

namespace Odbojkaska_Liga_Rekreativaca.vs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UtakmicaKorisnikController : ControllerBase
    {
    private readonly AppDBContext _dbContext;


        public UtakmicaKorisnikController(AppDBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        //dodavanje dvorane

        [HttpPost("/UtakmicaKorisnik/Add")]
        public ActionResult Dodaj([FromBody] UtakmicaKorisnikAddVM x)
        {
            var newKorisnikUloga = new UtakmicaKorisnik
            {

                KorisnikUlogaID = x.KorisnikID,
                UtakmicaID = x.UtakmicaID,
                obrisan = false


            };
            _dbContext.Add(newKorisnikUloga);
            _dbContext.SaveChanges();
            return Ok(newKorisnikUloga);
        }


        //prikaz dvorana
        [HttpGet("/UtakmicaKorisnik/Get")]
        public List<UtakmicaKorisnikGetAllVM> Get()
        {
            var upit = _dbContext.utakmicaKorisnik.OrderBy(p => p.UtakmicaKorisnikID).Where(z=>z.obrisan==false).Select(x=> new UtakmicaKorisnikGetAllVM
            {
               UtakmicaKorisnikID=x.UtakmicaKorisnikID,
                KorisnikUlogaID = x.KorisnikUlogaID,
               KorinsikulogaOpis=x.Korisnikuloga.Korisnik.Ime+" "+x.Korisnikuloga.Korisnik.Prezime,
                UtakmicaID = x.UtakmicaID, 
                NazivUtakmice=x.Utakmica.NazivUtakmice
            });
            return upit.ToList();
        }


        //update

        [HttpPost("/UtakmicaKorisnik/Update")]
        public ActionResult Update(int id, [FromBody] UtakmicaKorisnikAddVM x)
        {
            //if (!HttpContext.GetLoginInfo().isPermisijaStudentskaSluzba)
            //    return BadRequest("nije logiran");
            UtakmicaKorisnik obj;
            if (id == 0)
            {
                return BadRequest("pogresan ID");
            }
            else
            {
                obj = _dbContext.utakmicaKorisnik.Where(p => p.UtakmicaKorisnikID == id).FirstOrDefault();
                if (obj == null)
                    return BadRequest("pogresan ID");
            }
            obj.KorisnikUlogaID = x.KorisnikID;
            obj.UtakmicaID = x.UtakmicaID;
            _dbContext.SaveChanges();
            return Ok(obj);
        }

        //brisanje  

        [HttpPost("/UtakmicaKorisnik/Delete")]
        public ActionResult Delete(int id)
        {
            UtakmicaKorisnik obj = _dbContext.utakmicaKorisnik.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");
            obj.obrisan = true;
            _dbContext.Update(obj);

            _dbContext.SaveChanges();
            return Ok(obj);
        }
        [HttpDelete("/UtakmicaKorisnik/DeletePermanent")]
        public ActionResult DeletePermanent(int id)
        {
            UtakmicaKorisnik obj = _dbContext.utakmicaKorisnik.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");

            _dbContext.Remove(obj);

            _dbContext.SaveChanges();
            return Ok(obj);
        }

        [HttpGet]

        public ActionResult GetById(int utakmicaid)
        {

            List<Utakmica> odabranaUtakmica = _dbContext.utakmica.Where(x => x.UtakmicaID == utakmicaid).ToList();
            UtakmicaKorisnik s = _dbContext.utakmicaKorisnik.Find(utakmicaid);

            List<UtakmicaKorisnik> gardovi = _dbContext.utakmicaKorisnik
                 .Include(x => x.Korisnikuloga.Korisnik)
               //  .Include(x => x.Korisnik.Uloga)
                 .Include(x => x.Korisnikuloga.Korisnik.Grad)
                 .Include(x => x.Korisnikuloga.Korisnik.Grad.Kanton)
                 .Include(x => x.Korisnikuloga.Uloga)

                .Where(s => s.UtakmicaID == utakmicaid && s.obrisan == false).ToList();



            return Ok(new { gardovi, odabranaUtakmica});
        }

    };

      

    }
