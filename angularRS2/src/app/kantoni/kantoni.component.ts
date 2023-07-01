import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {Router} from "@angular/router";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";

@Component({
  selector: 'app-kantoni',
  templateUrl: './kantoni.component.html',
  styleUrls: ['./kantoni.component.css']
})
export class KantoniComponent implements OnInit {

  odabrani_kanton:any;

  constructor(private httpKlijent: HttpClient, private router: Router) {
  }


  kanton_podaci: any;
  filter_ime: any="";
  novi_kanton: any;


  preuzmi_podatke()
  {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Update/Get").subscribe(x=>{
      this.kanton_podaci = x;
    });

  }

  ngOnInit(): void {
    this.preuzmi_podatke();

  }

  getpodaci() {
    if (this.kanton_podaci==null)
      return [];
    return this.kanton_podaci.filter((x:any)=>x.nazivKantona.toLowerCase().startsWith(this.filter_ime.toLowerCase()));
  }

 /* snimi() {
    this.httpKlijent.post(MojConfig.adresa_servera+ "/Kanton/Update?id=" + this.odabrani_kanton.kantonID, this.odabrani_kanton)
      .subscribe((povratnaVrijednost:any) =>{
        alert("uredu..." + povratnaVrijednost.nazivKantona);
        this.getpodaci();
      });
  }*/
  p: number;

  obrisi(x:any) {
    this.httpKlijent.post(MojConfig.adresa_servera+ "/Kanton/Delete?id=" + x.kantonID, this.odabrani_kanton)
      .subscribe(x =>{
        this.preuzmi_podatke();
      });
  }


  novi_kanton_dugme() {
    this.odabrani_kanton = {
      kantonID:0,
      nazivKantona:"",
      obrisan:false

    };
  }
  snimi_dugme() {
    if(!this.odabrani_kanton.kantonID) {
    this.httpKlijent.post(`${MojConfig.adresa_servera}/Kanton/Add`, this.odabrani_kanton).subscribe(x=>{
      this.preuzmi_podatke();
    });
    }
    else{
      this.httpKlijent.post(`${MojConfig.adresa_servera}/Kanton/Update/?id=`+this.odabrani_kanton.kantonID, this.odabrani_kanton).subscribe(x=>{
        this.preuzmi_podatke();
      });
  }
  }
  otvori_detalje(s: any) {
    //
    this.router.navigate(['/putanja-gradovi', s.kantonID]);
  }

  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }
  logoutButton() {
    AutentifikacijaHelper.setLoginInfo(null);

    this.httpKlijent.post(MojConfig.adresa_servera + "/Autentifikacija/Logout/", null, MojConfig.http_opcije())
      .subscribe((x: any) => {
        this.router.navigateByUrl("/login");
        // porukaSuccess("Logout uspješan");
      });
  }
}


