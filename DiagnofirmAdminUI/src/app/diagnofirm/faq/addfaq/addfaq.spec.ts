import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Addfaq } from './addfaq';

describe('Addfaq', () => {
  let component: Addfaq;
  let fixture: ComponentFixture<Addfaq>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Addfaq]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Addfaq);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
