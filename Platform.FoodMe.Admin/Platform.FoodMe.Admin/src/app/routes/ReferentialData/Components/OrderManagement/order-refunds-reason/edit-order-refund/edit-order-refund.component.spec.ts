import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditOrderRefundComponent } from './edit-order-refund.component';

describe('EditOrderRefundComponent', () => {
  let component: EditOrderRefundComponent;
  let fixture: ComponentFixture<EditOrderRefundComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditOrderRefundComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditOrderRefundComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
