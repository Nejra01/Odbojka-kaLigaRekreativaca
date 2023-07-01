import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";

@Component({
  selector: 'app-pozicije',
  templateUrl: './pozicije.component.html',
  styleUrls: ['./pozicije.component.css']
})
export class PozicijeComponent implements OnInit {

  odabrana_pozicija:any;

  constructor(private httpKlijent: HttpClient) {
  }


  pozicija_podaci: any;


  preuzmi_podatke()
  {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Pozicija/Get").subscribe(x=>{
      this.pozicija_podaci = x;
    });

  }

  ngOnInit(): void {
    this.preuzmi_podatke();

  }

  getpodaci() {
    if (this.pozicija_podaci==null)
      return [];
    return this.pozicija_podaci;
  }

  snimi() {
    this.httpKlijent.post(MojConfig.adresa_servera+ "/Pozicija/Update?id=" + this.odabrana_pozicija.pozicijaID, this.odabrana_pozicija)
      .subscribe((povratnaVrijednost:any) =>{
        alert("uredu..." + povratnaVrijednost.nazivPozicije);
      });
  }

  obrisi(x:any) {
    this.httpKlijent.post(MojConfig.adresa_servera+ "/Pozicija/Delete?id=" + x.pozicijaID, this.odabrana_pozicija)
      .subscribe(x =>{
        this.preuzmi_podatke();
      });
  }


  nova_pozicija_dugme() {
    this.odabrana_pozicija = {
      pozicijaID:0,
      nazivPozicije:"",
      obrisan:false

    };
  }
  snimi_dugme() {
    if(!this.odabrana_pozicija.pozicijaID) {
      this.httpKlijent.post(`${MojConfig.adresa_servera}/Pozicija/Add`, this.odabrana_pozicija).subscribe(x=>{
        this.preuzmi_podatke();
      });
    }
    else{
      this.httpKlijent.post(`${MojConfig.adresa_servera}/Pozicija/Update/?id=`+this.odabrana_pozicija.pozicijaID, this.odabrana_pozicija).subscribe(x=>{
        this.preuzmi_podatke();
      });
    }
  }
  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }
}


