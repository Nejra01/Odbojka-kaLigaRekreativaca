import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UtakmicaKorisnikComponent } from './utakmica-korisnik.component';

describe('UtakmicaKorisnikComponent', () => {
  let component: UtakmicaKorisnikComponent;
  let fixture: ComponentFixture<UtakmicaKorisnikComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UtakmicaKorisnikComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UtakmicaKorisnikComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
