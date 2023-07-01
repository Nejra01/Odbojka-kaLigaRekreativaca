using Microsoft.AspNetCore.Mvc;
using Odbojkaska_Liga_Rekreativaca.Core.Modeli;
using Odbojkaska_Liga_Rekreativaca.Repository;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Dvorana;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Kanton;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Pozicije;
using System.Runtime.CompilerServices;

namespace Odbojkaska_Liga_Rekreativaca.vs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PozicijaController : ControllerBase
    {
    private readonly AppDBContext _dbContext;


        public PozicijaController(AppDBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        //dodavanje pozicije

        [HttpPost("/Pozicija/Add")]
        public ActionResult Dodaj([FromBody] PozicijeAddVM x)
        {
            var novaPozicija = new Pozicija
            {

                NazivPozicije = x.NazivPozicije
            };
            _dbContext.Add(novaPozicija);
            _dbContext.SaveChanges();
            return Ok(novaPozicija);
        }


        //prikaz pozicije
        [HttpGet("/Pozicija/Get")]
        public List<PozicijeGetAllVM> Get()
        {
            var upit = _dbContext.pozicija.OrderBy(p => p.PozicijaID).Where(z => z.obrisan == false).Select(k=> new PozicijeGetAllVM { 
                NazivPozicije = k.NazivPozicije,
                PozicijaID = k.PozicijaID
            });
            return upit.ToList();
        }


        //update

        [HttpPost("/Pozicija/Update")]
        public ActionResult Update(int id, [FromBody] PozicijeAddVM x)
        {
            //if (!HttpContext.GetLoginInfo().isPermisijaStudentskaSluzba)
            //    return BadRequest("nije logiran");
            Pozicija obj;
            if (id == 0)
            {
                return BadRequest("pogresan ID");
            }
            else
            {
                obj = _dbContext.pozicija.Where(p => p.PozicijaID == id).FirstOrDefault();
                if (obj == null)
                    return BadRequest("pogresan ID");
            }
            obj.NazivPozicije = x.NazivPozicije;
            _dbContext.SaveChanges();
            return Ok(obj);
        }

        //brisanje  

        [HttpDelete("/Pozicija/PermanentDelete")]
        public ActionResult PermanentDelete(int id)
        {
            Pozicija obj = _dbContext.pozicija.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");

            _dbContext.Remove(obj);

            _dbContext.SaveChanges();
            return Ok(obj);
        }

        [HttpPost("/Pozicija/Delete")]
        public ActionResult Delete(int id)
        {
            Pozicija obj = _dbContext.pozicija.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");

            obj.obrisan = true;
            _dbContext.Update(obj);

            _dbContext.SaveChanges();
            return Ok(obj);
        }

    };

      

    }
