using System.ComponentModel.DataAnnotations.Schema;

namespace Odbojkaska_Liga_Rekreativaca.Core.Modeli
{
    public class Dvorana
    {
        public int DvoranaID { get; set; }
        public string Adresa { get; set; }

        [ForeignKey("GradID")]
        public Grad Grad { get; set; }
        public int GradID { get; set; }
        public string ImeDvorane { get; set; }
        public bool obrisan { get; set; }


    }
}
