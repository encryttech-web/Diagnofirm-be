import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Imageview } from './imageview';

describe('Imageview', () => {
  let component: Imageview;
  let fixture: ComponentFixture<Imageview>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Imageview]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Imageview);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
