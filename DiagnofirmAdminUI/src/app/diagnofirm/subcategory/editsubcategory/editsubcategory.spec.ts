import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Editsubcategory } from './editsubcategory';

describe('Editsubcategory', () => {
  let component: Editsubcategory;
  let fixture: ComponentFixture<Editsubcategory>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Editsubcategory]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Editsubcategory);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
