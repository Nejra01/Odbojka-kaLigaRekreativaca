import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {ActivatedRoute, Router} from "@angular/router";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";

@Component({
  selector: 'app-gradovi',
  templateUrl: './gradovi.component.html',
  styleUrls: ['./gradovi.component.css']
})
export class GradoviComponent implements OnInit {
kantonid:number;
podaci:any;
  odabrani_grad:any;
  kanton_podaci:any;
  constructor(private httpKlijent: HttpClient, private route: ActivatedRoute, private router: Router) {
  }


  grad_podaci: any;
  grad_filter: any="";
  p: number;



  preuzmi_podatke()
  {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Grad/GetAll").subscribe(x=>{
      this.grad_podaci = x;
    });

  }

  fetchOpstine() :void
  {
    this.httpKlijent.get(MojConfig.adresa_servera+ "/Update/Get").subscribe(x=>{
      this.kanton_podaci = x;
    });
  }


  ngOnInit(): void {

    this.route.params.subscribe(params => {
      this.kantonid = +params['kantonid']; // (+) converts string 'id' to a number
      this.fetch();
     // this.preuzmi_podatke();
      this.fetchOpstine();
    });
  }

  getpodaci() {
    if (this.podaci==null)
      return [];
    return this.podaci.gardovi.filter((x:any)=>x.imeGrada.toLowerCase().startsWith(this.grad_filter.toLowerCase()));
  }
  novi_grad_dugme() {
    this.odabrani_grad = {
      id:0,
      nazivGrada:"",
      kantonID:0,
      nazivKantona:"",
      obrisan:false


    };
  }
  obrisi(x:any) {
    this.httpKlijent.post(MojConfig.adresa_servera+ "/Grad/Delete?id=" + x.gradID, this.odabrani_grad)
      .subscribe(x =>{
        this.preuzmi_podatke();
        this.fetch();

      });
  }
  snimi() {
    if(!this.odabrani_grad.gradID){
    this.httpKlijent.post(MojConfig.adresa_servera+ "/Grad/Add", this.odabrani_grad)
      .subscribe(x=>{
        this.preuzmi_podatke();
        this.fetch();
      });

    }
    else{
      this.httpKlijent.post(MojConfig.adresa_servera+ "/Grad/Update?id=" + this.odabrani_grad.gradID, this.odabrani_grad)
        .subscribe(x=>{
          this.preuzmi_podatke();
          this.fetch();
        });
    }

    }

  private fetch() {
    this.httpKlijent.get(MojConfig.adresa_servera+ "/Grad?kantonid="+this.kantonid).subscribe((x:any)=>{
      this.podaci = x
    });
  }
  otvori_detalje(s: any) {
    //
    this.router.navigate(['/putanja-dvorane', s.gradID]);
  }

  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

}

