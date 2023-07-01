using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Odbojkaska_Liga_Rekreativaca.Core.Modeli;
using Odbojkaska_Liga_Rekreativaca.Repository;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Dvorana;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Kanton;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.UtakmicaTimLiga;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.UtakmicaTimLigaIgrac;
using System.Runtime.CompilerServices;

namespace Odbojkaska_Liga_Rekreativaca.vs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UtakmicaTimLigaIgracController : ControllerBase
    {
    private readonly AppDBContext _dbContext;


        public UtakmicaTimLigaIgracController(AppDBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        //dodavanje dvorane

        [HttpPost("/UtakmicaTimLigaIgrac/Add")]
        public ActionResult Dodaj([FromBody] UtakmicaTimLigaIgracAddVM x)
        {
            var newUtakmicaTimLigaIgrac = new UtakmicaTimLigaIgrac
            {

               UtakmicaTimLigaID=x.UtakmicaTimLigaID,
                obrisan=false,
               IgracID=x.IgracID,
            };
            _dbContext.Add(newUtakmicaTimLigaIgrac);
            _dbContext.SaveChanges();
            return Ok(newUtakmicaTimLigaIgrac);
        }


        //prikaz dvorana
        [HttpGet("/UtakmicaTimLigaIgrac/Get")]
        public List<UtakmicaTimLigaIgracGetAllVM> Get()
        {
            var upit = _dbContext.utakmicaTimLigaIgrac.OrderBy(p => p.UtakmicaTimLigaIgracID).Where(z => z.obrisan == false).Select(x=> new UtakmicaTimLigaIgracGetAllVM
            {
                UtakmicaTimLigaIgracID=x.UtakmicaTimLigaIgracID,
                UtakmicaTimLigaID=x.UtakmicaTimLigaID,
                UtakmicaNaziv=x.UtakmicaTimLiga.Utakmica.NazivUtakmice,
                TimOpis=x.UtakmicaTimLiga.TimLiga.TimIgrac.Tim.ImeTima,
                LigaOpis=x.UtakmicaTimLiga.TimLiga.Liga.GodinaLige,
                 IgracID = x.IgracID,
                 IgracOpis=x.Igrac.Ime+" "+x.Igrac.Prezime
            });
            return upit.ToList();
        }

        //update

        [HttpPost("/UtakmicaTimLigaIgrac/Update")]
        public ActionResult Update(int id, [FromBody] UtakmicaTimLigaIgracAddVM x)
        {
            //if (!HttpContext.GetLoginInfo().isPermisijaStudentskaSluzba)
            //    return BadRequest("nije logiran");
            UtakmicaTimLigaIgrac obj;
            if (id == 0)
            {
                return BadRequest("pogresan ID");
            }
            else
            {
                obj = _dbContext.utakmicaTimLigaIgrac.Where(p => p.UtakmicaTimLigaIgracID == id).FirstOrDefault();
                if (obj == null)
                    return BadRequest("pogresan ID");
            }
            obj.UtakmicaTimLigaID = x.UtakmicaTimLigaID;
            obj.IgracID = x.IgracID;
            _dbContext.SaveChanges();
            return Ok(obj);
        }


        //brisanje  

        [HttpDelete("/UtakmicaTimLigaIgrac/PermanentDelete")]
        public ActionResult PermanentDelete(int id)
        {
            UtakmicaTimLigaIgrac obj = _dbContext.utakmicaTimLigaIgrac.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");

            _dbContext.Remove(obj);

            _dbContext.SaveChanges();
            return Ok(obj);
        }
        [HttpPost("/UtakmicaTimLigaIgrac/Delete")]
        public ActionResult Delete(int id)
        {
            UtakmicaTimLigaIgrac obj = _dbContext.utakmicaTimLigaIgrac.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");

            obj.obrisan = true;
            _dbContext.Update(obj);

            _dbContext.SaveChanges();
            return Ok(obj);
        }


        [HttpGet]
        public ActionResult GetById(int utakmicatimligaid)
        {
            UtakmicaTimLigaIgrac s = _dbContext.utakmicaTimLigaIgrac.Find(utakmicatimligaid);

            List<UtakmicaTimLigaIgrac> kola = _dbContext.utakmicaTimLigaIgrac
                .Include(a => a.UtakmicaTimLiga)
                .Include(a => a.UtakmicaTimLiga.TimLiga.TimIgrac.Tim)
                .Include(a => a.UtakmicaTimLiga.TimLiga.Liga)
                .Include(a => a.UtakmicaTimLiga.Utakmica)
                .Include(a => a.UtakmicaTimLiga.Utakmica.Status)
                .Include(a => a.UtakmicaTimLiga.Utakmica.Kolo)
                .Include(x=>x.Igrac)
                .Include(x=>x.Igrac.Spol)
                .Include(x=>x.Igrac.Grad.Kanton)
                .Include(x=>x.Igrac.Grad)
                .Include(x=>x.Igrac.Pozicija)


                .Where(s => s.UtakmicaTimLigaID == utakmicatimligaid && s.obrisan == false).ToList();



            return Ok(kola);
        }
    };

      

    }
