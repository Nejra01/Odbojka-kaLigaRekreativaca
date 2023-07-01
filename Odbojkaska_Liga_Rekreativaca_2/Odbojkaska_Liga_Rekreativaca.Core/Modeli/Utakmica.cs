using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Odbojkaska_Liga_Rekreativaca.Core.Modeli
{
    public class Utakmica
    {
        public int UtakmicaID { get; set; }
        public string NazivUtakmice { get; set; }   
        public DateTime DatumIgranja { get; set; }
        public DateTime VrijemePocetka { get; set; }


        [ForeignKey("StatusID")]
        public Status Status { get; set; }
        public int StatusID { get; set; }


        [ForeignKey("KoloID")]
        public Kolo Kolo { get; set; }
        public int KoloID { get; set; }
        public bool obrisan { get; set; }


    }
}
