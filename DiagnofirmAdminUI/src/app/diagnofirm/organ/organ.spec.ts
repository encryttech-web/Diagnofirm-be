import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Organ } from './organ';

describe('Organ', () => {
  let component: Organ;
  let fixture: ComponentFixture<Organ>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Organ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Organ);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
