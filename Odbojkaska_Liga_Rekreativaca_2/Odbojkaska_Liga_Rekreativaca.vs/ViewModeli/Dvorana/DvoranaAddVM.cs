using System.ComponentModel.DataAnnotations.Schema;

namespace Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Dvorana
{
    public class DvoranaAddVM
    {
        public string Adresa { get; set; }
        public int GradID { get; set; }
        public string ImeDvorane { get; set; }
    }
}
