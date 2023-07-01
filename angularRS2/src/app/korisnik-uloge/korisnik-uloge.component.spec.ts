import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KorisnikUlogeComponent } from './korisnik-uloge.component';

describe('KorisnikUlogeComponent', () => {
  let component: KorisnikUlogeComponent;
  let fixture: ComponentFixture<KorisnikUlogeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ KorisnikUlogeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(KorisnikUlogeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
