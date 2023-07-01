using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Odbojkaska_Liga_Rekreativaca.Core.Modeli
{
    public class UtakmicaKorisnik
    {
        //UTAKMICA KORISNIK ULOGA 
        public int UtakmicaKorisnikID { get; set; }

        [ForeignKey("UtakmicaID")]
        public Utakmica Utakmica { get; set; }
        public int UtakmicaID { get; set; }

        [ForeignKey("KorisnikUlogaID")]
        public KorisnikUloga Korisnikuloga { get; set; }
        public int KorisnikUlogaID { get; set; }
        public bool obrisan { get; set; }

    }
}
