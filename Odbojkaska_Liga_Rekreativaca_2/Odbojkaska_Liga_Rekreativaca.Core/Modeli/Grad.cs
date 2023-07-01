using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Odbojkaska_Liga_Rekreativaca.Core.Modeli
{
    public class Grad
    {
        public int GradID { get; set; }
        public string ImeGrada { get; set; }

        [ForeignKey("KantonID")]
        public Kanton Kanton { get; set; }
        public int KantonID { get; set; }
        public bool obrisan { get; set; }

    }
}
