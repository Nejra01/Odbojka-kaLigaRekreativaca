import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TimIgracComponent } from './tim-igrac.component';

describe('TimIgracComponent', () => {
  let component: TimIgracComponent;
  let fixture: ComponentFixture<TimIgracComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TimIgracComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TimIgracComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
