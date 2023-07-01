import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IgraciComponent } from './igraci.component';

describe('IgraciComponent', () => {
  let component: IgraciComponent;
  let fixture: ComponentFixture<IgraciComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IgraciComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IgraciComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
