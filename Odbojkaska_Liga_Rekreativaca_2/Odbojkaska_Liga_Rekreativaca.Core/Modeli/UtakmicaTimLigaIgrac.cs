using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odbojkaska_Liga_Rekreativaca.Core.Modeli
{
    public class UtakmicaTimLigaIgrac
    {
        public int UtakmicaTimLigaIgracID { get; set; }

        [ForeignKey("UtakmicaTimLigaID")]
        public UtakmicaTimLiga UtakmicaTimLiga { get; set; }
        public int UtakmicaTimLigaID { get; set; }


        [ForeignKey("IgracID")]
        public Igrac Igrac { get; set; }
        public int IgracID { get; set; }
        public bool obrisan { get; set; }

    }
}
