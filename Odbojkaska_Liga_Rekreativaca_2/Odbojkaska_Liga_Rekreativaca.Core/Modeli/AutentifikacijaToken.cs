using Odbojkaska_Liga_Rekreativaca.Core.Modeli;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Odbojkaska_Liga_Rekreativaca.Core.Modeli
{
    public class AutentifikacijaToken
    {
        [Key]
        public int id { get; set; }
        public string vrijednost { get; set; }
        [ForeignKey(nameof(Korisnik))]
        public int KorisnikID { get; set; }
        public Korisnik Korisnik { get; set; }
        public DateTime vrijemeEvidentiranja { get; set; }
        public string ipAdresa { get; set; }

        [JsonIgnore]
        public string twoFCode { get; set; }
        public bool twoFJelOtkljucano { get; set; }

    }
}
