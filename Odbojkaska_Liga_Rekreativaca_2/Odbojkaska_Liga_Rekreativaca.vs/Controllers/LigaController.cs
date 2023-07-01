using FIT_Api_Examples.Helper.AutentifikacijaAutorizacija;
using Microsoft.AspNetCore.Mvc;
using Odbojkaska_Liga_Rekreativaca.Core.Modeli;
using Odbojkaska_Liga_Rekreativaca.Repository;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Kanton;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Liga;
using System.Runtime.CompilerServices;

namespace Odbojkaska_Liga_Rekreativaca.vs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LigaController : ControllerBase
    {
    private readonly AppDBContext _dbContext;


        public LigaController(AppDBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        //dodavanje kantona

        [HttpPost("/Liga/Add")]
        public ActionResult Dodaj([FromBody] LigaAddVM x)
        {
            var novaLiga = new Liga
            {

                GodinaLige = x.GodinaLige
            };
            _dbContext.Add(novaLiga);
            _dbContext.SaveChanges();
            return Ok(novaLiga);
        }


        //prikaz kantona
        [HttpGet("/Liga/GetAll")]
       
        public List<LigaGetAllVM> Get()
        {
            var upit = _dbContext.liga.OrderBy(p => p.LigaID).Where(z => z.obrisan == false).Select(k=> new LigaGetAllVM { 
                GodinaLige = k.GodinaLige,
                LigaID = k.LigaID
            });
            return upit.ToList();
        }
        //brisanje lige 
        [HttpDelete("/Liga/PermanentDelete")]
        public ActionResult PermanentDelete(int id)
        {
            Liga obj = _dbContext.liga.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");

            _dbContext.Remove(obj);

            _dbContext.SaveChanges();
            return Ok(obj);
        }


        [HttpPost("/Liga/Delete")]
        public ActionResult Delete(int id)
        {
            Liga obj = _dbContext.liga.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");
            obj.obrisan = true;
            _dbContext.Update(obj);

            _dbContext.SaveChanges();

            return Ok(obj);
        }

        //update 

        [HttpPost("/Liga/Update")]
        public ActionResult Update(int id, [FromBody] LigaAddVM x)
        {
            //if (!HttpContext.GetLoginInfo().isPermisijaStudentskaSluzba)
            //    return BadRequest("nije logiran");

            Liga obj;

            if (id == 0)
            {

                return BadRequest("pogresan ID");
            }
            else
            {
                obj = _dbContext.liga.Where(p => p.LigaID == id).FirstOrDefault();
                // student = _dbContext.Student.Include(s => s.opstina_rodjenja.drzava).FirstOrDefault(s => s.id == id);
                if (obj == null)
                    return BadRequest("pogresan ID");
            }

            obj.GodinaLige = x.GodinaLige;

            _dbContext.SaveChanges();
            return Ok(obj);
        }
    };

      

    }
