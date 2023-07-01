import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UtakmicaTimligaComponent } from './utakmica-timliga.component';

describe('UtakmicaTimligaComponent', () => {
  let component: UtakmicaTimligaComponent;
  let fixture: ComponentFixture<UtakmicaTimligaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UtakmicaTimligaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UtakmicaTimligaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
