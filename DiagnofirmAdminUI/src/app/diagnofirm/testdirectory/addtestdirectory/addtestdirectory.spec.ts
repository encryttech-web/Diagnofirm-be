import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Addtestdirectory } from './addtestdirectory';

describe('Addtestdirectory', () => {
  let component: Addtestdirectory;
  let fixture: ComponentFixture<Addtestdirectory>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Addtestdirectory]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Addtestdirectory);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
