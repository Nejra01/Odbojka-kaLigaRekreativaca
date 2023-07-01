using Microsoft.AspNetCore.Mvc;
using Odbojkaska_Liga_Rekreativaca.Core.Modeli;
using Odbojkaska_Liga_Rekreativaca.Repository;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Dvorana;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Kanton;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Spol;
using System.Runtime.CompilerServices;

namespace Odbojkaska_Liga_Rekreativaca.vs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpolController : ControllerBase
    {
    private readonly AppDBContext _dbContext;


        public SpolController(AppDBContext dbContext)
        {
            this._dbContext = dbContext;
        }


        [HttpPost("/Spol/Add")]

        public ActionResult Dodaj([FromBody] SpolAddVM x)
        {
            var noviIgrac = new Spol
            {

                NazivSpola = x.NazivSpola


            };
            _dbContext.Add(noviIgrac);
            _dbContext.SaveChanges();
            return Ok(noviIgrac);
        }


        //prikaz spolova
        [HttpGet("/Spol/Get")]
        public List<SpolGetAllVM> Get()
        {
            var upit = _dbContext.spol.OrderBy(p => p.NazivSpola).Where(z=>z.obrisan==false).Select(k=> new SpolGetAllVM { 
                NazivSpola = k.NazivSpola,
                SpolID = k.SpolID
            });
            return upit.ToList();
        }

        //update

        [HttpPost("/Spol/Update")]
        public ActionResult Update(int id, [FromBody] SpolAddVM x)
        {
            //if (!HttpContext.GetLoginInfo().isPermisijaStudentskaSluzba)
            //    return BadRequest("nije logiran");
            Spol obj;
            if (id == 0)
            {
                return BadRequest("pogresan ID");
            }
            else
            {
                obj = _dbContext.spol.Where(p => p.SpolID == id).FirstOrDefault();
                if (obj == null)
                    return BadRequest("pogresan ID");
            }
            obj.NazivSpola = x.NazivSpola;
            _dbContext.SaveChanges();
            return Ok(obj);
        }

        //brisanje  

        [HttpDelete("/Spol/PermanentDelete")]
        public ActionResult PermanentDelete(int id)
        {
            Spol obj = _dbContext.spol.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");

            _dbContext.Remove(obj);

            _dbContext.SaveChanges();
            return Ok(obj);
        }
        [HttpPost("/Spol/Delete")]
        public ActionResult Delete(int id)
        {
            Spol obj = _dbContext.spol.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");

            obj.obrisan = true;
            _dbContext.Update(obj);

            _dbContext.SaveChanges();
            return Ok(obj);
        }
    };

      

    }
