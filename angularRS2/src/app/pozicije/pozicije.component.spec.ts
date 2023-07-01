import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PozicijeComponent } from './pozicije.component';

describe('PozicijeComponent', () => {
  let component: PozicijeComponent;
  let fixture: ComponentFixture<PozicijeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PozicijeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PozicijeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
