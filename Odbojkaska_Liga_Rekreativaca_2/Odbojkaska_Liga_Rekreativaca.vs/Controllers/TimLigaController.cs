using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Odbojkaska_Liga_Rekreativaca.Core.Modeli;
using Odbojkaska_Liga_Rekreativaca.Repository;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Dvorana;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Kanton;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.TimIgrac;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.TimLiga;
using System.Runtime.CompilerServices;

namespace Odbojkaska_Liga_Rekreativaca.vs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimLigaController : ControllerBase
    {
    private readonly AppDBContext _dbContext;


        public TimLigaController(AppDBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        //dodavanje dvorane

        [HttpPost("/TimLiga/Add")]
        public ActionResult Dodaj([FromBody] TimLigaAddVM x)
        {
            var noviTimLiga = new TimLiga
            {
            
                LigaID=x.LigaID,
                TimIgracID=x.TimIgracID,
                DatumPrijave=x.DatumPrijave,
                obrisan = false



            };
            _dbContext.Add(noviTimLiga);
            _dbContext.SaveChanges();
            return Ok(noviTimLiga);
        }


        //prikaz dvorana
        [HttpGet("/TimLiga/Get")]
        public List<TimLigaGetAllVM> Get()
        {
            var upit = _dbContext.timLiga.OrderBy(p => p.TimLigaID).Where(z => z.obrisan == false).Select(x=> new TimLigaGetAllVM
            {
                TimLigaID=x.TimLigaID,
                LigaID = x.LigaID,
                LigaOpis=x.Liga.GodinaLige,
                TimIgracID = x.TimIgracID,
                TimOpis=x.TimIgrac.Tim.ImeTima,
                IgracOpis=x.TimIgrac.Igrac.Ime+" "+x.TimIgrac.Igrac.Prezime,
                DatumPrijave = x.DatumPrijave

            });
            return upit.ToList();
        }

        //update

        [HttpPost("/TimLiga/Update")]
        public ActionResult Update(int id, [FromBody] TimLigaAddVM x)
        {
            //if (!HttpContext.GetLoginInfo().isPermisijaStudentskaSluzba)
            //    return BadRequest("nije logiran");
            TimLiga obj;
            if (id == 0)
            {
                return BadRequest("pogresan ID");
            }
            else
            {
                obj = _dbContext.timLiga.Where(p => p.TimLigaID == id).FirstOrDefault();
                if (obj == null)
                    return BadRequest("pogresan ID");
            }
            obj.DatumPrijave = x.DatumPrijave;
            obj.TimIgracID = x.TimIgracID;
            obj.LigaID = x.LigaID;
            _dbContext.SaveChanges();
            return Ok(obj);
        }


        //brisanje  

        [HttpDelete("/TimLiga/PermanentDelete")]
        public ActionResult PermanentDelete(int id)
        {
            TimLiga obj = _dbContext.timLiga.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");

            _dbContext.Remove(obj);

            _dbContext.SaveChanges();
            return Ok(obj);
        }
        [HttpPost("/TimLiga/Delete")]
        public ActionResult Delete(int id)
        {
            TimLiga obj = _dbContext.timLiga.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");

            obj.obrisan = true;
            _dbContext.Update(obj);

            _dbContext.SaveChanges();
            return Ok(obj);
        }

        //za get igraca

        //[HttpGet]
        //public ActionResult GetById(int timligaid)
        //{
        //    TimLiga s = _dbContext.timLiga.Find(timligaid);

        //    List<TimLiga> kola = _dbContext.timLiga
        //        .Include(a => a.TimIgrac)
        //        .Include(a => a.TimIgrac.Tim)
        //        .Include(a => a.TimIgrac.Igrac)
        //        .Include(a => a.TimIgrac.Igrac.Spol)
        //        .Include(a => a.TimIgrac.Igrac.Pozicija)
        //        .Include(a => a.TimIgrac.Igrac.Grad)
        //        .Include(a => a.TimIgrac.Igrac.Grad.Kanton)
        //        .Include(a => a.Liga)

        //        .Where(s => s.TimLigaID == timligaid && s.obrisan == false).ToList();



        //    return Ok(kola);
        //}
    };

      

    }
