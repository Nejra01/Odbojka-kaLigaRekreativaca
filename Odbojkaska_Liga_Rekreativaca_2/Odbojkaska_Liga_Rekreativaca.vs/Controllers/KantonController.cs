using Microsoft.AspNetCore.Mvc;
using Odbojkaska_Liga_Rekreativaca.Core.Modeli;
using Odbojkaska_Liga_Rekreativaca.Repository;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Dvorana;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Kanton;
using System.Runtime.CompilerServices;

namespace Odbojkaska_Liga_Rekreativaca.vs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KantonController : ControllerBase
    {
    private readonly AppDBContext _dbContext;


        public KantonController(AppDBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        //dodavanje kantona

        [HttpPost("/Kanton/Add")]
        public ActionResult Dodaj([FromBody] KantonAddVM x)
        {
            var noviKanton = new Kanton
            {

                NazivKantona = x.NazivKantona,
                obrisan=false 
                
            };
            _dbContext.Add(noviKanton);
            _dbContext.SaveChanges();
            return Ok(noviKanton);
        }


        //prikaz kantona
        [HttpGet("/Update/Get")]
        public List<KantoniGetAllVM> Get()
        {
            var upit = _dbContext.kanton.OrderBy(p => p.KantonID).Where(z=>z.obrisan==false).Select(k=> new KantoniGetAllVM { 
                NazivKantona = k.NazivKantona,
                KantonID = k.KantonID,
                obrisan=k.obrisan
             
            });
            
            return upit.ToList();

        }

        //update

        [HttpPost("/Kanton/Update")]
        public ActionResult Update(int id, [FromBody] KantonAddVM x)
        {
            //if (!HttpContext.GetLoginInfo().isPermisijaStudentskaSluzba)
            //    return BadRequest("nije logiran");
            Kanton obj;
            if (id == 0)
            {
                return BadRequest("pogresan ID");
            }
            else
            {
                obj = _dbContext.kanton.Where(p => p.KantonID == id).FirstOrDefault();
                if (obj == null)
                    return BadRequest("pogresan ID");
            }
            obj.NazivKantona = x.NazivKantona;
            _dbContext.SaveChanges();
            return Ok(obj);
        }

        //brisanje  

        [HttpDelete("/Kanton/DeletePermanent")]
        public ActionResult DeletePermanent(int id)
        {
            Kanton obj = _dbContext.kanton.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");


            _dbContext.Remove(obj);

            _dbContext.SaveChanges();
            return Ok(obj);
        }

        [HttpPost("/Kanton/Delete")]
        public ActionResult Delete(int id)
        {
            Kanton obj = _dbContext.kanton.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");

            //_dbContext.Remove(obj);



            obj.obrisan = true;

            _dbContext.Update(obj);
            _dbContext.SaveChanges();
            return Ok(obj);
        }
    };

    }
