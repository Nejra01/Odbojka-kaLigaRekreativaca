

export class LoginInformacije {
  autentifikacijaToken:        AutentifikacijaToken=null;
  isLogiran:                   boolean=false;
}

export interface AutentifikacijaToken {
  id:                   number;
  vrijednost:           string;
  korisnikID:    number;
  korisnik:      Korisnik;
  vrijemeEvidentiranja: Date;
  ipAdresa:             string;
}

export interface Korisnik {
  KorisnikID:                 number;
  Ime:      string;
  Prezime:    string;
  DatumRodjenja:    string;
  BrojTelefona:    string;
  Email:    string;
  korisnickoIme:    string;
  GradID:    number;
  SpolID:    number;
  isSudija:        boolean;
  isZapisnicar:          boolean;
  isAdmin:          boolean;
  isAktiviran:      boolean;
}
