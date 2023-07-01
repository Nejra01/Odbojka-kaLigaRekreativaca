import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-korisnik-uloge',
  templateUrl: './korisnik-uloge.component.html',
  styleUrls: ['./korisnik-uloge.component.css']
})
export class KorisnikUlogeComponent implements OnInit {

  odabrani_korisnikUloge: any;
  korisnik_podaci:any;
  uloge_podaci:any;
  korisnikID: number;
p:number;
  constructor(private httpKlijent: HttpClient, private route: ActivatedRoute) {
  }


  korisnikUloge_podaci: any;
  filter_ime: any = "";


  preuzmi_podatke() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/KorisnikUloga?korisnikid="+this.korisnikID).subscribe(x => {
      this.korisnikUloge_podaci = x;
    });

  }


  preuzmi_uloge() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Uloge/GetAll").subscribe(x => {
      this.uloge_podaci = x;
    });}

  obrisi(x:any) {
    this.httpKlijent.post(MojConfig.adresa_servera+ "/KorisnikUloga/Delete?id=" + x.korisnikUlogaID, this.odabrani_korisnikUloge)
      .subscribe(x =>{
        this.preuzmi_podatke();
      });
  }

  ngOnInit(): void {



    this.route.params.subscribe(params => {
      this.korisnikID = +params['korisnikID'];

      this.preuzmi_podatke();
      this.preuzmi_uloge();

      });
  }

  getpodaci() {
    if (this.korisnikUloge_podaci == null)
      return [];
    return this.korisnikUloge_podaci.gardovi.filter((x:any)=>x.uloga.nazivUloge.toLowerCase().startsWith(this.filter_ime.toLowerCase()));

  }

  snimi() {
    if (!this.odabrani_korisnikUloge.korisnikUlogaID) {
      this.httpKlijent.post(MojConfig.adresa_servera + "/KorisnikUloga/Add", this.odabrani_korisnikUloge)
        .subscribe(x => {
          this.preuzmi_podatke();
        });
    } else {
      this.httpKlijent.post(MojConfig.adresa_servera + "/KorisnikUloga/KorisnikUloga/Update?id=" + this.odabrani_korisnikUloge.korisnikUlogaID, this.odabrani_korisnikUloge)
        .subscribe(x => {
          this.preuzmi_podatke();
        });
    }
  }

  novi_korisnikUloge_dugme() {
    this.odabrani_korisnikUloge = {
      korisnikUlogaID: 0,
      korisnikID: 0,
      korisnikOpis:"",
      ulogaID: 0,
      nazivUloge: "",
      obrisan:false
    };
  }
  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }
}
