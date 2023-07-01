namespace Odbojkaska_Liga_Rekreativaca.vs.ViewModeli.Korisnik
{
    public class KorisnikGetAllVM
    {
        public int KorisnikID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string BrojTelefona { get; set; }
        public string Email { get; set; }
         public string Username { get; set; }
         public string Password { get; set; }

        public bool isSudija { get; set; }
        public bool isZapisnicar { get; set; }
        public bool isAdmin { get; set; }

        //public int UlogaID { get; set; }
        public string UlogaOpis { get; set; }

        public int GradID { get; set; }
        public string GradOpis { get; set; }

        public int SpolID { get; set; }
        public string SpolOpis { get; set; }

    }
}
