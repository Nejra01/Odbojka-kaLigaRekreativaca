namespace Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.TimIgrac
{
    public class TimIgracGetAllVM
    {
        public int TimIgracID { get; set; }

        public int TimID { get; set; }
        public string TimOpis { get; set; }

        public int IgracID { get; set; }
        public string IgracOpis { get; set; }

        public DateTime DatumPrijave { get; set; }
    }
}
