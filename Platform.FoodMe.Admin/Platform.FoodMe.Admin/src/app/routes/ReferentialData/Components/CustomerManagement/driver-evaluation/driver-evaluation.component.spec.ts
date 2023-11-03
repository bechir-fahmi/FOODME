import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DriverEvaluationComponent } from './driver-evaluation.component';

describe('DriverEvaluationComponent', () => {
  let component: DriverEvaluationComponent;
  let fixture: ComponentFixture<DriverEvaluationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DriverEvaluationComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DriverEvaluationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
