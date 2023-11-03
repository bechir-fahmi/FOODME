import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrderRefundsReasonComponent } from './order-refunds-reason.component';

describe('OrderRefundsReasonComponent', () => {
  let component: OrderRefundsReasonComponent;
  let fixture: ComponentFixture<OrderRefundsReasonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OrderRefundsReasonComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OrderRefundsReasonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
