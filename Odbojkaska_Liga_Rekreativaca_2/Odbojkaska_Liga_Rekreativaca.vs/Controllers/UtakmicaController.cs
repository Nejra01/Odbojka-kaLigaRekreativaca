using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Odbojkaska_Liga_Rekreativaca.Core.Modeli;
using Odbojkaska_Liga_Rekreativaca.Repository;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Dvorana;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Kanton;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Utakmica;
using System.Runtime.CompilerServices;

namespace Odbojkaska_Liga_Rekreativaca.vs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UtakmicaController : ControllerBase
    {
    private readonly AppDBContext _dbContext;


        public UtakmicaController(AppDBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        //dodavanje dvorane

        [HttpPost("/Utakmica/Add")]
        public ActionResult Dodaj([FromBody] UtakmicaAddVM x)
        {
            var utakmica = new Utakmica
            {
              NazivUtakmice = x.NazivUtakmice,    
              StatusID=x.StatusID,
              DatumIgranja=x.DatumIgranja,
              KoloID=x.KoloID,
              VrijemePocetka=x.VrijemePocetka,
                obrisan = false


            };
            _dbContext.Add(utakmica);
            _dbContext.SaveChanges();
            return Ok(utakmica);
        }


        //prikaz dvorana
        [HttpGet("/Utakmica/Get")]
        public List<UtakmicaGetAllVM> Get()
        {
            var upit = _dbContext.utakmica.OrderBy(p => p.UtakmicaID).Where(z => z.obrisan == false).Select(x => new UtakmicaGetAllVM
            {
                UtakmicaID = x.UtakmicaID,
                NazivUtakmice = x.NazivUtakmice,
                StatusID = x.StatusID,
                StatusOpis = x.Status.NazivStatusa,
                DatumIgranja = x.DatumIgranja,
                KoloID = x.KoloID,
                KoloOpis = x.Kolo.BrojKola.ToString(),
                VrijemePocetka = x.VrijemePocetka
        });
            return upit.ToList();
        }

        //update

        [HttpPost("/Utakmica/Update")]
        public ActionResult Update(int id, [FromBody] UtakmicaAddVM x)
        {
            //if (!HttpContext.GetLoginInfo().isPermisijaStudentskaSluzba)
            //    return BadRequest("nije logiran");
            Utakmica obj;
            if (id == 0)
            {
                return BadRequest("pogresan ID");
            }
            else
            {
                obj = _dbContext.utakmica.Where(p => p.UtakmicaID == id).FirstOrDefault();
                if (obj == null)
                    return BadRequest("pogresan ID");
            }
            obj.StatusID = x.StatusID;

            obj.DatumIgranja = x.DatumIgranja;
            obj.KoloID = x.KoloID;
            obj.VrijemePocetka = x.VrijemePocetka;
            obj.NazivUtakmice = x.NazivUtakmice;
            _dbContext.SaveChanges();
            return Ok(obj);
        }


        //brisanje  

        [HttpDelete("/Utakmica/PermanentDelete")]
        public ActionResult PermanentDelete(int id)
        {
            Utakmica obj = _dbContext.utakmica.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");

            _dbContext.Remove(obj);

            _dbContext.SaveChanges();
            return Ok(obj);
        }
        [HttpPost("/Utakmica/Delete")]
        public ActionResult Delete(int id)
        {
            Utakmica obj = _dbContext.utakmica.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");

            obj.obrisan = true;
            _dbContext.Update(obj);

            _dbContext.SaveChanges();
            return Ok(obj);
        }


        [HttpGet]
        public ActionResult GetById(int koloID)
        {
           List< Kolo> odabranoKolo=_dbContext.kolo.Where(x=>x.KoloID==koloID).Include(a=>a.Liga).ToList();
           
            Utakmica s = _dbContext.utakmica.Find(koloID);
          
            List<Utakmica> kola = _dbContext.utakmica
                .Include(a=>a.Kolo.Liga)
                .Include(a=>a.Status)
   
                .Where(s => s.KoloID == koloID && s.obrisan == false).ToList();



            return Ok(new { kola, odabranoKolo});
        }


    };

      

    }
