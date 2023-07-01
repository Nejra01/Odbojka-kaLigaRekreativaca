using Microsoft.AspNetCore.Mvc;
using Odbojkaska_Liga_Rekreativaca.Core.Modeli;
using Odbojkaska_Liga_Rekreativaca.Repository;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Dvorana;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Kanton;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Setovi;
using System.Runtime.CompilerServices;

namespace Odbojkaska_Liga_Rekreativaca.vs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SetoviController : ControllerBase
    {
    private readonly AppDBContext _dbContext;


        public SetoviController(AppDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost("/Setovi/Add")]

        public ActionResult Dodaj([FromBody] SetoviAddVM x)
        {
            var noviIgrac = new Setovi
            {

               BrojSeta=x.BrojSeta


            };
            _dbContext.Add(noviIgrac);
            _dbContext.SaveChanges();
            return Ok(noviIgrac);
        }
        //prikaz setova
        [HttpGet("/Setovi/GetAll")]
        public List<SetoviGetAllVM> Get()
        {
            var upit = _dbContext.setovi.OrderBy(p => p.SetoviID).Where(z => z.obrisan == false).Select(k=> new SetoviGetAllVM { 
                SetID = k.SetoviID,
                BrojSeta = k.BrojSeta
             
            });

            return upit.ToList();
        }
        //brisanje etova
        [HttpDelete("/Setovi/PermanentDelete")]
        public ActionResult PermanentDelete(int id)
        {
            Setovi obj = _dbContext.setovi.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");

            _dbContext.Remove(obj);

            _dbContext.SaveChanges();
            return Ok(obj);
        }

        [HttpPost("/Setovi/Delete")]
        public ActionResult Delete(int id)
        {
            Setovi obj = _dbContext.setovi.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");

            obj.obrisan = true;
            _dbContext.Update(obj);

            _dbContext.SaveChanges();
            return Ok(obj);
        }
        //update 
        [HttpPost("/Setovi/Update")]
        public ActionResult Update(int id, [FromBody] SetoviAddVM x)
        {
            //if (!HttpContext.GetLoginInfo().isPermisijaStudentskaSluzba)
            //    return BadRequest("nije logiran");

            Setovi obj;

            if (id == 0)
            {

                return BadRequest("pogresan ID");
            }
            else
            {
                obj = _dbContext.setovi.Where(p => p.SetoviID == id).FirstOrDefault();
                // student = _dbContext.Student.Include(s => s.opstina_rodjenja.drzava).FirstOrDefault(s => s.id == id);
                if (obj == null)
                    return BadRequest("pogresan ID");
            }

            obj.BrojSeta = x.BrojSeta;

            _dbContext.SaveChanges();
            return Ok(obj);
        }
    };

      

    }
