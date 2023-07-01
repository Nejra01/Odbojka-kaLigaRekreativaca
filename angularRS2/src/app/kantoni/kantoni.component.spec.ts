import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KantoniComponent } from './kantoni.component';

describe('KantoniComponent', () => {
  let component: KantoniComponent;
  let fixture: ComponentFixture<KantoniComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ KantoniComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(KantoniComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
