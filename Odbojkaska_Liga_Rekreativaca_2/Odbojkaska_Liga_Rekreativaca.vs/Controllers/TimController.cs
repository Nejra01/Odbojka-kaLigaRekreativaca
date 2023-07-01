using Microsoft.AspNetCore.Mvc;
using Odbojkaska_Liga_Rekreativaca.Core.Modeli;
using Odbojkaska_Liga_Rekreativaca.Repository;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Kanton;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Tim;
using System.Runtime.CompilerServices;

namespace Odbojkaska_Liga_Rekreativaca.vs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimController : ControllerBase
    {
    private readonly AppDBContext _dbContext;


        public TimController(AppDBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        //dodavanje tima

        [HttpPost("/Tim/Add")]
        public ActionResult Dodaj([FromBody] TimAddVM x)
        {
            var noviTim = new Tim
            {

                ImeTima = x.ImeTima
                
                
            };
            _dbContext.Add(noviTim);
            _dbContext.SaveChanges();
            return Ok(noviTim);
        }


        //prikaz tima
        [HttpGet("/Tim/GetAll")]
        public List<TimGetAllVM> Get()
        {
            var upit = _dbContext.tim.OrderBy(p => p.TimID).Where(z => z.obrisan == false).Select(k=> new TimGetAllVM { 
                ImeTima = k.ImeTima,
                TimID = k.TimID
            });
            return upit.ToList();
        }
        //delete 
        [HttpDelete("/Tim/PermanentDelete")]
        public ActionResult PermanentDelete(int id)
        {
            Tim obj = _dbContext.tim.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");

            _dbContext.Remove(obj);

            _dbContext.SaveChanges();
            return Ok(obj);
        }

        [HttpPost("/Tim/Delete")]
        public ActionResult Delete(int id)
        {
            Tim obj = _dbContext.tim.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");

            obj.obrisan = true;
            _dbContext.Update(obj);

            _dbContext.SaveChanges();
            return Ok(obj);
        }
        //update 
        [HttpPost("/Tim/Update")]
        public ActionResult Update(int id, [FromBody] TimAddVM x)
        {
            //if (!HttpContext.GetLoginInfo().isPermisijaStudentskaSluzba)
            //    return BadRequest("nije logiran");

            Tim obj;

            if (id == 0)
            {

                return BadRequest("pogresan ID");
            }
            else
            {
                obj = _dbContext.tim.Where(p => p.TimID == id).FirstOrDefault();
                // student = _dbContext.Student.Include(s => s.opstina_rodjenja.drzava).FirstOrDefault(s => s.id == id);
                if (obj == null)
                    return BadRequest("pogresan ID");
            }

            obj.ImeTima = x.ImeTima;

            _dbContext.SaveChanges();
            return Ok(obj);
        }
    };

      

    }
