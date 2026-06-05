import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Diagnofirm } from './diagnofirm';

describe('Diagnofirm', () => {
  let component: Diagnofirm;
  let fixture: ComponentFixture<Diagnofirm>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Diagnofirm]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Diagnofirm);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
