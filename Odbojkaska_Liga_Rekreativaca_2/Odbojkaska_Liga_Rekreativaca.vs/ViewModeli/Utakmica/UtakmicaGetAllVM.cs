namespace Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Utakmica
{
    public class UtakmicaGetAllVM
    {
        public int UtakmicaID { get; set; }
        public DateTime DatumIgranja { get; set; }
        public DateTime VrijemePocetka { get; set; }
        public string NazivUtakmice { get; set; }



        public int StatusID { get; set; }
        public string StatusOpis { get; set; }


        public int KoloID { get; set; }
        public string KoloOpis { get; set; }
    }
}
