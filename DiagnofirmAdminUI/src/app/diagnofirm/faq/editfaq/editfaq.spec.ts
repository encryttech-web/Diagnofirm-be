import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Editfaq } from './editfaq';

describe('Editfaq', () => {
  let component: Editfaq;
  let fixture: ComponentFixture<Editfaq>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Editfaq]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Editfaq);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
