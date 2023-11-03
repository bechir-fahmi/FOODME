import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrderCancellationReasonComponent } from './order-cancellation-reason.component';

describe('OrderCancellationReasonComponent', () => {
  let component: OrderCancellationReasonComponent;
  let fixture: ComponentFixture<OrderCancellationReasonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OrderCancellationReasonComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OrderCancellationReasonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
