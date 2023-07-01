import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {ActivatedRoute, Router} from "@angular/router";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";

@Component({
  selector: 'app-kolo',
  templateUrl: './kolo.component.html',
  styleUrls: ['./kolo.component.css']
})
export class KoloComponent implements OnInit {

  odabrano_kolo:any;
  studentid:number;
  podaci:any;
  constructor(private httpKlijent: HttpClient, private route: ActivatedRoute,  private router: Router) {
  }
  kolo_podaci: any;
liga_podaci:any;
niz:any=[1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25];
  p: number;
  preuzmi_podatke()
  {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Kolo/GetAll").subscribe(x=>{
      this.kolo_podaci = x;
    });

  }
  preuzmi_lige()
  {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Liga/GetAll").subscribe(x=>{
      this.liga_podaci = x;
    });

  }
  ngOnInit(): void {
    //this.preuzmi_podatke();
  //this.preuzmi_lige();
    this.route.params.subscribe(params => {
      this.studentid = +params['ligaid']; // (+) converts string 'id' to a number

      //fetch detalji o studentu
      //-- upisani semestri
      //-- ocjene, uplate itd.
      //class UpisAkademskaGodina
      //studentid, akademskaGodinaid, godina_studija, cijena_skolarine, bool obnova, datum_upisazimski

      this.fetch();
      this.preuzmi_lige();
      this.preuzmi_podatke();
    });
  }

  getpodaci() {
    if (this.podaci==null)
      return [];
    return this.podaci.kola;
  }

  obrisi(x:any) {
    this.httpKlijent.post(MojConfig.adresa_servera+ "/Kolo/Delete?id=" + x.koloID, this.odabrano_kolo)
      .subscribe(x =>{
        this.preuzmi_podatke();
        this.fetch();
      });
  }

  snimi() {
    this.httpKlijent.post(MojConfig.adresa_servera+ "/Kolo/Update?id=" + this.odabrano_kolo.koloID, this.odabrano_kolo)
      .subscribe((povratnaVrijednost:any) =>{
        alert("uredu..." + povratnaVrijednost.nazivSpola);
      });
  }



  novo_kolo_dugme() {
    this.odabrano_kolo = {
      koloID:0,
      brojKola:0,
      ligaID:0,
      ligaOpis:"",
      obrisan:false

    };
  }
  snimi_dugme() {
    if(!this.odabrano_kolo.koloID) {
      this.httpKlijent.post(`${MojConfig.adresa_servera}/Kolo/Add`, this.odabrano_kolo).subscribe(x=>{
        this.preuzmi_podatke();
        this.fetch();

      });
    }
    else{
      this.httpKlijent.post(`${MojConfig.adresa_servera}/Kolo/Update/?id=`+this.odabrano_kolo.koloID, this.odabrano_kolo).subscribe(x=>{
        this.preuzmi_podatke();
        this.fetch();
      });
    }
  }

  private fetch() {
    this.httpKlijent.get(MojConfig.adresa_servera+ "/Kolo?ligaID="+this.studentid).subscribe((x:any)=>{
      this.podaci = x

    });
  }


  otvori_maticnuknjigu(x: any) {
    this.router.navigate(['/putanja-utakmica', x.koloID]);
  }
  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }
}


