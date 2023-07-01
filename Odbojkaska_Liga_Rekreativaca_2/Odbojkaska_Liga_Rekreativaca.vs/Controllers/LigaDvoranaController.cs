using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Odbojkaska_Liga_Rekreativaca.Core.Modeli;
using Odbojkaska_Liga_Rekreativaca.Repository;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Dvorana;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Kanton;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.LigaDvorana;
using System.Runtime.CompilerServices;

namespace Odbojkaska_Liga_Rekreativaca.vs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LigaDvoranaController : ControllerBase
    {
    private readonly AppDBContext _dbContext;


        public LigaDvoranaController(AppDBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        //dodavanje dvorane

        [HttpPost("/LigaDvorana/Add")]
        public ActionResult Dodaj([FromBody] LigaDvoranaAddVM x)
        {
            var novaLigaDvorana = new LigaDvorana
            {

                LigaID=x.LigaID,
                DvoranaID=x.DvoranaID,
                obrisan = false

            };
            _dbContext.Add(novaLigaDvorana);
            _dbContext.SaveChanges();
            return Ok(novaLigaDvorana);
        }


        //prikaz dvorana
        [HttpGet("/LigaDvorana/Get")]
        public List<LigaDvoranaGetAllVM> Get()
        {
            var upit = _dbContext.ligaDvorana.OrderBy(p => p.LigaDvoranaID).Where(z => z.obrisan == false).Select(x=> new LigaDvoranaGetAllVM
            {
                LigaDvoranaID = x.LigaDvoranaID,
                LigaID = x.LigaID,
                LigaOpis=x.Liga.GodinaLige,
                DvoranaID = x.DvoranaID, 
                DvoranaOpis=x.Dvorana.ImeDvorane
            });
            return upit.ToList();
        }

        //update dvoroana

        [HttpPost("/LigaDvorana/Update")]
        public ActionResult Update(int id, [FromBody] LigaDvoranaAddVM x)
        {
            //if (!HttpContext.GetLoginInfo().isPermisijaStudentskaSluzba)
            //    return BadRequest("nije logiran");
            LigaDvorana obj;
            if (id == 0)
            {
                return BadRequest("pogresan ID");
            }
            else
            {
                obj = _dbContext.ligaDvorana.Where(p => p.LigaDvoranaID == id).FirstOrDefault();
                if (obj == null)
                    return BadRequest("pogresan ID");
            }
            obj.DvoranaID = x.DvoranaID;
            obj.LigaID = x.LigaID;
            _dbContext.SaveChanges();
            return Ok(obj);
        }


        //brisanje  

        [HttpDelete("/LigaDvorana/PermanentDelete")]
        public ActionResult PermanentDelete(int id)
        {
            LigaDvorana obj = _dbContext.ligaDvorana.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");

            _dbContext.Remove(obj);

            _dbContext.SaveChanges();
            return Ok(obj);
        }
        [HttpPost("/LigaDvorana/Delete")]
        public ActionResult Delete(int id)
        {
            LigaDvorana obj = _dbContext.ligaDvorana.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");

            obj.obrisan = true;
            _dbContext.Update(obj);

            _dbContext.SaveChanges();
            return Ok(obj);
        }

        [HttpGet]
                public ActionResult GetById(int ligaid)
        {
            List<Liga> odabraniKanton = _dbContext.liga
               .Where(x => x.LigaID == ligaid).ToList();
            LigaDvorana s = _dbContext.ligaDvorana.Find(ligaid);

            List<LigaDvorana> gardovi = _dbContext.ligaDvorana
                .Include(s=>s.Dvorana).Include(s=>s.Dvorana.Grad).Include(s=>s.Dvorana.Grad.Kanton)
           
                .Where(s => s.LigaID == ligaid && s.obrisan == false).ToList();



            return Ok(new { gardovi, odabraniKanton });
        }
    };

      

    }
