import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";
import {Router} from "@angular/router";

@Component({
  selector: 'app-korisnici',
  templateUrl: './korisnici.component.html',
  styleUrls: ['./korisnici.component.css']
})
export class KorisniciComponent implements OnInit {

  odabrani_korisnik:any;

  constructor(private httpKlijent: HttpClient, private router: Router) {
  }

isActive:true;
  korisnik_podaci: any;
  filter_ime: any="";
  uloga_podaci:any;
  grad_podaci:any;
  spol_podaci:any;
  p: number;


  preuzmi_podatke()
  {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Korisnik/GetAll").subscribe(x=>{
      this.korisnik_podaci = x;
    });

  }
  preuzmi_uloge(){
    this.httpKlijent.get(MojConfig.adresa_servera + "/Uloge/GetAll").subscribe(x => {
      this.uloga_podaci = x;
    });

  }
  preuzmi_spol() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Spol/Get").subscribe(x => {
      this.spol_podaci = x;
    });}
  preuzmi_grad() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Grad/GetAll").subscribe(x => {
      this.grad_podaci = x;
    });}

  obrisi(x:any) {
    this.httpKlijent.post(MojConfig.adresa_servera+ "/Korisnik/Delete?id=" + x.korisnikID, this.odabrani_korisnik)
      .subscribe(x =>{
        this.preuzmi_podatke();
      });
  }
  otvori_detalje(s: any) {
    //
    this.router.navigate(['/putanja-korisnik-uloge', s.korisnikID]);
  }
  ngOnInit(): void {
    this.preuzmi_podatke();
    this.preuzmi_grad();
    this.preuzmi_spol();
    this.preuzmi_uloge();
  }

  getpodaci() {
    if (this.korisnik_podaci==null)
      return [];
    return this.korisnik_podaci.filter((x:any)=>x.ime.toLowerCase().startsWith(this.filter_ime.toLowerCase()) || x.prezime.toLowerCase().startsWith(this.filter_ime.toLowerCase()));
  }

  snimi() {
    if(!this.odabrani_korisnik.korisnikID) {
      this.httpKlijent.post(MojConfig.adresa_servera + "/Korisnik/Add", this.odabrani_korisnik)
        .subscribe(x => {
          this.preuzmi_podatke();
        });
    }
    else{
      this.httpKlijent.post(MojConfig.adresa_servera + "/Korisnik/Update?id=" + this.odabrani_korisnik.korisnikID, this.odabrani_korisnik)
        .subscribe(x => {
          this.preuzmi_podatke();
        });
  }
  }


  novi_korinik_dugme() {
    this.odabrani_korisnik =
    {
      korisnikID: 0,
      ime: "",
      prezime: "",
      datumRodjenja: "",
      spolID: 0,
      gradID: 0,
      ulogaID: 0,
      brojTelefona: "",
      email: "",
      spolOpis: "",
      ulogaOpis: "",
      gradOpis: "",
      obrisan:false

  };
  }
  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }
}
