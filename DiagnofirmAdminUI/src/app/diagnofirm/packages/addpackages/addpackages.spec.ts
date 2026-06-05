import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Addpackages } from './addpackages';

describe('Addpackages', () => {
  let component: Addpackages;
  let fixture: ComponentFixture<Addpackages>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Addpackages]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Addpackages);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
