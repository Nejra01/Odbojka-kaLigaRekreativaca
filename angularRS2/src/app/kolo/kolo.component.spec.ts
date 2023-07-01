import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KoloComponent } from './kolo.component';

describe('KoloComponent', () => {
  let component: KoloComponent;
  let fixture: ComponentFixture<KoloComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ KoloComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(KoloComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
