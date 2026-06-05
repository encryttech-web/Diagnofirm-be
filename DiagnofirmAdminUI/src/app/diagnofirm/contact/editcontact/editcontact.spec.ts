import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Editcontact } from './editcontact';

describe('Editcontact', () => {
  let component: Editcontact;
  let fixture: ComponentFixture<Editcontact>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Editcontact]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Editcontact);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
