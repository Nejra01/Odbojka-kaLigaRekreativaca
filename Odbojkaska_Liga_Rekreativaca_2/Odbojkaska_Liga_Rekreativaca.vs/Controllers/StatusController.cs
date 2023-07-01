using Microsoft.AspNetCore.Mvc;
using Odbojkaska_Liga_Rekreativaca.Core.Modeli;
using Odbojkaska_Liga_Rekreativaca.Repository;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Dvorana;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Kanton;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Status;
using System.Runtime.CompilerServices;

namespace Odbojkaska_Liga_Rekreativaca.vs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusController : ControllerBase
    {
    private readonly AppDBContext _dbContext;


        public StatusController(AppDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost("/Status/Add")]
        public ActionResult Dodaj([FromBody] StatusAddVM x)
        {
            var noviIgrac = new Status
            {

               NazivStatusa=x.NazivStatusa


            };
            _dbContext.Add(noviIgrac);
            _dbContext.SaveChanges();
            return Ok(noviIgrac);
        }

        //prikaz statusa
        [HttpGet("/Status/Get")]
        public List<StatusGetAllVM> Get()
        {
            var upit = _dbContext.status.OrderBy(p => p.StatusID).Where(z => z.obrisan == false).Select(k=> new StatusGetAllVM
            {
                StatusID=k.StatusID,
                NazivStatusa = k.NazivStatusa

               
            });
            return upit.ToList();
        }

        //update

        [HttpPost("/Status/Update")]
        public ActionResult Update(int id, [FromBody] StatusAddVM x)
        {
            //if (!HttpContext.GetLoginInfo().isPermisijaStudentskaSluzba)
            //    return BadRequest("nije logiran");
            Status obj;
            if (id == 0)
            {
                return BadRequest("pogresan ID");
            }
            else
            {
                obj = _dbContext.status.Where(p => p.StatusID == id).FirstOrDefault();
                if (obj == null)
                    return BadRequest("pogresan ID");
            }
            obj.NazivStatusa = x.NazivStatusa;
            _dbContext.SaveChanges();
            return Ok(obj);
        }

        //brisanje  

        [HttpDelete("/Status/PermanentDelete")]
        public ActionResult PermanentDelete(int id)
        {
            Status obj = _dbContext.status.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");

            _dbContext.Remove(obj);

            _dbContext.SaveChanges();
            return Ok(obj);
        }


        [HttpPost("/Status/Delete")]
        public ActionResult Delete(int id)
        {
            Status obj = _dbContext.status.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");

            obj.obrisan = true;
            _dbContext.Update(obj);

            _dbContext.SaveChanges();
            return Ok(obj);
        }
    };

      

    }
