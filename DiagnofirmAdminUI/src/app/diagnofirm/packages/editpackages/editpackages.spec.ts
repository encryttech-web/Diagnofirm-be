import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Editpackages } from './editpackages';

describe('Editpackages', () => {
  let component: Editpackages;
  let fixture: ComponentFixture<Editpackages>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Editpackages]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Editpackages);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
