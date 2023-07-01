import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TimComponent } from './tim.component';

describe('TimComponent', () => {
  let component: TimComponent;
  let fixture: ComponentFixture<TimComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TimComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TimComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
