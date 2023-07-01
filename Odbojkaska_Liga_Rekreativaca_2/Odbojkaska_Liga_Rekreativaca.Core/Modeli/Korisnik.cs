using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Odbojkaska_Liga_Rekreativaca.Core.Modeli
{
    public class Korisnik
    {
        public int KorisnikID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string BrojTelefona { get; set; }
        public string Email { get; set; }
        public string korisnickoIme { get; set; }
        [JsonIgnore]
        public string lozinka { get; set; }

        // public string Username { get; set; }
        // public string Password { get; set; }
        public bool isAdmin { get; set; }
        //[ForeignKey("UlogaID")]
        //public Uloga Uloga { get; set; }
        //public int UlogaID { get; set; }


        // [JsonIgnore]
        //public Korisnik korisnik => this as Korisnik;
        //public bool isSudija => korisnik != null;
        //public bool isZapisnicar => korisnik != null;
        //public bool isAdmin { get; set; }
        public bool isSudija { get; set; }
        public bool isZapisnicar { get; set; }


        [ForeignKey("GradID")]
        public Grad Grad { get; set; }
        public int GradID { get; set; }

        [ForeignKey("SpolID")]
        public Spol Spol { get; set; }
        public int SpolID { get; set; }
        public bool obrisan { get; set; }

        public bool isAktiviran { get; set; }
        public string? aktivacijaGUID { get; set; }


    }
}
