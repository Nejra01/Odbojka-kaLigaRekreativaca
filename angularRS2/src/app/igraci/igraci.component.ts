import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";

@Component({
  selector: 'app-igraci',
  templateUrl: './igraci.component.html',
  styleUrls: ['./igraci.component.css']
})
export class IgraciComponent implements OnInit {

  odabrani_igrac: any;
  spol_podaci:any;
  pozicija_podaci:any;
  grad_podaci:any;
  constructor(private httpKlijent: HttpClient) {
  }


  igrac_podaci: any;
  filter_ime: any = "";
  p: number;


  preuzmi_podatke() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Igrac/GetAll").subscribe(x => {
      this.igrac_podaci = x;
    });

  }
  preuzmi_pozicije() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Pozicija/Get").subscribe(x => {
      this.pozicija_podaci = x;
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
    this.httpKlijent.post(MojConfig.adresa_servera+ "/Igrac/Delete?id=" + x.igracID, this.odabrani_igrac)
      .subscribe(x =>{
        this.preuzmi_podatke();
      });
  }
  ngOnInit(): void {
    this.preuzmi_podatke();
    this.preuzmi_grad();
    this.preuzmi_spol();
    this.preuzmi_pozicije();
  }

  getpodaci() {
    if (this.igrac_podaci == null)
      return [];
    return this.igrac_podaci.filter((x: any) => x.ime.toLowerCase().startsWith(this.filter_ime.toLowerCase()) || x.prezime.toLowerCase().startsWith(this.filter_ime.toLowerCase()));
  }

  snimi() {
    if (!this.odabrani_igrac.igracID) {
      this.httpKlijent.post(MojConfig.adresa_servera + "/Igrac/Add", this.odabrani_igrac)
        .subscribe(x => {
          this.preuzmi_podatke();
        });
    } else {
      this.httpKlijent.post(MojConfig.adresa_servera + "/Igrac/Update?id=" + this.odabrani_igrac.igracID, this.odabrani_igrac)
        .subscribe(x => {
          this.preuzmi_podatke();
        });
    }
  }

  novi_igrac_dugme() {
    this.odabrani_igrac = {
      igracID: 0,
      ime: "",
      prezime: "",
      datumRodjenja: "",
      spolID: 0,
      gradID: 0,
      pozicijaID: 0,
      brojTelefona: "",
      emailAdresa: "",
      spolOpis: "",
      pozicijaOpis: "",
      gradOpis: "",
      obrisan:false


    };
  }
  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

}
