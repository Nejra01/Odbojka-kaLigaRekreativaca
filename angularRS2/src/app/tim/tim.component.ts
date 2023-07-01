import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {ActivatedRoute, Router} from "@angular/router";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";


import jsPDF from 'jspdf';
import html2canvas from 'html2canvas';


@Component({
  selector: 'app-tim',
  templateUrl: './tim.component.html',
  styleUrls: ['./tim.component.css']
})
export class TimComponent implements OnInit {

  odabrani_tim:any;

  constructor(private httpKlijent: HttpClient, private router: Router) {
  }


  tim_podaci: any;
  filter_ime: any="";
  p: number = 1;


  preuzmi_podatke()
  {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Tim/GetAll").subscribe(x=>{
      this.tim_podaci = x;
    });

  }

  ngOnInit(): void {
    this.preuzmi_podatke();

  }

  getpodaci() {
    if (this.tim_podaci==null)
      return [];
    return this.tim_podaci.filter((x:any)=>x.imeTima.toLowerCase().startsWith(this.filter_ime.toLowerCase()));
  }

  snimi() {
    this.httpKlijent.post(MojConfig.adresa_servera+ "/Tim/Update?id=" + this.odabrani_tim.timID, this.odabrani_tim)
      .subscribe((povratnaVrijednost:any) =>{
        alert("uredu..." + povratnaVrijednost.imeTima);
      });
  }

  obrisi(x:any) {
    this.httpKlijent.post(MojConfig.adresa_servera+ "/Tim/Delete?id=" + x.timID, this.odabrani_tim)
      .subscribe(x =>{
        this.preuzmi_podatke();
      });
  }


  novi_tim_dugme() {
    this.odabrani_tim = {
      id:0,
      imeTima:"",
      obrisan:false

    };
  }
  snimi_dugme() {
    if(!this.odabrani_tim.timID) {
      this.httpKlijent.post(`${MojConfig.adresa_servera}/Tim/Add`, this.odabrani_tim).subscribe(x=>{
        this.preuzmi_podatke();
      });
    }
    else{
      this.httpKlijent.post(`${MojConfig.adresa_servera}/Tim/Update/?id=`+this.odabrani_tim.timID, this.odabrani_tim).subscribe(x=>{
        this.preuzmi_podatke();
      });
    }
  }
  otvori_detalje(x: any) {
    this.router.navigate(['/putanja-tim-igrac', x.timID]);
  }
  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }


  public openPDF(): void {
    let DATA: any = document.getElementById('htmlData');
    html2canvas(DATA).then((canvas) => {
      let fileWidth = 208;
      let fileHeight = (canvas.height * fileWidth) / canvas.width;
      const FILEURI = canvas.toDataURL('image/png');
      let PDF = new jsPDF('p', 'mm', 'a4');
      let position = 0;
      PDF.addImage(FILEURI, 'PNG', 0, position, fileWidth, fileHeight);
      PDF.save('angular-demo.pdf');
    });
  }



}


