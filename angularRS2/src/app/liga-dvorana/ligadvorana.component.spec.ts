import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LigaDvoranaComponent } from './ligadvorana.component';

describe('LigadvoranaComponent', () => {
  let component: LigaDvoranaComponent;
  let fixture: ComponentFixture<LigaDvoranaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LigaDvoranaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LigaDvoranaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
