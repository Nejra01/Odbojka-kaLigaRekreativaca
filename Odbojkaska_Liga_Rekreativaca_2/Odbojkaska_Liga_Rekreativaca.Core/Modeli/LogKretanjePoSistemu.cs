using Odbojkaska_Liga_Rekreativaca.Core.Modeli;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Odbojkaska_Liga_Rekreativaca.Core.Modeli
{
    public class LogKretanjePoSistemu
    {
        [Key]
        public int id { get; set; }
        [ForeignKey(nameof(Korisnik))]
        public string KorisnikID { get; set; }
        public Korisnik Korisnik { get; set; }
        public string queryPath { get; set; }
        public string postData { get; set; }
        public DateTime vrijeme { get; set; }
        public string ipAdresa { get; set; }
        public string exceptionMessage { get; set; }
        public bool isException { get; set; }
    }
}
