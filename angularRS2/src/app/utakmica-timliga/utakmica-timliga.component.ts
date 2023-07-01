import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {ActivatedRoute, Router} from "@angular/router";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";

@Component({
  selector: 'app-utakmica-timliga',
  templateUrl: './utakmica-timliga.component.html',
  styleUrls: ['./utakmica-timliga.component.css']
})
export class UtakmicaTimligaComponent implements OnInit {

podaci:any;
utakmicaID:number;
  odabrana_utakmicatimliga: any;

  utakmica_podaci:any;
  timliga_podaci:any;
  constructor(private httpKlijent: HttpClient, private route: ActivatedRoute,private router: Router) {
  }

  utakmicatimliga_podaci: any;
  filter_ime: any = "";

  preuzmi_podatke() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/UtakmicaTimLiga/GetAll").subscribe(x => {
      this.utakmicatimliga_podaci = x;
    });
  }

  preuzmi_utakmice() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Utakmica/Get").subscribe(x => {
      this.utakmica_podaci = x;
    });}
  preuzmi_timliga() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/TimLiga/Get").subscribe(x => {
      this.timliga_podaci = x;
    });}

  obrisi(x:any) {
    this.httpKlijent.post(MojConfig.adresa_servera+ "/UtakmicaTimLiga/Delete?id=" + x.utakmicaTimLigaID, this.odabrana_utakmicatimliga)
      .subscribe(x =>{
        this.preuzmi_podatke();
       // this.fetch();

      });
  }

  ngOnInit(): void {
    this.preuzmi_podatke();
   this.preuzmi_utakmice();
    this.preuzmi_timliga() ;
   /* this.route.params.subscribe(params => {
      this.utakmicaID = +params['utakmicaid']; // (+) converts string 'id' to a number

      this.fetch();
      this.preuzmi_timliga();
    });*/
  }

  getpodaci() {
    if (this.utakmicatimliga_podaci == null)
      return [];
    return this.utakmicatimliga_podaci;
  }

  snimi() {
    if (!this.odabrana_utakmicatimliga.utakmicaTimLigaID) {
      this.httpKlijent.post(MojConfig.adresa_servera + "/UtakmicaTimLiga/Add", this.odabrana_utakmicatimliga)
        .subscribe(x => {
          this.preuzmi_podatke();
         // this.fetch();

        });
    } else {
      this.httpKlijent.post(MojConfig.adresa_servera + "/UtakmicaTimLiga/Update?id=" + this.odabrana_utakmicatimliga.utakmicaTimLigaID, this.odabrana_utakmicatimliga)
        .subscribe(x => {
          this.preuzmi_podatke();
         // this.fetch();

        });
    }
  }

  nova_utakmicatimliga_dugme() {
    this.odabrana_utakmicatimliga = {
      utakmicaTimLigaID: 0,
      utakmicaID: 0,
      nazivUtakmice: "",
      timLigaID: 0,
      timOpis: "",
      ligaOpis: "",
      obrisan:false
    };
  }
  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }
 /* private fetch() {
    this.httpKlijent.get(MojConfig.adresa_servera+ "/UtakmicaTimLiga?utakmicaID="+this.utakmicaID).subscribe((x:any)=>{
      this.podaci = x
    });
  }

  otvori_detalje(s: any) {
    //
    this.router.navigate(['/putanja-utakmica-timligaigrac', s.utakmicaTimLigaID]);
  }
*/
}
