import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Addfeedback } from './addfeedback';

describe('Addfeedback', () => {
  let component: Addfeedback;
  let fixture: ComponentFixture<Addfeedback>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Addfeedback]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Addfeedback);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
