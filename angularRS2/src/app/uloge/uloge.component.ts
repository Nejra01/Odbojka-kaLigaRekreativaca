import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";

@Component({
  selector: 'app-uloge',
  templateUrl: './uloge.component.html',
  styleUrls: ['./uloge.component.css']
})
export class UlogeComponent implements OnInit {

  odabrani_uloge:any;

  constructor(private httpKlijent: HttpClient) {
  }


  uloge_podaci: any;


  preuzmi_podatke()
  {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Uloge/GetAll").subscribe(x=>{
      this.uloge_podaci = x;
    });

  }

  ngOnInit(): void {
    this.preuzmi_podatke();

  }

  getpodaci() {
    if (this.uloge_podaci==null)
      return [];
    return this.uloge_podaci;
  }

  snimi() {
    this.httpKlijent.post(MojConfig.adresa_servera+ "/Uloge/Update?id=" + this.odabrani_uloge.ulogaID, this.odabrani_uloge)
      .subscribe((povratnaVrijednost:any) =>{
        alert("uredu..." + povratnaVrijednost.nazivUloge);
      });
  }

  obrisi(x:any) {
    this.httpKlijent.post(MojConfig.adresa_servera+ "/Uloge/Delete?id=" + x.ulogaID, this.odabrani_uloge)
      .subscribe(x =>{
        this.preuzmi_podatke();
      });
  }


  nova_uloga_dugme() {
    this.odabrani_uloge = {
      ulogaID:0,
      nazivUloge:"",
      obrisan:false

    };
  }
  snimi_dugme() {
    if(!this.odabrani_uloge.ulogaID) {
      this.httpKlijent.post(`${MojConfig.adresa_servera}/Uloge/Add`, this.odabrani_uloge).subscribe(x=>{
        this.preuzmi_podatke();
      });
    }
    else{
      this.httpKlijent.post(`${MojConfig.adresa_servera}/Uloge/Update/?id=`+this.odabrani_uloge.ulogaID, this.odabrani_uloge).subscribe(x=>{
        this.preuzmi_podatke();
      });
    }
  }
  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }
}


