import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Usermaster } from './usermaster';

describe('Usermaster', () => {
  let component: Usermaster;
  let fixture: ComponentFixture<Usermaster>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Usermaster]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Usermaster);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
