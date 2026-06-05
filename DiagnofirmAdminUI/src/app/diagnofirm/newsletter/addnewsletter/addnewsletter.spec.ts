import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Addnewsletter } from './addnewsletter';

describe('Addnewsletter', () => {
  let component: Addnewsletter;
  let fixture: ComponentFixture<Addnewsletter>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Addnewsletter]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Addnewsletter);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
