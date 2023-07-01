import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SpolComponent } from './spol.component';

describe('SpolComponent', () => {
  let component: SpolComponent;
  let fixture: ComponentFixture<SpolComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SpolComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SpolComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
