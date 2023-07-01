import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {LoginInformacije} from "./_helpers/login-informacije";
import {AutentifikacijaHelper} from "./_helpers/autentifikacija-helper";
import {MojConfig} from "../app/moj-config";

import {TranslateService} from "@ngx-translate/core";

import {ActivatedRoute, Router} from "@angular/router";
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  constructor(private httpKlijent: HttpClient, private route: ActivatedRoute, private router: Router, public translate: TranslateService) {
    translate.addLangs(['Bosanski', 'English','Српски']);
    translate.setDefaultLang('Bosanski');
  }
  switchLanguage(lang: string){
    this.translate.use(lang);
  }
  ngOnInit(): void {

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
