import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Testdirectory } from './testdirectory';

describe('Testdirectory', () => {
  let component: Testdirectory;
  let fixture: ComponentFixture<Testdirectory>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Testdirectory]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Testdirectory);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
