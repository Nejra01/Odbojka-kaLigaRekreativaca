namespace Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Rezultati
{
    public class RezultatiGetAllVM
    {
        public int RezultatiID { get; set; }

        public int UtakmicaID { get; set; }
        public string NazivUtakmice { get; set; }

        public int TimID { get; set; }
        public string TimOpis { get; set; }

        public int SetoviID { get; set; }
        public string SetOpis { get; set; }

        public int OsvojeniBodovi { get; set; }
        public int IzgubljeniBodovi { get; set; }
    }
}
