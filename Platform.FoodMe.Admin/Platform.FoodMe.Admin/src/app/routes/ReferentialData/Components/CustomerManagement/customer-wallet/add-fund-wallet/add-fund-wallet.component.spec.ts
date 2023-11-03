import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddFundWalletComponent } from './add-fund-wallet.component';

describe('AddFundWalletComponent', () => {
  let component: AddFundWalletComponent;
  let fixture: ComponentFixture<AddFundWalletComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddFundWalletComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddFundWalletComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
