using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Odbojkaska_Liga_Rekreativaca.Core.Modeli;
using Odbojkaska_Liga_Rekreativaca.Repository;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Dvorana;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Kanton;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.UtakmicaTimLiga;
using System.Runtime.CompilerServices;

namespace Odbojkaska_Liga_Rekreativaca.vs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UtakmicaTimLigaController : ControllerBase
    {
    private readonly AppDBContext _dbContext;


        public UtakmicaTimLigaController(AppDBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        //dodavanje dvorane

        [HttpPost("/UtakmicaTimLiga/Add")]
        public ActionResult Dodaj([FromBody] UtakmicaTimLigaAddVM x)
        {
            var newUtakmicaTimLiga = new UtakmicaTimLiga
            {

               UtakmicaID=x.UtakmicaID,
               TimLigaID=x.TimLigaID,
                obrisan = false

            };
            _dbContext.Add(newUtakmicaTimLiga);
            _dbContext.SaveChanges();
            return Ok(newUtakmicaTimLiga);
        }


        //prikaz dvorana
        [HttpGet("/UtakmicaTimLiga/GetAll")]
        public List<UtakmicaTimLigaGetAllVM> Get()
        {
            var upit = _dbContext.UtakmicaTimLiga.OrderBy(p => p.UtakmicaTimLigaID).Where(z => z.obrisan == false).Select(x=> new UtakmicaTimLigaGetAllVM
            {
                UtakmicaTimLigaID=x.UtakmicaTimLigaID,
                UtakmicaID = x.UtakmicaID,
                NazivUtakmice=x.Utakmica.NazivUtakmice,
                TimLigaID = x.TimLigaID,
                TimOpis = x.TimLiga.TimIgrac.Tim.ImeTima,
                LigaOpis = x.TimLiga.Liga.GodinaLige
            });
            return upit.ToList();
        }
        [HttpDelete("/UtakmicaTimLiga/PermanentDelete")]
        public ActionResult PermanentDelete(int id)
        {
            UtakmicaTimLiga obj = _dbContext.UtakmicaTimLiga.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");

            _dbContext.Remove(obj);

            _dbContext.SaveChanges();
            return Ok(obj);
        }
        [HttpPost("/UtakmicaTimLiga/Delete")]
        public ActionResult Delete(int id)
        {
            UtakmicaTimLiga obj = _dbContext.UtakmicaTimLiga.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");

            obj.obrisan = true;
            _dbContext.Update(obj);

            _dbContext.SaveChanges();
            return Ok(obj);
        }
        //Update
        [HttpPost("/UtakmicaTimLiga/Update")]
        public ActionResult Update(int id, [FromBody] UtakmicaTimLigaAddVM x)
        {
            //if (!HttpContext.GetLoginInfo().isPermisijaStudentskaSluzba)
            //    return BadRequest("nije logiran");

            UtakmicaTimLiga obj;

            if (id == 0)
            {

                return BadRequest("pogresan ID");
            }
            else
            {
                obj = _dbContext.UtakmicaTimLiga.Where(p => p.UtakmicaTimLigaID == id).FirstOrDefault();
                // student = _dbContext.Student.Include(s => s.opstina_rodjenja.drzava).FirstOrDefault(s => s.id == id);
                if (obj == null)
                    return BadRequest("pogresan ID");
            }


            obj.UtakmicaID = x.UtakmicaID;
            obj.TimLigaID = x.TimLigaID;

            _dbContext.SaveChanges();
            return Ok(obj);
        }


        //povlacenje u utakmica detalji

        [HttpGet]
        public ActionResult GetById(int utakmicaID)
        {

            UtakmicaTimLiga s = _dbContext.UtakmicaTimLiga.Find(utakmicaID);

            List<UtakmicaTimLiga> utakmice = _dbContext.UtakmicaTimLiga
                .Include(x=> x.Utakmica.Status)
                .Include(x=>x.TimLiga.TimIgrac.Tim)
                //.Include(x=>x.TimLiga.Liga)
                .Include(x=>x.TimLiga.TimIgrac.Igrac)
                .Include(x=>x.TimLiga.TimIgrac.Igrac.Spol)
                .Include(x=>x.TimLiga.TimIgrac.Igrac.Pozicija)
                .Include(x=>x.TimLiga.TimIgrac.Igrac.Grad)
                .Include(x=>x.TimLiga.TimIgrac.Igrac.Grad.Kanton)
                .Include(x=> x.Utakmica.Kolo.Liga)
                .Include(x => x.Utakmica.Kolo)

                .Where(s => s.UtakmicaID == utakmicaID && s.obrisan == false).ToList();



            return Ok(utakmice);
        }

    };

      

    }
