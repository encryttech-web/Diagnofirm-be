import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Addsubcategory } from './addsubcategory';

describe('Addsubcategory', () => {
  let component: Addsubcategory;
  let fixture: ComponentFixture<Addsubcategory>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Addsubcategory]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Addsubcategory);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
