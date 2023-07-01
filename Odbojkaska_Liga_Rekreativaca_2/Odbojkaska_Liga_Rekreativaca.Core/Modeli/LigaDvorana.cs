using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odbojkaska_Liga_Rekreativaca.Core.Modeli
{
    public class LigaDvorana
    {
        public int LigaDvoranaID { get; set; }
        [ForeignKey("DvoranaID")]
        public Dvorana Dvorana { get; set; }
        public int DvoranaID { get; set; }

        [ForeignKey("LigaID")]
        public Liga Liga { get; set; }
        public int LigaID { get; set; }
        public bool obrisan { get; set; }

    }
}
