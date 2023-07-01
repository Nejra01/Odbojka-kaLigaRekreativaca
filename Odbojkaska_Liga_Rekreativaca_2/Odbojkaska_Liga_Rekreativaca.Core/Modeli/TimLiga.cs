using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Odbojkaska_Liga_Rekreativaca.Core.Modeli
{
    public class TimLiga
    {
        public int TimLigaID { get; set; }
        public DateTime DatumPrijave { get; set; }


        [ForeignKey("TimIgracID")]
        public TimIgrac TimIgrac { get; set; }
        public int TimIgracID { get; set; }


        [ForeignKey("LigaID")]
        public Liga Liga { get; set; }
        public int LigaID { get; set; }
        public bool obrisan { get; set; }

    }
}
