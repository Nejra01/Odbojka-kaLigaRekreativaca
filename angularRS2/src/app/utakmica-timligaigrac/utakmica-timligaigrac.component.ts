import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {ActivatedRoute} from "@angular/router";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";

@Component({
  selector: 'app-utakmica-timligaigrac',
  templateUrl: './utakmica-timligaigrac.component.html',
  styleUrls: ['./utakmica-timligaigrac.component.css']
})
export class UtakmicaTimligaigracComponent implements OnInit {


  odabrana_utakmicatimligaigrac: any;
  utakmicatimligaid:number;
  podaci:any;
  utakmicaTimLiga_podaci:any;
  igrac_podaci:any;
  constructor(private httpKlijent: HttpClient, private route: ActivatedRoute) {
  }

  utakmicatimligaigrac_podaci: any;
  filter_ime: any = "";

  preuzmi_podatke() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/UtakmicaTimLigaIgrac/Get").subscribe(x => {
      this.utakmicatimligaigrac_podaci = x;
    });
  }

  preuzmi_utakmicaTimLiga() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/UtakmicaTimLiga/GetAll").subscribe(x => {
      this.utakmicaTimLiga_podaci = x;
    });}
  preuzmi_igrac() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Igrac/GetAll").subscribe(x => {
      this.igrac_podaci = x;
    });}

  obrisi(x:any) {
    this.httpKlijent.post(MojConfig.adresa_servera+ "/UtakmicaTimLigaIgrac/Delete?id=" + x.utakmicaTimLigaIgracID, this.odabrana_utakmicatimligaigrac)
      .subscribe(x =>{
        this.preuzmi_podatke();
      //  this.fetch();

      });
  }


  ngOnInit(): void {
    this.preuzmi_podatke();
    this.preuzmi_utakmicaTimLiga();
    this.preuzmi_igrac() ;
    /*this.route.params.subscribe(params => {
      this.utakmicatimligaid = +params['utakmicatimligaid']; // (+) converts string 'id' to a number

      //fetch detalji o studentu
      //-- upisani semestri
      //-- ocjene, uplate itd.
      //class UpisAkademskaGodina
      //studentid, akademskaGodinaid, godina_studija, cijena_skolarine, bool obnova, datum_upisazimski

      this.fetch();

    });*/
  }

  getpodaci() {
    if (this.utakmicatimligaigrac_podaci == null)
      return [];
    return this.utakmicatimligaigrac_podaci;
  }

  snimi() {
    if (!this.odabrana_utakmicatimligaigrac.utakmicaTimLigaIgracID) {
      this.httpKlijent.post(MojConfig.adresa_servera + "/UtakmicaTimLigaIgrac/Add", this.odabrana_utakmicatimligaigrac)
        .subscribe(x => {
          this.preuzmi_podatke();
         // this.fetch();
        });
    } else {
      this.httpKlijent.post(MojConfig.adresa_servera + "/UtakmicaTimLigaIgrac/Update?id=" + this.odabrana_utakmicatimligaigrac.utakmicaTimLigaIgracID, this.odabrana_utakmicatimligaigrac)
        .subscribe(x => {
          this.preuzmi_podatke();
         // this.fetch();

        });
    }
  }

  nova_utakmicatimligaigrac_dugme() {
    this.odabrana_utakmicatimligaigrac = {
      utakmicaTimLigaIgracID: 0,
      utakmicaTimLigaID: 0,
      utakmicaNaziv: "",
      timOpis: "",
      ligaOpis: "",
      igracID: 0,
      igracOpis: "",
      obrisan:false
    };
  }
  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }
  /*private fetch() {
    this.httpKlijent.get(MojConfig.adresa_servera+ "/UtakmicaTimLigaIgrac?utakmicatimligaid="+this.utakmicatimligaid).subscribe((x:any)=>{
      this.podaci = x
    });
  }*/
}
