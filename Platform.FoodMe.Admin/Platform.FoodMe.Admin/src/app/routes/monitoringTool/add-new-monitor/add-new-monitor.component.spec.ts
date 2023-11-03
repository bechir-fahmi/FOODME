import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddNewMonitorComponent } from './add-new-monitor.component';

describe('AddNewMonitorComponent', () => {
  let component: AddNewMonitorComponent;
  let fixture: ComponentFixture<AddNewMonitorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddNewMonitorComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddNewMonitorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
