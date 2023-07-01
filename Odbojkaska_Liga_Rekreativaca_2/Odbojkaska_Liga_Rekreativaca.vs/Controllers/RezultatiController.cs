using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Odbojkaska_Liga_Rekreativaca.Core.Modeli;
using Odbojkaska_Liga_Rekreativaca.Repository;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Dvorana;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Kanton;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Rezultati;
using System.Runtime.CompilerServices;

namespace Odbojkaska_Liga_Rekreativaca.vs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RezultatiController : ControllerBase
    {
    private readonly AppDBContext _dbContext;


        public RezultatiController(AppDBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        //dodavanje dvorane

        [HttpPost("/Rezultati/Add")]
        public ActionResult Dodaj([FromBody] RezultatiAddVM x)
        {
            var NoviRezultati = new Rezultati
            {

               UtakmicaID=x.UtakmicaID,
               TimID=x.TimID,
               SetoviID =x.SetoviID,
               OsvojeniBodovi=x.OsvojeniBodovi,
               IzgubljeniBodovi=x.IzgubljeniBodovi,
                obrisan = false


            };
            _dbContext.Add(NoviRezultati);
            _dbContext.SaveChanges();
            return Ok(NoviRezultati);
        }


        //prikaz dvorana
        [HttpGet("/Rezultati/GetAll")]
        public List<RezultatiGetAllVM> Get()
        {
            var upit = _dbContext.rezultati.OrderBy(p => p.RezultatiID).Where(z => z.obrisan == false).Select(x=> new RezultatiGetAllVM {
                RezultatiID = x.RezultatiID,
                UtakmicaID = x.UtakmicaID,
                NazivUtakmice=x.Utakmica.NazivUtakmice,
                TimID = x.TimID,
                TimOpis=x.Tim.ImeTima,
                SetoviID = x.SetoviID,
                SetOpis=x.Setovi.BrojSeta,
                OsvojeniBodovi = x.OsvojeniBodovi,
                IzgubljeniBodovi = x.IzgubljeniBodovi
            });
            return upit.ToList();
        }
        //brisanje rezultata 

        [HttpDelete("/Rezultati/PermanentDelete")]
        public ActionResult PermanentDelete(int id)
        {
            Rezultati obj = _dbContext.rezultati.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");

            _dbContext.Remove(obj);

            _dbContext.SaveChanges();
            return Ok(obj);
        }
        [HttpPost("/Rezultati/Delete")]
        public ActionResult Delete(int id)
        {
            Rezultati obj = _dbContext.rezultati.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");

            obj.obrisan = true;
            _dbContext.Update(obj);

            _dbContext.SaveChanges();
            return Ok(obj);
        }
        //Update 
        [HttpPost("/Rezultati/Update")]
        public ActionResult Update(int id, [FromBody] RezultatiAddVM x)
        {
            //if (!HttpContext.GetLoginInfo().isPermisijaStudentskaSluzba)
            //    return BadRequest("nije logiran");

            Rezultati obj;

            if (id == 0)
            {

                return BadRequest("pogresan ID");
            }
            else
            {
                obj = _dbContext.rezultati.Where(p => p.RezultatiID == id).FirstOrDefault();
                // student = _dbContext.Student.Include(s => s.opstina_rodjenja.drzava).FirstOrDefault(s => s.id == id);
                if (obj == null)
                    return BadRequest("pogresan ID");
            }

            obj.UtakmicaID = x.UtakmicaID;
            obj.TimID = x.TimID;
            obj.SetoviID = x.SetoviID;
            obj.OsvojeniBodovi = x.OsvojeniBodovi;
            obj.IzgubljeniBodovi = x.IzgubljeniBodovi;
           
            _dbContext.SaveChanges();
            return Ok(obj);
        }

        [HttpGet]
        public ActionResult GetById(int utakmicaid)
        {
            List<Utakmica> odabranaUtakmica = _dbContext.utakmica
                .Where(x => x.UtakmicaID == utakmicaid).Include(a=>a.Kolo).Include(a=>a.Kolo.Liga).ToList();

            Utakmica s = _dbContext.utakmica.Find(utakmicaid);

            List<Rezultati> gardovi = _dbContext.rezultati
                 .Include(x => x.Tim)
                 .Include(x => x.Setovi)
                   .Include(x => x.Utakmica.Kolo)
                 .Include(x => x.Utakmica.Kolo.Liga)


                .Where(s => s.UtakmicaID == utakmicaid && s.obrisan == false).ToList();



            return Ok(new { gardovi, odabranaUtakmica });
        }
    };

      

    }
