namespace Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Igrac
{
    public class IgracAddVM
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string BrojTelefona { get; set; }
        public string EmailAdresa { get; set; }
        //     public string Username { get; set; }
        //   public string Password { get; set; }

        //spol
        public int SpolID { get; set; }

        //pozicija

        public int PozicijaID { get; set; }
        //grad
      
        public int GradID { get; set; }
    }
}
