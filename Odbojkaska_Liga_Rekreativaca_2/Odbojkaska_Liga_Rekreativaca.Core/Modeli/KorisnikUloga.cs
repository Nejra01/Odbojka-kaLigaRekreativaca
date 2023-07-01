using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odbojkaska_Liga_Rekreativaca.Core.Modeli
{
    public class KorisnikUloga
    {
        public int KorisnikUlogaID { get; set; }
        [ForeignKey("KorisnikID")]
        public Korisnik Korisnik { get; set; }
        public int KorisnikID { get; set; }

        [ForeignKey("UlogaID")]
        public Uloga Uloga { get; set; }
        public int UlogaID { get; set; }
        public bool obrisan { get; set; }

    }
}
