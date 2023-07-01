import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import {FormsModule} from "@angular/forms";
//import {HttpClientModule} from "@angular/common/http";
import {RouterModule, RouterOutlet} from "@angular/router";
import { GradoviComponent } from './gradovi/gradovi.component';
import { DvoraneComponent } from './dvorane/dvorane.component';
import { IgraciComponent } from './igraci/igraci.component';
import { KorisniciComponent } from './korisnici/korisnici.component';
import { KantoniComponent } from './kantoni/kantoni.component';
import { SpolComponent } from './spol/spol.component';
import { KoloComponent } from './kolo/kolo.component';
import { LigaComponent } from './liga/liga.component';
import { RezultatiComponent } from './rezultati/rezultati.component';
import { SetoviComponent } from './setovi/setovi.component';
import { StatusComponent } from './status/status.component';
import { PozicijeComponent } from './pozicije/pozicije.component';
import { TimComponent } from './tim/tim.component';
import { UlogeComponent } from './uloge/uloge.component';
import { KorisnikUlogeComponent } from './korisnik-uloge/korisnik-uloge.component';
import { LigaDvoranaComponent } from './liga-dvorana/ligadvorana.component';
import { TimIgracComponent } from './tim-igrac/tim-igrac.component';
import { TimLigaComponent } from './tim-liga/tim-liga.component';
import { UtakmicaComponent } from './utakmica/utakmica.component';
import { UtakmicaKorisnikComponent } from './utakmica-korisnik/utakmica-korisnik.component';
import { UtakmicaTimligaComponent } from './utakmica-timliga/utakmica-timliga.component';
import { UtakmicaTimligaigracComponent } from './utakmica-timligaigrac/utakmica-timligaigrac.component';
import { LoginComponent } from './login/login.component';
import { TwoFOtkljucajComponent } from './two-f-otkljucaj/two-f-otkljucaj.component';
import {NgxPaginationModule} from 'ngx-pagination';
import {AutorizacijaLoginProvjera} from './_guards/autorizacija-login-provjera.service';
import { UserNotActiveComponent } from "./user-not-active/user-not-active.component";

import {TranslateLoader, TranslateModule} from "@ngx-translate/core";
import {TranslateHttpLoader} from "@ngx-translate/http-loader";
import {HttpClient, HttpClientModule} from "@angular/common/http";


@NgModule({
  declarations: [
    AppComponent,
    GradoviComponent,
    DvoraneComponent,
    IgraciComponent,
    KorisniciComponent,
    KantoniComponent,
    SpolComponent,
    KoloComponent,
    LigaComponent,
    RezultatiComponent,
    SetoviComponent,
    UlogeComponent,
    TimComponent,
    PozicijeComponent,
    StatusComponent,
    KorisnikUlogeComponent,
    LigaDvoranaComponent,
    TimIgracComponent,
    TimLigaComponent,
    UtakmicaComponent,
    UtakmicaKorisnikComponent,
    UtakmicaTimligaComponent,
    UtakmicaTimligaigracComponent,
    LoginComponent,
    TwoFOtkljucajComponent,
    UserNotActiveComponent


  ],
  imports: [
    BrowserModule,
    FormsModule,
    NgxPaginationModule,
    HttpClientModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: httpTranslateLoader,
        deps: [HttpClient]
      }
    }),
    RouterModule.forRoot([
      {path: 'user-not-active', component: UserNotActiveComponent},
      {path: 'putanja-kantoni', component: KantoniComponent},
      {path: 'putanja-gradovi/:kantonid', component: GradoviComponent},
      {path: 'putanja-dvorane/:gradid', component: DvoraneComponent},
      {path: 'putanja-igraci', component: IgraciComponent},
      {path: 'putanja-korisnici', component: KorisniciComponent},
      {path: 'putanja-spol', component: SpolComponent},
      {path: 'putanja-kolo/:ligaid', component: KoloComponent},
      {path: 'putanja-liga', component: LigaComponent},
      {path: 'putanja-rezultati/:utakmicaid', component: RezultatiComponent},
      {path: 'putanja-setovi', component: SetoviComponent},
      {path: 'putanja-status', component: StatusComponent},
      {path: 'putanja-uloge', component: UlogeComponent},
      {path: 'putanja-korisnik-uloge/:korisnikID', component: KorisnikUlogeComponent},
      {path: 'putanja-pozicije', component: PozicijeComponent},
      {path: 'putanja-liga-dvorana/:dvoranaid', component: LigaDvoranaComponent},
      {path: 'putanja-tim', component: TimComponent},
      {path: 'putanja-tim-igrac/:timid', component: TimIgracComponent},
      {path: 'putanja-tim-liga', component: TimLigaComponent},
      {path: 'putanja-utakmica/:koloid', component: UtakmicaComponent},
      {path: 'putanja-utakmica-korisnik/:utakmicaid', component: UtakmicaKorisnikComponent},
      {path: 'putanja-utakmica-timliga', component: UtakmicaTimligaComponent},
      {path: 'putanja-utakmica-timligaigrac', component: UtakmicaTimligaigracComponent},
      {path: 'putanja-login', component:     LoginComponent},
      {path: 'two-f-otkljucaj', component: TwoFOtkljucajComponent},


    ]),
    FormsModule,
    HttpClientModule,
  ],
  providers: [  AutorizacijaLoginProvjera,],
  bootstrap: [AppComponent]
})
export class AppModule { }

export function httpTranslateLoader (http: HttpClient){
  return new TranslateHttpLoader(http);
}
