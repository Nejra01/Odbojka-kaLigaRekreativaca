import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TimLigaComponent } from './tim-liga.component';

describe('TimLigaComponent', () => {
  let component: TimLigaComponent;
  let fixture: ComponentFixture<TimLigaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TimLigaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TimLigaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
