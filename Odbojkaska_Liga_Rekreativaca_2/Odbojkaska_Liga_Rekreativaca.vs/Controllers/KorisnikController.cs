using Microsoft.AspNetCore.Mvc;
using Odbojkaska_Liga_Rekreativaca.Core.Modeli;
using Odbojkaska_Liga_Rekreativaca.Repository;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Dvorana;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Igrac;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Kanton;
using Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Korisnik;
using System.Runtime.CompilerServices;

namespace Odbojkaska_Liga_Rekreativaca.vs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KorisnikController : ControllerBase
    {
    private readonly AppDBContext _dbContext;


        public KorisnikController(AppDBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        //dodavanje dvorane

        [HttpPost("/Korisnik/Add")]
        public ActionResult Dodaj([FromBody] KorisnikAddVM x)
        {
            var noviKorisnik = new Korisnik
            {


                Ime = x.Ime,
                Prezime = x.Prezime,
                DatumRodjenja = x.DatumRodjenja,
                BrojTelefona = x.BrojTelefona,
                Email = x.Email,
                SpolID = x.SpolID,
                GradID = x.GradID,
             //   UlogaID=x.UlogaID,
                obrisan = false,
                korisnickoIme = x.Username,
                lozinka = x.Password,
                isZapisnicar=x.isZapisnicar,
                isSudija=x.isSudija,
                isAdmin = x.isAdmin



            };
            _dbContext.Add(noviKorisnik);
            _dbContext.SaveChanges();
            return Ok(noviKorisnik);
        }


        //prikaz dvorana
        [HttpGet("/Korisnik/GetAll")]
        public List<KorisnikGetAllVM> Get()
        {
            var upit = _dbContext.korisnik.OrderBy(p => p.KorisnikID).Where(z => z.obrisan == false).Select(x => new KorisnikGetAllVM
            {
                KorisnikID = x.KorisnikID,  
                Ime = x.Ime,
                Prezime = x.Prezime,
                DatumRodjenja = x.DatumRodjenja,
                BrojTelefona = x.BrojTelefona,
                Email = x.Email,
                SpolID = x.SpolID,
                SpolOpis=x.Spol.NazivSpola,
                GradID = x.GradID,
                GradOpis=x.Grad.ImeGrada,
                //UlogaID = x.UlogaID, 
                UlogaOpis=x.isAdmin?"Administrator":(x.isSudija?"Sudija":"Zapisnicar"),
                Username = x.korisnickoIme,
                Password = x.lozinka,
                isZapisnicar = x.isZapisnicar,
                isSudija =x.isSudija,
                isAdmin=x.isAdmin,
                
           
              

            });
            return upit.ToList();
        }
        //brisanje korinsika 
        [HttpPost("/Korisnik/Delete")]
        public ActionResult Delete(int id)
        {
            Korisnik obj = _dbContext.korisnik.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");

            obj.obrisan = true;
            _dbContext.Update(obj);

            _dbContext.SaveChanges();
            return Ok(obj);
        }
        [HttpDelete("/Korisnik/PermanentDelete")]
        public ActionResult PermanentDelete(int id)
        {
            Korisnik obj = _dbContext.korisnik.Find(id);

            if (obj == null)
                return BadRequest("pogresan ID");

            _dbContext.Remove(obj);
            _dbContext.SaveChanges();
            return Ok(obj);
        }
        //update 
        [HttpPost("/Korisnik/Update")]
        public ActionResult Update(int id, [FromBody] KorisnikAddVM x)
        {
            //if (!HttpContext.GetLoginInfo().isPermisijaStudentskaSluzba)
            //    return BadRequest("nije logiran");

            Korisnik obj;

            if (id == 0)
            {

                return BadRequest("pogresan ID");
            }
            else
            {
                obj = _dbContext.korisnik.Where(p => p.KorisnikID == id).FirstOrDefault();
                // student = _dbContext.Student.Include(s => s.opstina_rodjenja.drzava).FirstOrDefault(s => s.id == id);
                if (obj == null)
                    return BadRequest("pogresan ID");
            }

            obj.Ime = x.Ime;
            obj.Prezime = x.Prezime;
            obj.DatumRodjenja = x.DatumRodjenja;
            obj.BrojTelefona = x.BrojTelefona;
            obj.Email = x.Email;
            obj.SpolID = x.SpolID;
            obj.GradID = x.GradID;
            //obj.UlogaID = x.UlogaID;
            obj.isSudija = x.isSudija;
            obj.isZapisnicar = x.isZapisnicar;
            obj.isAdmin = x.isAdmin;
            obj.korisnickoIme = x.Username;
            obj.lozinka = x.Password;

            _dbContext.SaveChanges();
            return Ok(obj);
        }
    };

      

    }
