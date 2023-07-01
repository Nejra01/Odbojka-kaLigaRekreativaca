import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {ActivatedRoute} from "@angular/router";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";

@Component({
  selector: 'app-liga-dvorana',
  templateUrl: './ligadvorana.component.html',
  styleUrls: ['./ligadvorana.component.css']
})
export class LigaDvoranaComponent implements OnInit {
ligaid:number;
podaci:any;
  odabrani_ligadvorana: any;
  liga_podaci:any;
  dvorana_podaci:any;
  constructor(private httpKlijent: HttpClient, private route: ActivatedRoute) {
  }


  ligadvorana_podaci: any;
  filter_ime: any = "";
  filter_grad: any = "";
  filter_kanton: any = "";
  p: number;


  preuzmi_podatke() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/LigaDvorana?ligaid="+this.ligaid).subscribe(x => {
      this.ligadvorana_podaci = x;
    });

  }
  preuzmi_ligu() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Liga/GetAll").subscribe(x => {
      this.liga_podaci = x;
    });

  }
  preuzmi_dvoranu() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Dvorana/Get").subscribe(x => {
      this.dvorana_podaci = x;
    });}

  obrisi(x:any) {
    this.httpKlijent.post(MojConfig.adresa_servera+ "/LigaDvorana/Delete?id=" + x.ligaDvoranaID, this.odabrani_ligadvorana)
      .subscribe(x =>{
        this.fetch();
      });
  }

  ngOnInit(): void {
    //this.preuzmi_podatke();


    this.route.params.subscribe(params => {
      this.ligaid = +params['dvoranaid']; // (+) converts string 'id' to a number

      this.fetch();
      this.preuzmi_ligu();
      this.preuzmi_dvoranu() ;
    });
  }

  getpodaci() {
    if (this.podaci == null)
      return [];
    return this.podaci.gardovi.filter((x:any)=>x.dvorana.imeDvorane.toLowerCase().startsWith(this.filter_ime.toLowerCase())
      && x.dvorana.grad.imeGrada.toLowerCase().startsWith(this.filter_grad.toLowerCase())
      && x.dvorana.grad.kanton.nazivKantona.toLowerCase().startsWith(this.filter_kanton.toLowerCase()));
  }

  snimi() {
    if (!this.odabrani_ligadvorana.ligaDvoranaID) {
      this.httpKlijent.post(MojConfig.adresa_servera + "/LigaDvorana/Add", this.odabrani_ligadvorana)
        .subscribe(x => {
          this.fetch();

        });
    } else {
      this.httpKlijent.post(MojConfig.adresa_servera + "/LigaDvorana/Update?id=" + this.odabrani_ligadvorana.ligaDvoranaID, this.odabrani_ligadvorana)
        .subscribe(x => {
          this.fetch();
        });
    }
  }

  novi_rezultat_dugme() {
    this.odabrani_ligadvorana = {
      ligaDvoranaID: 0,
      ligaID: 0,
      godinaLige: "",
      dvoranaID: 0,
      imeDvorane: "",
      obrisan:false
    };
  }
   fetch() {
    this.httpKlijent.get(MojConfig.adresa_servera+ "/LigaDvorana?ligaid="+this.ligaid).subscribe((x:any)=>{
      this.podaci = x
    });
  }
  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }
}
