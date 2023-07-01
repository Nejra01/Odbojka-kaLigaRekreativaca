import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {Router} from "@angular/router";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";
import {LoginInformacije} from "../_helpers/login-informacije";
@Component({
  selector: 'app-liga',
  templateUrl: './liga.component.html',
  styleUrls: ['./liga.component.css']
})
export class LigaComponent implements OnInit {

  odabrana_liga:any;

  constructor(private httpKlijent: HttpClient, private router: Router) {
  }

  liga_podaci: any;
  filter_ime: any="";
  p: number;

  preuzmi_podatke()
  {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Liga/GetAll").subscribe(x=>{
      this.liga_podaci = x;
    });
  }

  ngOnInit(): void {
    this.preuzmi_podatke();

  }

  getpodaci() {
    if (this.liga_podaci==null)
      return [];
    return this.liga_podaci.filter((x:any)=>x.godinaLige.toLowerCase().startsWith(this.filter_ime.toLowerCase()));
  }

  snimi() {
    this.httpKlijent.post(MojConfig.adresa_servera+ "/Liga/Update?id=" + this.odabrana_liga.ligaID, this.odabrana_liga)
      .subscribe((povratnaVrijednost:any) =>{
        alert("uredu..." + povratnaVrijednost.godinaLige);
      });
  }

  obrisi(x:any) {
    this.httpKlijent.post(MojConfig.adresa_servera+ "/Liga/Delete?id=" + x.ligaID, this.odabrana_liga)
      .subscribe(x =>{
        this.preuzmi_podatke();
      });
  }


  nova_liga_dugme() {
    this.odabrana_liga = {
      id:0,
      godinaLige:"",
      obrisan:false

    };
  }
  snimi_dugme() {
    if(!this.odabrana_liga.ligaID) {
      this.httpKlijent.post(`${MojConfig.adresa_servera}/Liga/Add`, this.odabrana_liga).subscribe(x=>{
        this.preuzmi_podatke();
      });
    }
    else{
      this.httpKlijent.post(`${MojConfig.adresa_servera}/Liga/Update/?id=`+this.odabrana_liga.ligaID, this.odabrana_liga).subscribe(x=>{
        this.preuzmi_podatke();
      });
    }
  }

  otvori_detalje(s: any) {
    //
    this.router.navigate(['/putanja-kolo', s.ligaID]);
  }

  otvori_detalje01(s: any) {
    //
    this.router.navigate(['/putanja-liga-dvorana', s.ligaID]);
  }
  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

}


