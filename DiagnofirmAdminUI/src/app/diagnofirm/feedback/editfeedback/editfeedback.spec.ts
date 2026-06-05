import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Editfeedback } from './editfeedback';

describe('Editfeedback', () => {
  let component: Editfeedback;
  let fixture: ComponentFixture<Editfeedback>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Editfeedback]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Editfeedback);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
