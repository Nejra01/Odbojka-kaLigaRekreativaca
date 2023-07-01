import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UtakmicaTimligaigracComponent } from './utakmica-timligaigrac.component';

describe('UtakmicaTimligaigracComponent', () => {
  let component: UtakmicaTimligaigracComponent;
  let fixture: ComponentFixture<UtakmicaTimligaigracComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UtakmicaTimligaigracComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UtakmicaTimligaigracComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
