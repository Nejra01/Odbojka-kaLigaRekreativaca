import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";

@Component({
  selector: 'app-status',
  templateUrl: './status.component.html',
  styleUrls: ['./status.component.css']
})
export class StatusComponent implements OnInit {

  odabrani_status:any;

  constructor(private httpKlijent: HttpClient) {
  }


  status_podaci: any;


  preuzmi_podatke()
  {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Status/Get").subscribe(x=>{
      this.status_podaci = x;
    });

  }

  ngOnInit(): void {
    this.preuzmi_podatke();

  }

  getpodaci() {
    if (this.status_podaci==null)
      return [];
    return this.status_podaci;
  }

  snimi() {
    this.httpKlijent.post(MojConfig.adresa_servera+ "/Status/Update?id=" + this.odabrani_status.statusID, this.odabrani_status)
      .subscribe((povratnaVrijednost:any) =>{
        alert("uredu..." + povratnaVrijednost.nazivStatusa);
      });
  }

  obrisi(x:any) {
    this.httpKlijent.post(MojConfig.adresa_servera+ "/Status/Delete?id=" + x.statusID, this.odabrani_status)
      .subscribe(x =>{
        this.preuzmi_podatke();
      });
  }


  novi_status_dugme() {
    this.odabrani_status = {
      id:0,
      nazivStatusa:"",
      obrisan:false

    };
  }
  snimi_dugme() {
    if(!this.odabrani_status.statusID) {
      this.httpKlijent.post(`${MojConfig.adresa_servera}/Status/Add`, this.odabrani_status).subscribe(x=>{
        this.preuzmi_podatke();
      });
    }
    else{
      this.httpKlijent.post(`${MojConfig.adresa_servera}/Status/Update/?id=`+this.odabrani_status.statusID, this.odabrani_status).subscribe(x=>{
        this.preuzmi_podatke();
      });
    }
  }
  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }
}


