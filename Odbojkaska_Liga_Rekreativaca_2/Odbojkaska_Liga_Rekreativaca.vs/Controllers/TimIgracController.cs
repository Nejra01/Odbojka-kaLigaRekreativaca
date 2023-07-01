using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Odbojkaska_Liga_Rekreativaca.Core.Modeli;
using Odbojkaska_Liga_Rekreativaca.Repository;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Dvorana;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Kanton;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.TimIgrac;
using System.Runtime.CompilerServices;

namespace Odbojkaska_Liga_Rekreativaca.vs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimIgracController : ControllerBase
    {
    private readonly AppDBContext _dbContext;


        public TimIgracController(AppDBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        //dodavanje dvorane

        [HttpPost("/TimIgrac/Add")]
        public ActionResult Dodaj([FromBody] TimIgracAddVM x)
        {
            var novaDvorana = new TimIgrac
            {
                TimID=x.TimID,
                obrisan=false,
                IgracID=x.IgracID


            };
            _dbContext.Add(novaDvorana);
            _dbContext.SaveChanges();
            return Ok(novaDvorana);
        }


        //prikaz dvorana
        [HttpGet("/TimIgrac/GetAll")]
        public List<TimIgracGetAllVM> Get()
        {
            var upit = _dbContext.timIgrac.OrderBy(p => p.TimIgracID).Where(z=>z.obrisan==false).Select(x=> new TimIgracGetAllVM
            {
                TimIgracID=x.TimIgracID,
                TimID = x.TimID,
                TimOpis=x.Tim.ImeTima,
                IgracID = x.IgracID, 
                IgracOpis=x.Igrac.Ime+" "+x.Igrac.Prezime
            });
            return upit.ToList();
        }
        //deleet 
        [HttpDelete("/TimIgrac/DeletePermanent")]
        public ActionResult DeletePermanent(int id)
        {
            TimIgrac obj = _dbContext.timIgrac.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");

            _dbContext.Remove(obj);

            _dbContext.SaveChanges();
            return Ok(obj);
        }

        [HttpPost("/TimIgrac/Delete")]
        public ActionResult Delete(int id)
        {
            TimIgrac obj = _dbContext.timIgrac.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");

            obj.obrisan = true;
            _dbContext.Update(obj);

            _dbContext.SaveChanges();
            return Ok(obj);
        }
        //Update
        [HttpPost("/TimIgrac/Update")]
        public ActionResult Update(int id, [FromBody] TimIgracAddVM x)
        {
            //if (!HttpContext.GetLoginInfo().isPermisijaStudentskaSluzba)
            //    return BadRequest("nije logiran");

            TimIgrac obj;

            if (id == 0)
            {

                return BadRequest("pogresan ID");
            }
            else
            {
                obj = _dbContext.timIgrac.Where(p => p.TimIgracID == id).FirstOrDefault();
                // student = _dbContext.Student.Include(s => s.opstina_rodjenja.drzava).FirstOrDefault(s => s.id == id);
                if (obj == null)
                    return BadRequest("pogresan ID");
            }

            obj.TimID = x.TimID;
            obj.IgracID = x.IgracID;

            _dbContext.SaveChanges();
            return Ok(obj);
        }



        //za get igraca

        //[HttpGet]
        //public ActionResult GetById(int timigracID)
        //{
        //    TimIgrac s = _dbContext.timIgrac.Find(timigracID);

        //    List<TimIgrac> kola = _dbContext.timIgrac
        //        .Include(a => a.Tim)
        //        .Include(a => a.Igrac)
        //        .Include(a => a.Igrac.Spol)
        //        .Include(a => a.Igrac.Pozicija)
        //        .Include(a => a.Igrac.Grad)
        //        .Include(a => a.Igrac.Grad.Kanton)

        //        .Where(s => s.TimIgracID == timigracID && s.obrisan == false).ToList();



        //    return Ok(kola);
        //}

        [HttpGet]
        public ActionResult GetById(int timid)
        {
            List<Tim> odabranoKolo = _dbContext.tim.Where(x => x.TimID == timid)
                .ToList();
            TimIgrac s = _dbContext.timIgrac.Find(timid);

            List<TimIgrac> gardovi = _dbContext.timIgrac
                // .Include(x => x.Tim)
                 .Include(x => x.Igrac)
                 .Include(x => x.Igrac.Grad)
                 .Include(x => x.Igrac.Grad.Kanton)
                 .Include(x => x.Igrac.Spol)
                 .Include(x => x.Igrac.Pozicija)


                .Where(s => s.TimID == timid && s.obrisan == false).ToList();



            return Ok(new { gardovi, odabranoKolo });
        }
    };

      

    }
