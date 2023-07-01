import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {ActivatedRoute} from "@angular/router";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";

@Component({
  selector: 'app-dvorane',
  templateUrl: './dvorane.component.html',
  styleUrls: ['./dvorane.component.css']
})
export class DvoraneComponent implements OnInit {
gradid:number;
podaci:any;
  odabrana_dvorana:any;
  grad_podaci:any;
  constructor(private httpKlijent: HttpClient, private route: ActivatedRoute) {
  }


  dvorana_podaci: any;
  dvorane_filter: any="";
  p: number;



  preuzmi_podatke()
  {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Dvorana/Get").subscribe(x=>{
      this.dvorana_podaci = x;
    });

  }
  preuzmi_gradove()
  {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Grad/GetAll").subscribe(x=>{
      this.grad_podaci = x;
    });

  }
  ngOnInit(): void {

    this.route.params.subscribe(params => {
      this.gradid = +params['gradid']; // (+) converts string 'id' to a number

      this.fetch();
      this.preuzmi_podatke();
      this.preuzmi_gradove();
    });
  }

  getpodaci() {
    if (this.podaci==null)
      return [];
    return this.podaci.gardovi.filter((x:any)=>x.imeDvorane.toLowerCase().startsWith(this.dvorane_filter.toLowerCase()));
  }

  snimi() {
    if(!this.odabrana_dvorana.dvoranaID){
    this.httpKlijent.post(MojConfig.adresa_servera+ "/Dvorana/Add", this.odabrana_dvorana)
      .subscribe(x=>{
        this.preuzmi_podatke();
        this.fetch();

      });}
    else {this.httpKlijent.post(MojConfig.adresa_servera+ "/Dvorana/Update?id=" + this.odabrana_dvorana.dvoranaID, this.odabrana_dvorana)
      .subscribe(x=>{
        this.preuzmi_podatke();
        this.fetch();

      });}
  }
  obrisi(x:any) {
    this.httpKlijent.post(MojConfig.adresa_servera+ "/Dvorana/Delete?id=" + x.dvoranaID, this.odabrana_dvorana)
      .subscribe(x =>{
        this.preuzmi_podatke();
        this.fetch();

      });
  }
  dvorana_dugme() {
    this.odabrana_dvorana = {
      dvoranaID:0,
      adresa:"",
      grad:"",
      imeDvorane:"",
      obrisan:false,
      gradID:0

    };
  }
  private fetch() {
    this.httpKlijent.get(MojConfig.adresa_servera+ "/Dvorana?gradid="+this.gradid).subscribe((x:any)=>{
      this.podaci = x
    });
  }
  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

}

