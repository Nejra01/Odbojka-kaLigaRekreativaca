using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Odbojkaska_Liga_Rekreativaca.Core.Modeli
{
    public class UtakmicaTimLiga
    {
        public int UtakmicaTimLigaID { get; set; }

        [ForeignKey("UtakmicaID")]
        public Utakmica Utakmica { get; set; }
        public int UtakmicaID { get; set; }

        [ForeignKey("TimLigaID")]
        public TimLiga TimLiga { get; set; }
        public int TimLigaID { get; set; }
        public bool obrisan { get; set; }


    }
}
