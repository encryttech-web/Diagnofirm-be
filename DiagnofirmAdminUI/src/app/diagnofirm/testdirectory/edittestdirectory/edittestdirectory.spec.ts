import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Edittestdirectory } from './edittestdirectory';

describe('Edittestdirectory', () => {
  let component: Edittestdirectory;
  let fixture: ComponentFixture<Edittestdirectory>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Edittestdirectory]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Edittestdirectory);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
