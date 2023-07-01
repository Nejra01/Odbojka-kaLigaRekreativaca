using Microsoft.AspNetCore.Mvc;
using Odbojkaska_Liga_Rekreativaca.Core.Modeli;
using Odbojkaska_Liga_Rekreativaca.Repository;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Kanton;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Uloge;
using System.Runtime.CompilerServices;

namespace Odbojkaska_Liga_Rekreativaca.vs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UlogeController : ControllerBase
    {
        private readonly AppDBContext _dbContext;


        public UlogeController(AppDBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        //dodavanje uloga

        [HttpPost("/Uloge/Add")]
        public ActionResult Dodaj([FromBody] UlogeAddVM x)
        {
            var novaUloga = new Uloga
            {

                NazivUloge = x.NazivUloge
            };
            _dbContext.Add(novaUloga);
            _dbContext.SaveChanges();
            return Ok(novaUloga);
        }


        //prikaz uloga
        [HttpGet("/Uloge/GetAll")]
        public List<UlogeGetAllVM> Get()
        {
            var upit = _dbContext.uloga.OrderBy(p => p.UlogaID).Where(z => z.obrisan == false).Select(k => new UlogeGetAllVM
            {
                UlogaID = k.UlogaID,
                NazivUloge = k.NazivUloge
            });
            return upit.ToList();
        }
        //deleete
        [HttpDelete("/Uloge/PermanentDelete")]
        public ActionResult PermanentDelete(int id)
        {
            Uloga obj = _dbContext.uloga.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");

            _dbContext.Remove(obj);

            _dbContext.SaveChanges();
            return Ok(obj);
        }
        //deleete
        [HttpPost("/Uloge/Delete")]
        public ActionResult Delete(int id)
        {
            Uloga obj = _dbContext.uloga.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");
            obj.obrisan = true;
            _dbContext.Update(obj);

            _dbContext.SaveChanges();
            return Ok(obj);
        }
        //Update
        [HttpPost("/Uloge/Update")]
        public ActionResult Update(int id, [FromBody] UlogeAddVM x)
        {
            //if (!HttpContext.GetLoginInfo().isPermisijaStudentskaSluzba)
            //    return BadRequest("nije logiran");

            Uloga obj;

            if (id == 0)
            {

                return BadRequest("pogresan ID");
            }
            else
            {
                obj = _dbContext.uloga.Where(p => p.UlogaID == id).FirstOrDefault();
                // student = _dbContext.Student.Include(s => s.opstina_rodjenja.drzava).FirstOrDefault(s => s.id == id);
                if (obj == null)
                    return BadRequest("pogresan ID");
            }

            obj.NazivUloge = x.NazivUloge;

            _dbContext.SaveChanges();
            return Ok(obj);
        }
    };



}
