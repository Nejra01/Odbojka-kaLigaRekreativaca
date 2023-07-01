using System.ComponentModel.DataAnnotations.Schema;

namespace Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Dvorana
{
    public class DvoranaGetAllVM
    {
        public int DvoranaID { get; set; }
        public int GradID { get; set; }
        public string Adresa { get; set; }
        public string Grad { get; set; }
        public string ImeDvorane { get; set; }
    }
}
