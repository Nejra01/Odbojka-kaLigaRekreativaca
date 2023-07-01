namespace Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Igrac
{
    public class IgracGetAllVM
    {
        public int IgracID { get; set; }

        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string BrojTelefona { get; set; }
        public string EmailAdresa { get; set; }
        //     public string Username { get; set; }
        //   public string Password { get; set; }
        public int SpolID { get; set; }

        //spol
        public string SpolOpis { get; set; }



        //pozicija

        public int PozicijaID { get; set; }
        public string PozicijaOpis { get; set; }

        //grad
        public int GradID { get; set; }
        public string GradOpis { get; set; }

    }
}
