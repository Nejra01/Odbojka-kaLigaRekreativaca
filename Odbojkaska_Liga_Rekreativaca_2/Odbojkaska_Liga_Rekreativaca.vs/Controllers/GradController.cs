using Microsoft.AspNetCore.Mvc;
using Odbojkaska_Liga_Rekreativaca.Core.Modeli;
using Odbojkaska_Liga_Rekreativaca.Repository;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Grad;
using System.Runtime.CompilerServices;

namespace Odbojkaska_Liga_Rekreativaca.vs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GradController : ControllerBase
    {
    private readonly AppDBContext _dbContext;


        public GradController(AppDBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        //dodavanje gradova

        [HttpPost("/Grad/Add")]
        public ActionResult Dodaj([FromBody] GradAddVM x)
        {
            var noviGrad = new Grad
            {
                ImeGrada = x.NazivGrada,
                KantonID = x.KantonID,
                obrisan = false

            };
            _dbContext.Add(noviGrad);
            _dbContext.SaveChanges();
            return Ok(noviGrad);
        }


        //prikaz gradova
        [HttpGet("/Grad/GetAll")]
        public List<GradoviGetAllVM> Get()
        {
            var upit = _dbContext.grad.OrderBy(p => p.KantonID).Where(z=>z.obrisan==false).Select(k=> new GradoviGetAllVM { 
                GradID = k.GradID,
                NazivKantona = k.Kanton.NazivKantona,
                KantonID = k.KantonID,
                NazivGrada = k.ImeGrada
                
            });
            return upit.ToList();
        }

        //brisanje 

        [HttpDelete("/Grad/DeletePermanent")]
        public ActionResult PermanentDelete(int id)
        {
            Grad obj = _dbContext.grad.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");

            _dbContext.Remove(obj);

            _dbContext.SaveChanges();
            return Ok(obj);
        }


        [HttpPost("/Grad/Delete")]
        public ActionResult Delete(int id)
        {
            Grad obj = _dbContext.grad.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");
            obj.obrisan = true;
            _dbContext.Update(obj);

            _dbContext.SaveChanges();
            return Ok(obj);
        }
        //update grad 

        [HttpPost("/Grad/Update")]
        public ActionResult Update(int id, [FromBody] GradUpdateVM x)
        {
            //if (!HttpContext.GetLoginInfo().isPermisijaStudentskaSluzba)
            //    return BadRequest("nije logiran");

            Grad obj;

            if (id == 0)
            {

                return BadRequest("pogresan ID");
            }
            else
            {
                obj = _dbContext.grad.Where(p => p.GradID == id).FirstOrDefault();
                    // student = _dbContext.Student.Include(s => s.opstina_rodjenja.drzava).FirstOrDefault(s => s.id == id);
                if (obj == null)
                    return BadRequest("pogresan ID");
            }

            obj.ImeGrada = x.NazivGrada;
            obj.KantonID = x.KantonID;
           

            _dbContext.SaveChanges();
            return Ok(obj);
        }

        [HttpGet]
        public ActionResult GetById(int kantonid)
        {
            List<Kanton> odabraniKanton = _dbContext.kanton
               .Where(x => x.KantonID == kantonid).ToList();
            Grad s = _dbContext.grad.Find(kantonid);

            List<Grad> gardovi = _dbContext.grad
           
                .Where(s => s.KantonID == kantonid && s.obrisan == false).ToList();



            return Ok(new { gardovi, odabraniKanton });
        }
    };
      
      

    }
