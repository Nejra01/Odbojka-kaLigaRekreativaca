using System.ComponentModel.DataAnnotations.Schema;

namespace Odbojkaska_Liga_Rekreativaca.Core.Modeli
{
    public class Kolo
    {
        public int KoloID { get; set; }
        public int BrojKola { get; set; }

        [ForeignKey("LigaID")]
        public Liga Liga { get; set; }
        public int LigaID { get; set; }
        public bool obrisan { get; set; }
    }
}