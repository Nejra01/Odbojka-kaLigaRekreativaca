import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {ActivatedRoute} from "@angular/router";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";

@Component({
  selector: 'app-utakmica-korisnik',
  templateUrl: './utakmica-korisnik.component.html',
  styleUrls: ['./utakmica-korisnik.component.css']
})
export class UtakmicaKorisnikComponent implements OnInit {

  odabrana_utakmicakorisnik: any;
  utakmicaid:number;
  podaci:any;
  utakmica_podaci:any;
  korisnik_podaci:any;
  p: number;
  constructor(private httpKlijent: HttpClient, private route: ActivatedRoute) {
  }

  utakmicakorisnik_podaci: any;
  filter_ime: any = "";

  preuzmi_podatke() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/UtakmicaKorisnik/Get").subscribe(x => {
      this.utakmicakorisnik_podaci = x;
    });
  }

  preuzmi_utakmice() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Utakmica/Get").subscribe(x => {
      this.utakmica_podaci = x;
    });}
  preuzmi_korisnike() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/KorisnikUloga/Get").subscribe(x => {
      this.korisnik_podaci = x;
    });}

  obrisi(x:any) {
    this.httpKlijent.post(MojConfig.adresa_servera+ "/UtakmicaKorisnik/Delete?id=" + x.utakmicaKorisnikID, this.odabrana_utakmicakorisnik)
      .subscribe(x =>{
        this.preuzmi_podatke();
        this.fetch();

      });
  }


  ngOnInit(): void {


    this.route.params.subscribe(params => {
      this.utakmicaid = +params['utakmicaid']; // (+) converts string 'id' to a number

      this.fetch();
      this.preuzmi_podatke();
      this.preuzmi_utakmice();
      this.preuzmi_korisnike() ;
    });

  }

  getpodaci() {
    if (this.podaci == null)
      return [];
    return this.podaci.gardovi.filter((x:any)=>x.korisnikuloga.uloga.nazivUloge.toLowerCase().startsWith(this.filter_ime.toLowerCase()));
  }

  snimi() {
    if (!this.odabrana_utakmicakorisnik.utakmicaKorisnikID) {
      this.httpKlijent.post(MojConfig.adresa_servera + "/UtakmicaKorisnik/Add", this.odabrana_utakmicakorisnik)
        .subscribe(x => {
          this.preuzmi_podatke();
          this.fetch();

        });
    } else {
      this.httpKlijent.post(MojConfig.adresa_servera + "/UtakmicaKorisnik/Update?id=" + this.odabrana_utakmicakorisnik.utakmicaKorisnikID, this.odabrana_utakmicakorisnik)
        .subscribe(x => {
          this.preuzmi_podatke();
          this.fetch();

        });
    }
  }

  nova_utakmicakorisnik_dugme() {
    this.odabrana_utakmicakorisnik = {
      utakmicaKorisnikID: 0,
      utakmicaID: 0,
      nazivUtakmice: "",
      korisnikID: 0,
      korinsikOpis: "",
      obrisan:false
    };
  }
  private fetch() {
    this.httpKlijent.get(MojConfig.adresa_servera+ "/UtakmicaKorisnik?utakmicaid="+this.utakmicaid).subscribe((x:any)=>{
      this.podaci = x
    });
  }

  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }
}
