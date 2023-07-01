using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Odbojkaska_Liga_Rekreativaca.Core.Modeli;
using Odbojkaska_Liga_Rekreativaca.Repository;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Dvorana;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Kanton;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Kolo;
using System.Runtime.CompilerServices;

namespace Odbojkaska_Liga_Rekreativaca.vs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KoloController : ControllerBase
    {
    private readonly AppDBContext _dbContext;


        public KoloController(AppDBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        //dodavanje dvorane

        [HttpPost("/Kolo/Add")]
        public ActionResult Dodaj([FromBody] KoloAddVM x)
        {
            var novoKolo = new Kolo
            {
                BrojKola=x.BrojKola,
                LigaID=x.LigaID,
                obrisan = false



            };
            _dbContext.Add(novoKolo);
            _dbContext.SaveChanges();
            return Ok(novoKolo);
        }


        //prikaz 
        [HttpGet("/Kolo/GetAll")]
        public List<KoloGetAllVM> Get()
        {
            var upit = _dbContext.kolo.OrderBy(p => p.KoloID).Where(z=>z.obrisan==false).Select(k=> new KoloGetAllVM
            {   KoloID=k.KoloID,
                BrojKola = k.BrojKola,
                LigaID = k.LigaID,
                LigaOpis=k.Liga.GodinaLige

            });
            return upit.ToList();
        }
        //brisanje  kola
        [HttpDelete("/Kolo/DeletePermanent")]
        public ActionResult DeletePermanent(int id)
        {
            Kolo obj = _dbContext.kolo.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");

            _dbContext.Remove(obj);

            _dbContext.SaveChanges();
            return Ok(obj);
        }
        [HttpPost("/Kolo/Delete")]
        public ActionResult Delete(int id)
        {
            Kolo obj = _dbContext.kolo.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");
            obj.obrisan = true;
            _dbContext.Update(obj);
            _dbContext.SaveChanges();


            return Ok(obj);
        }
        //update 
        [HttpPost("/Kolo/Update")]
        public ActionResult Update(int id, [FromBody] KoloAddVM x)
        {
            //if (!HttpContext.GetLoginInfo().isPermisijaStudentskaSluzba)
            //    return BadRequest("nije logiran");

            Kolo obj;

            if (id == 0)
            {

                return BadRequest("pogresan ID");
            }
            else
            {
                obj = _dbContext.kolo.Where(p => p.KoloID == id).FirstOrDefault();
                // student = _dbContext.Student.Include(s => s.opstina_rodjenja.drzava).FirstOrDefault(s => s.id == id);
                if (obj == null)
                    return BadRequest("pogresan ID");
            }

            obj.BrojKola = x.BrojKola;
            obj.LigaID = x.LigaID;

            _dbContext.SaveChanges();
            return Ok(obj);
        }



        //Izlistavanje u Ligi

        //[HttpGet("/Kolo/GetByLigaId")]
        //public ActionResult GetById(int ligaid)
        //{



        //    var povratnavr = _dbContext.kolo.Where(s => s.LigaID == ligaid && s.obrisan==false)

        //        .Select(s => new
        //        {
        //            brojKola = s.BrojKola,
        //            ligaID = s.LigaID,
        //            KoloID = s.KoloID,
        //            nazivLige = s.Liga.GodinaLige
        //        })
        //        .ToList();


        //    return Ok(povratnavr);
        //}



        [HttpGet]
        public ActionResult GetById(int ligaID)
        {

            List<Liga> s = _dbContext.liga.Where(x=>x.LigaID==ligaID).ToList();

            List<Kolo> kola = _dbContext.kolo
           // .Include(x=>x.Liga)
                .Where(s => s.LigaID == ligaID && s.obrisan==false).ToList();

           

            return Ok(new { kola, s });
        }

    };

      

    }
