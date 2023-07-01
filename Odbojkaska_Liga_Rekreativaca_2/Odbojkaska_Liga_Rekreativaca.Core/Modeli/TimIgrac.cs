using System.ComponentModel.DataAnnotations.Schema;

namespace Odbojkaska_Liga_Rekreativaca.Core.Modeli

{
    public class TimIgrac
    {
        public int TimIgracID { get; set; }

        [ForeignKey("TimID")]
        public Tim Tim { get; set; }
        public int TimID { get; set; }


        [ForeignKey("IgracID")]
        public Igrac Igrac { get; set; }
        public int IgracID { get; set; }
        public bool obrisan { get; set; }

    }
}
