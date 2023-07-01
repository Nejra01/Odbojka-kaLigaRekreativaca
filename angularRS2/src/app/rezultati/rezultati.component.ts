import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {ActivatedRoute, Router} from "@angular/router";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";

@Component({
  selector: 'app-rezultati',
  templateUrl: './rezultati.component.html',
  styleUrls: ['./rezultati.component.css']
})
export class RezultatiComponent implements OnInit {
  utakmicaid:number;

  podaci:any;
  odabrani_rezultat: any;
  utakmica_podaci:any;
  tim_podaci:any;
  set_podaci:any;
  constructor(private httpKlijent: HttpClient, private route: ActivatedRoute, private router: Router) {
  }

  rezultat_podaci: any;
  filter_ime: any = "";
  p: number;

  preuzmi_podatke() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Rezultati/GetAll").subscribe(x => {
      this.rezultat_podaci = x;
    });
  }

  preuzmi_utakmice() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Utakmica/Get").subscribe(x => {
      this.utakmica_podaci = x;
    });
  }


  preuzmi_set() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Setovi/GetAll").subscribe(x => {
      this.set_podaci = x;
    });}
  preuzmi_tim() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Tim/GetAll").subscribe(x => {
      this.tim_podaci = x;
    });}

  obrisi(x:any) {
    this.httpKlijent.post(MojConfig.adresa_servera+ "/Rezultati/Delete?id=" + x.rezultatiID, this.odabrani_rezultat)
      .subscribe(x =>{
        this.preuzmi_podatke();
      });
  }
  ngOnInit(): void {

    this.preuzmi_utakmice();

    this.route.params.subscribe(params => {
      this.utakmicaid = +params['utakmicaid']; // (+) converts string 'id' to a number

      this.fetch();
      this.preuzmi_podatke();
      this.preuzmi_tim();
      this.preuzmi_set() ;

    });
  }

  getpodaci() {
    if (this.podaci == null)
      return [];
    return this.podaci.gardovi.filter((x:any)=>x.tim.imeTima.toLowerCase().startsWith(this.filter_ime.toLowerCase()));
  }

  snimi() {
    if (!this.odabrani_rezultat.rezultatiID) {
      this.httpKlijent.post(MojConfig.adresa_servera + "/Rezultati/Add", this.odabrani_rezultat)
        .subscribe(x => {
          this.preuzmi_podatke()
          this.fetch();;
        });
    } else {
      this.httpKlijent.post(MojConfig.adresa_servera + "/Rezultati/Update?id=" + this.odabrani_rezultat.rezultatiID, this.odabrani_rezultat)
        .subscribe(x => {
          this.preuzmi_podatke();
          this.fetch();
        });
    }
  }

  novi_rezultat_dugme() {
    this.odabrani_rezultat = {
      rezultatiID: 0,
      utakmicaID: 0,
      nazivUtakmice: "",
      timID: 0,
      timOpis: "",
      setoviID: 0,
      setOpis: "",
      osvojeniBodovi: 0,
      izgubljeniBodovi: 0,
      obrisan:false
    };
  }
  private fetch() {
    this.httpKlijent.get(MojConfig.adresa_servera+ "/Rezultati?utakmicaid="+this.utakmicaid).subscribe((x:any)=>{
      this.podaci = x
    });
  }

  otvori_detalje(s: any) {
    //
    this.router.navigate(['/putanja-tim-igrac', s.timID]);
  }
  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }
}
