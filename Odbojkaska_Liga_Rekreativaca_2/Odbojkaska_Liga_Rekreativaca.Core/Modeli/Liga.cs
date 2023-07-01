using System.ComponentModel.DataAnnotations.Schema;

namespace Odbojkaska_Liga_Rekreativaca.Core.Modeli
{
    public class Liga
    {
        
        public int LigaID { get; set; }
        public string GodinaLige { get; set; }
        public bool obrisan { get; set; }


    }
}
