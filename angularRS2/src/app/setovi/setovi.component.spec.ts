import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SetoviComponent } from './setovi.component';

describe('SetoviComponent', () => {
  let component: SetoviComponent;
  let fixture: ComponentFixture<SetoviComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SetoviComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SetoviComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
