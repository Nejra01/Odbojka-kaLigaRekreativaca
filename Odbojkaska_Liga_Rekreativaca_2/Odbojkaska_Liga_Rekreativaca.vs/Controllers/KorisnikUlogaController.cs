using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Odbojkaska_Liga_Rekreativaca.Core.Modeli;
using Odbojkaska_Liga_Rekreativaca.Repository;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Dvorana;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Kanton;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.KorisnikUloga;
using System.Runtime.CompilerServices;

namespace Odbojkaska_Liga_Rekreativaca.vs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KorisnikUlogaController : ControllerBase
    {
        private readonly AppDBContext _dbContext;


        public KorisnikUlogaController(AppDBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        //dodavanje dvorane

        [HttpPost("/KorisnikUloga/Add")]
        public ActionResult Dodaj([FromBody] KorisnikUlogaAddVM x)
        {
            var newKorisnikUloga = new KorisnikUloga
            {
                KorisnikID = x.KorisnikID,
                obrisan = false,

                UlogaID = x.UlogaID,


            };
            _dbContext.Add(newKorisnikUloga);
            _dbContext.SaveChanges();
            return Ok(newKorisnikUloga);
        }


        //prikaz dvorana
        [HttpGet("/KorisnikUloga/Get")]
        public List<KorisnikUlogaGetAllVM> Get()
        {
            var upit = _dbContext.korisnikUloga.OrderBy(p => p.KorisnikUlogaID).Where(z => z.obrisan == false).Select(x => new KorisnikUlogaGetAllVM
            {
                KorisnikUlogaID = x.KorisnikUlogaID,
                KorisnikID = x.KorisnikID,
                KorisnikOpis = x.Korisnik.Ime + " " + x.Korisnik.Prezime,
                UlogaID = x.UlogaID,
                UlogaOpis = x.Uloga.NazivUloge
            });
            return upit.ToList();
        }

        //update

        [HttpPost("KorisnikUloga/Update")]
        public ActionResult Update(int id, [FromBody] KorisnikUlogaAddVM x)
        {
            //if (!HttpContext.GetLoginInfo().isPermisijaStudentskaSluzba)
            //    return BadRequest("nije logiran");
            KorisnikUloga obj;
            if (id == 0)
            {
                return BadRequest("pogresan ID");
            }
            else
            {
                obj = _dbContext.korisnikUloga.Where(p => p.KorisnikUlogaID == id).FirstOrDefault();
                if (obj == null)
                    return BadRequest("pogresan ID");
            }
            obj.KorisnikID = x.KorisnikID;
            obj.UlogaID = x.UlogaID;
            _dbContext.SaveChanges();
            return Ok(obj);
        }

        //brisanje  

        [HttpPost("/KorisnikUloga/Delete")]
        public ActionResult Delete(int id)
        {
            KorisnikUloga obj = _dbContext.korisnikUloga.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");
            obj.obrisan = true;
            _dbContext.Update(obj);

            _dbContext.SaveChanges();
            return Ok(obj);
        }

        [HttpDelete("/KorisnikUloga/DeletePermanent")]
        public ActionResult DeletePermanent(int id)
        {
            KorisnikUloga obj = _dbContext.korisnikUloga.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");

            _dbContext.Remove(obj);

            _dbContext.SaveChanges();
            return Ok(obj);
        }

        [HttpGet]
        public ActionResult GetById(int korisnikid)
        {
            List<Korisnik> odabraniKanton = _dbContext.korisnik
               .Where(x => x.KorisnikID == korisnikid).ToList();
            KorisnikUloga s = _dbContext.korisnikUloga.Find(korisnikid);

            List<KorisnikUloga> gardovi = _dbContext.korisnikUloga.Include(x=> x.Korisnik).Include(x=>x.Uloga)

                .Where(s => s.KorisnikID == korisnikid && s.obrisan == false).ToList();



            return Ok(new { gardovi, odabraniKanton });
        }
    };



}
