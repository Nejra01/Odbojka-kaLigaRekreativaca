using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odbojkaska_Liga_Rekreativaca.Core.Modeli
{
    public class Rezultati
    {
        public int RezultatiID { get; set; }


        [ForeignKey("UtakmicaID")]
        public Utakmica Utakmica { get; set; }
        public int UtakmicaID { get; set; }


        [ForeignKey("TimID")]
        public Tim Tim { get; set; }
        public int TimID { get; set; }


        [ForeignKey("SetoviID")]
        public Setovi Setovi { get; set; }
        public int SetoviID { get; set; }

        public int OsvojeniBodovi { get; set; }
        public int IzgubljeniBodovi { get; set; }
        public bool obrisan { get; set; }

    }
}
