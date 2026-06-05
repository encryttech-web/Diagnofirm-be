import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Imageupload } from './imageupload';

describe('Imageupload', () => {
  let component: Imageupload;
  let fixture: ComponentFixture<Imageupload>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Imageupload]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Imageupload);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
