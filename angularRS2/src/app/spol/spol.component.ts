import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";

@Component({
  selector: 'app-spol',
  templateUrl: './spol.component.html',
  styleUrls: ['./spol.component.css']
})
export class SpolComponent implements OnInit {

  odabrani_spol:any;

  constructor(private httpKlijent: HttpClient) {
  }

  spol_podaci: any;

  preuzmi_podatke()
  {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Spol/Get").subscribe(x=>{
      this.spol_podaci = x;
    });
  }

  ngOnInit(): void {
    this.preuzmi_podatke();
  }

  getpodaci() {
    if (this.spol_podaci==null)
      return [];
    return this.spol_podaci;
  }

  snimi() {
    this.httpKlijent.post(MojConfig.adresa_servera+ "/Spol/Update?id=" + this.odabrani_spol.spolID, this.odabrani_spol)
      .subscribe((povratnaVrijednost:any) =>{
        alert("uredu..." + povratnaVrijednost.nazivSpola);
      });
  }

  obrisi(x:any) {
    this.httpKlijent.post(MojConfig.adresa_servera+ "/Spol/Delete?id=" + x.spolID, this.odabrani_spol)
      .subscribe(x =>{
        this.preuzmi_podatke();
      });
  }


  novi_spol_dugme() {
    this.odabrani_spol = {
      spolID:0,
      nazivSpola:"",
      obrisan:false

    };
  }
  snimi_dugme() {
    if(!this.odabrani_spol.spolID) {
      this.httpKlijent.post(`${MojConfig.adresa_servera}/Spol/Add`, this.odabrani_spol).subscribe(x=>{
        this.preuzmi_podatke();
      });
    }
    else{
      this.httpKlijent.post(`${MojConfig.adresa_servera}/Spol/Update/?id=`+this.odabrani_spol.spolID, this.odabrani_spol).subscribe(x=>{
        this.preuzmi_podatke();
      });
    }
  }
  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

}


