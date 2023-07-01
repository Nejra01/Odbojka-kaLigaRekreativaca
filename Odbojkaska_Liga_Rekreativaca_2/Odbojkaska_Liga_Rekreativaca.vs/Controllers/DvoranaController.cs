using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Odbojkaska_Liga_Rekreativaca.Core.Modeli;
using Odbojkaska_Liga_Rekreativaca.Repository;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Dvorana;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Kanton;
using System.Runtime.CompilerServices;

namespace Odbojkaska_Liga_Rekreativaca.vs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DvoranaController : ControllerBase
    {
    private readonly AppDBContext _dbContext;


        public DvoranaController(AppDBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        //dodavanje dvorane

        [HttpPost("/Dvorana/Add")]
        public ActionResult Dodaj([FromBody] DvoranaAddVM x)
        {
            var novaDvorana = new Dvorana
            {

                ImeDvorane = x.ImeDvorane,
                Adresa = x.Adresa,
                GradID = x.GradID,
                obrisan = false


            };
            _dbContext.Add(novaDvorana);
            _dbContext.SaveChanges();
            return Ok(novaDvorana);
        }


        //prikaz dvorana
        [HttpGet("/Dvorana/Get")]
        public List<DvoranaGetAllVM> Get()
        {
            var upit = _dbContext.dvorana.OrderBy(p => p.DvoranaID).Where(z=>z.obrisan==false).Select(k=> new DvoranaGetAllVM { 
                DvoranaID = k.DvoranaID,
                ImeDvorane = k.ImeDvorane,
                Adresa = k.Adresa,
                Grad = k.Grad.ImeGrada,
                GradID=k.GradID
            });
            return upit.ToList();
        }

        //update

        [HttpPost("/Dvorana/Update")]
        public ActionResult Update(int id, [FromBody] DvoranaUpdateVM x)
        {
            //if (!HttpContext.GetLoginInfo().isPermisijaStudentskaSluzba)
            //    return BadRequest("nije logiran");
            Dvorana obj;
            if (id == 0)
            {
                return BadRequest("pogresan ID");
            }
            else
            {
                obj = _dbContext.dvorana.Where(p => p.DvoranaID == id).FirstOrDefault();
                if (obj == null)
                    return BadRequest("pogresan ID");
            }
            obj.ImeDvorane = x.ImeDvorane;
            obj.Adresa = x.Adresa;
                obj.GradID = x.GradID;
            _dbContext.SaveChanges();
            return Ok(obj);
        }


        //brisanje  

        [HttpDelete("/Dvorana/PermanentDelete")]
        public ActionResult PermanentDelete(int id)
        {
            Dvorana obj = _dbContext.dvorana.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");

            _dbContext.Remove(obj);

            _dbContext.SaveChanges();
            return Ok(obj);
        }

        [HttpPost("/Dvorana/Delete")]
        public ActionResult Delete(int id)
        {
            Dvorana obj = _dbContext.dvorana.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");
            obj.obrisan = true;
            _dbContext.Update(obj);

            _dbContext.SaveChanges();
            return Ok(obj);
        }
        [HttpGet]

        public ActionResult GetById(int gradid)
        {
            List<Grad> odabraniKanton = _dbContext.grad
              .Where(x => x.GradID == gradid).Include(a=>a.Kanton).ToList();
            Dvorana s = _dbContext.dvorana.Find(gradid);

            List<Dvorana> gardovi = _dbContext.dvorana
                 .Include(x => x.Grad.Kanton)
                .Where(s => s.GradID == gradid && s.obrisan == false).ToList();



            return Ok(new { gardovi, odabraniKanton });
        }
    };

      

    }
