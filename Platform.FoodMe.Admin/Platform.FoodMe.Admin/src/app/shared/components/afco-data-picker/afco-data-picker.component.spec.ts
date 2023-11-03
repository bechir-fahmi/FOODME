import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AfcoDataPickerComponent } from './afco-data-picker.component';

describe('AfcoDataPickerComponent', () => {
  let component: AfcoDataPickerComponent;
  let fixture: ComponentFixture<AfcoDataPickerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AfcoDataPickerComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AfcoDataPickerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
