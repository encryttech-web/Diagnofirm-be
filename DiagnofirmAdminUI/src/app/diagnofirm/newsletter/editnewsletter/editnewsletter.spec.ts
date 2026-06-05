import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Editnewsletter } from './editnewsletter';

describe('Editnewsletter', () => {
  let component: Editnewsletter;
  let fixture: ComponentFixture<Editnewsletter>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Editnewsletter]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Editnewsletter);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
