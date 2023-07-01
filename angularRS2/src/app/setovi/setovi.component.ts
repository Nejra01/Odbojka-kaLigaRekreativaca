import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";

@Component({
  selector: 'app-setovi',
  templateUrl: './setovi.component.html',
  styleUrls: ['./setovi.component.css']
})
export class SetoviComponent implements OnInit {
  odabrani_set:any;

  constructor(private httpKlijent: HttpClient) {
  }

  set_podaci: any;

  preuzmi_podatke()
  {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Setovi/GetAll").subscribe(x=>{
      this.set_podaci = x;
    });
  }

  ngOnInit(): void {
    this.preuzmi_podatke();
  }

  getpodaci() {
    if (this.set_podaci==null)
      return [];
    return this.set_podaci;
  }

  snimi() {
    this.httpKlijent.post(MojConfig.adresa_servera+ "/Setovi/Update?id=" + this.odabrani_set.ligaID, this.odabrani_set)
      .subscribe((povratnaVrijednost:any) =>{
        alert("uredu..." + povratnaVrijednost.brojSeta);
      });
  }

  obrisi(x:any) {
    this.httpKlijent.post(MojConfig.adresa_servera+ "/Setovi/Delete?id=" + x.setID, this.odabrani_set)
      .subscribe(x =>{
        this.preuzmi_podatke();
      });
  }


  novi_set_dugme() {
    this.odabrani_set = {
      id:0,
      brojSeta:"",
      obrisan:false

    };
  }
  snimi_dugme() {
    if(!this.odabrani_set.setID) {
      this.httpKlijent.post(`${MojConfig.adresa_servera}/Setovi/Add`, this.odabrani_set).subscribe(x=>{
        this.preuzmi_podatke();
      });
    }
    else{
      this.httpKlijent.post(`${MojConfig.adresa_servera}/Setovi/Update/?id=`+this.odabrani_set.setID, this.odabrani_set).subscribe(x=>{
        this.preuzmi_podatke();
      });
    }
  }
  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }
}


