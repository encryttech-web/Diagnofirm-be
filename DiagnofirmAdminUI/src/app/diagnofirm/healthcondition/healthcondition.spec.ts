import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Healthcondition } from './healthcondition';

describe('Healthcondition', () => {
  let component: Healthcondition;
  let fixture: ComponentFixture<Healthcondition>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Healthcondition]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Healthcondition);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
