import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";

@Component({
  selector: 'app-tim-liga',
  templateUrl: './tim-liga.component.html',
  styleUrls: ['./tim-liga.component.css']
})
export class TimLigaComponent implements OnInit {
  odabrani_timliga: any;
  timigrac_podaci:any;
  liga_podaci:any;

  constructor(private httpKlijent: HttpClient) {
  }

  timliga_podaci: any;
  filter_ime: any = "";


  preuzmi_podatke() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/TimLiga/Get").subscribe(x => {
      this.timliga_podaci = x;
    });

  }
  preuzmi_timigrac() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/TimIgrac/GetAll").subscribe(x => {
      this.timigrac_podaci = x;
    });

  }
  preuzmi_lige() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Liga/GetAll").subscribe(x => {
      this.liga_podaci = x;
    });}

  obrisi(x:any) {
    this.httpKlijent.post(MojConfig.adresa_servera+ "/TimLiga/Delete?id=" + x.timLigaID, this.odabrani_timliga)
      .subscribe(x =>{
        this.preuzmi_podatke();
      });
  }
  ngOnInit(): void {
    this.preuzmi_podatke();
    this.preuzmi_timigrac();
    this.preuzmi_lige() ;
  }

  getpodaci() {
    if (this.timliga_podaci == null)
      return [];
    return this.timliga_podaci;
  }

  snimi() {
    if (!this.odabrani_timliga.timLigaID) {
      this.httpKlijent.post(MojConfig.adresa_servera + "/TimLiga/Add", this.odabrani_timliga)
        .subscribe(x => {
          this.preuzmi_podatke();
        });
    } else {
      this.httpKlijent.post(MojConfig.adresa_servera + "/TimLiga/Update?id=" + this.odabrani_timliga.timLigaID, this.odabrani_timliga)
        .subscribe(x => {
          this.preuzmi_podatke();
        });
    }
  }

  novi_timliga_dugme() {
    this.odabrani_timliga = {
      timLigaID: 0,
      datumPrijave: "",
      timIgracID: 0,
      timOpis: "",
      igracOpis: "",
      ligaID: 0,
      ligaOpis: "",
      obrisan:false
    };
  }
  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }
}
