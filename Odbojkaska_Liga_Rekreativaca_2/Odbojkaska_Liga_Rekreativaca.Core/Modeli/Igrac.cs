using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Odbojkaska_Liga_Rekreativaca.Core.Modeli
{
    public class Igrac
    {
        public int IgraciD { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string BrojTelefona { get; set; }
        public string EmailAdresa { get; set; }
      //  public bool obrisan { get; set; }
        //     public string Username { get; set; }
        //   public string Password { get; set; }

        //spol
        [ForeignKey("SpolID")]
        public Spol Spol { get; set; }
        public int SpolID { get; set; }
        //pozicija
        [ForeignKey("PozicijaID")]
        public Pozicija Pozicija { get; set; }
        public int PozicijaID { get; set; }
        //grad
        [ForeignKey("GradID")]
        public Grad Grad { get; set; }
        public int GradID { get; set; }
        public bool obrisan { get; set; }

    }
}
