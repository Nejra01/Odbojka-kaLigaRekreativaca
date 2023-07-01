import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {ActivatedRoute, Router} from "@angular/router";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";

@Component({
  selector: 'app-utakmica',
  templateUrl: './utakmica.component.html',
  styleUrls: ['./utakmica.component.css']
})
export class UtakmicaComponent implements OnInit {

  odabrana_utakmica: any;
  studentid:number;
  podaci:any;
  korisnik_podaci:any;
  status_podaci:any;
  kolo_podaci:any;
  p: number;
  constructor(private httpKlijent: HttpClient, private route: ActivatedRoute, private router: Router) {
  }

  utakmica_podaci: any;
  filter_ime: any = "";

  preuzmi_podatke() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Utakmica/Get").subscribe(x => {
      this.utakmica_podaci = x;
    });
  }

  preuzmi_status() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Status/Get").subscribe(x => {
      this.status_podaci = x;
    });}
  preuzmi_kolo() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Kolo/GetAll").subscribe(x => {
      this.kolo_podaci = x;
    });}

  obrisi(x:any) {
    this.httpKlijent.post(MojConfig.adresa_servera+ "/Utakmica/Delete?id=" + x.utakmicaID, this.odabrana_utakmica)
      .subscribe(x =>{
        this.preuzmi_podatke();
        this.fetch();
      });
  }

  ngOnInit(): void {
   // this.preuzmi_podatke();
   // this.preuzmi_kolo() ;
    this.route.params.subscribe(params => {
      this.studentid = +params['koloid']; // (+) converts string 'id' to a number

      this.fetch();

      this.preuzmi_status();

    });
  }

  getpodaci() {
    if (this.podaci == null)
      return [];

   return this.podaci.kola.filter((x:any)=>x.nazivUtakmice.toLowerCase().startsWith(this.filter_ime.toLowerCase()));
  }

  snimi() {
    if (!this.odabrana_utakmica.utakmicaID) {
      this.httpKlijent.post(MojConfig.adresa_servera + "/Utakmica/Add", this.odabrana_utakmica)
        .subscribe(x => {
          this.preuzmi_podatke();
          this.fetch();

        });
    } else {
      this.httpKlijent.post(MojConfig.adresa_servera + "/Utakmica/Update?id=" + this.odabrana_utakmica.utakmicaID, this.odabrana_utakmica)
        .subscribe(x => {
          this.preuzmi_podatke();
          this.fetch();

        });
    }
  }

  nova_utakmica_dugme() {
    this.odabrana_utakmica = {
      utakmicaID: 0,
      datumIgranja: "",
      vrijemePocetka: "",
      nazivUtakmice: "",
      statusID: 0,
      statusOpis: "",
      koloID: 0,
      koloOpis: "",
      obrisan:false
    };
  }


  private fetch() {
    this.httpKlijent.get(MojConfig.adresa_servera+ "/Utakmica?koloID="+this.studentid).subscribe((x:any)=>{
      this.podaci = x
    });
  }

  otvori_detalje(s: any) {
    //
    this.router.navigate(['/putanja-rezultati', s.utakmicaID]);
  }

  otvori_detalje_sudije(x: any) {
    this.router.navigate(['/putanja-utakmica-korisnik', x.utakmicaID]);
  }

  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }
}

