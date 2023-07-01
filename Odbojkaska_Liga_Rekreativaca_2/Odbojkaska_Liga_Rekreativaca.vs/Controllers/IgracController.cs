using Microsoft.AspNetCore.Mvc;
using Odbojkaska_Liga_Rekreativaca.Core.Modeli;
using Odbojkaska_Liga_Rekreativaca.Repository;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Dvorana;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Igrac;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Kanton;
using System.Runtime.CompilerServices;

namespace Odbojkaska_Liga_Rekreativaca.vs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IgracController : ControllerBase
    {
    private readonly AppDBContext _dbContext;


        public IgracController(AppDBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        //dodavanje dvorane

        [HttpPost("/Igrac/Add")]
        public ActionResult Dodaj([FromBody] IgracAddVM x)
        {
            var noviIgrac = new Igrac
            {

                Ime = x.Ime,
                Prezime = x.Prezime,
                DatumRodjenja = x.DatumRodjenja,
                PozicijaID = x.PozicijaID,
                BrojTelefona = x.BrojTelefona,
                EmailAdresa = x.EmailAdresa,
                SpolID = x.SpolID,
                GradID = x.GradID,
                obrisan = false



            };
            _dbContext.Add(noviIgrac);
            _dbContext.SaveChanges();
            return Ok(noviIgrac);
        }


        //prikaz dvorana
        [HttpGet("/Igrac/GetAll")]
        public List<IgracGetAllVM> Get()
        {

            var upit = _dbContext.igrac.OrderBy(p =>p.IgraciD).Where(z=>z.obrisan==false).Select(x=> new IgracGetAllVM {
            

                IgracID = x.IgraciD,
                Ime = x.Ime,
                Prezime = x.Prezime,
                DatumRodjenja = x.DatumRodjenja,
                BrojTelefona = x.BrojTelefona,
                EmailAdresa = x.EmailAdresa,
                SpolOpis=x.Spol.NazivSpola,
                SpolID = x.SpolID,
                GradOpis=x.Grad.ImeGrada,
                GradID = x.GradID, 
                PozicijaOpis    =x.Pozicija.NazivPozicije,
                PozicijaID = x.PozicijaID,

            });
            return upit.ToList();
        }

        //brisanje igraca 
        [HttpPost("/Igrac/Delete")]
        public ActionResult Delete(int id)
        {
            Igrac obj = _dbContext.igrac.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");



            obj.obrisan = true;
            _dbContext.Update(obj);
            _dbContext.SaveChanges();
            return Ok(obj);
        }
        [HttpDelete("/Igrac/DeletePermanent")]
        public ActionResult DeletePermanent(int id)
        {
            Igrac obj = _dbContext.igrac.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");



            _dbContext.Remove(obj);
            _dbContext.SaveChanges();
            return Ok(obj);
        }
        //update 
        [HttpPost("/Igrac/Update")]
        public ActionResult Update(int id, [FromBody] IgracAddVM x)
        {
            //if (!HttpContext.GetLoginInfo().isPermisijaStudentskaSluzba)
            //    return BadRequest("nije logiran");

            Igrac obj;

            if (id == 0)
            {

                return BadRequest("pogresan ID");
            }
            else
            {
                obj = _dbContext.igrac.Where(p => p.IgraciD == id).FirstOrDefault();
                // student = _dbContext.Student.Include(s => s.opstina_rodjenja.drzava).FirstOrDefault(s => s.id == id);
                if (obj == null)
                    return BadRequest("pogresan ID");
            }

                obj.Ime = x.Ime;
                obj.Prezime = x.Prezime;
                obj.DatumRodjenja = x.DatumRodjenja;
                obj.PozicijaID = x.PozicijaID;
                obj.BrojTelefona = x.BrojTelefona;
                obj.EmailAdresa = x.EmailAdresa;
                obj.SpolID = x.SpolID;
                obj.GradID = x.GradID;

            _dbContext.SaveChanges();
            return Ok(obj);
        }
    };

      

    }
