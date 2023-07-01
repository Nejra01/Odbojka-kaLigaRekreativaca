import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RezultatiComponent } from './rezultati.component';

describe('RezultatiComponent', () => {
  let component: RezultatiComponent;
  let fixture: ComponentFixture<RezultatiComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RezultatiComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RezultatiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
