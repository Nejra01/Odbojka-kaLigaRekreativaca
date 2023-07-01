import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {ActivatedRoute} from "@angular/router";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";

@Component({
  selector: 'app-tim-igrac',
  templateUrl: './tim-igrac.component.html',
  styleUrls: ['./tim-igrac.component.css']
})
export class TimIgracComponent implements OnInit {
timid:number;
podaci:any;
  odabrani_timigrac: any;
  tim_podaci:any;
  igrac_podaci:any;
  p: number;
  constructor(private httpKlijent: HttpClient, private route: ActivatedRoute) {
  }

  timigrac_podaci: any;
  filter_ime: any = "";


  preuzmi_podatke() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/TimIgrac/GetAll").subscribe(x => {
      this.timigrac_podaci = x;
    });

  }
  preuzmi_tim() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Tim/GetAll").subscribe(x => {
      this.tim_podaci = x;
    });

  }
  preuzmi_igrace() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Igrac/GetAll").subscribe(x => {
      this.igrac_podaci = x;
    });}


  obrisi(x:any) {
    this.httpKlijent.post(MojConfig.adresa_servera+ "/TimIgrac/Delete?id=" + x.timIgracID, this.odabrani_timigrac)
      .subscribe(x =>{
        this.preuzmi_podatke();
        this.fetch();

      });
  }


  ngOnInit(): void {
    //this.preuzmi_podatke();



    this.route.params.subscribe(params => {
      this.timid = +params['timid']; // (+) converts string 'id' to a number
      this.fetch();
      this.preuzmi_tim();
      this.preuzmi_igrace() ;
    });
  }

  getpodaci() {
    if (this.podaci == null)
      return [];
    return this.podaci.gardovi.filter((x: any) => x.igrac.ime.toLowerCase().startsWith(this.filter_ime.toLowerCase()) || x.igrac.prezime.toLowerCase().startsWith(this.filter_ime.toLowerCase()));


  }

  snimi() {
    if (!this.odabrani_timigrac.timIgracID) {
      this.httpKlijent.post(MojConfig.adresa_servera + "/TimIgrac/Add", this.odabrani_timigrac)
        .subscribe(x => {
          this.preuzmi_podatke();
          this.fetch();

        });
    } else {
      this.httpKlijent.post(MojConfig.adresa_servera + "/TimIgrac/Update?id=" + this.odabrani_timigrac.timIgracID, this.odabrani_timigrac)
        .subscribe(x => {
          this.preuzmi_podatke();
          this.fetch();

        });
    }
  }

  novi_timigrac_dugme() {
    this.odabrani_timigrac = {
      timIgracID: 0,
      timID: 0,
      timOpis: "",
      igracID: 0,
      igracOpis: "",
      datumPrijave: "",
      obrisan:false

    };
  }
  private fetch() {
    this.httpKlijent.get(MojConfig.adresa_servera+ "/TimIgrac?timid="+this.timid).subscribe((x:any)=>{
      this.podaci = x
    });
  }

  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }
}
