import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisrationComponent } from './regisration.component';

describe('RegisrationComponent', () => {
  let component: RegisrationComponent;
  let fixture: ComponentFixture<RegisrationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RegisrationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RegisrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
