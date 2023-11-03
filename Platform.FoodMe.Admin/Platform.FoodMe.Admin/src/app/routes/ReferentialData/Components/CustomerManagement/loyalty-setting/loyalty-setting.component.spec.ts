import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoyaltySettingComponent } from './loyalty-setting.component';

describe('LoyaltySettingComponent', () => {
  let component: LoyaltySettingComponent;
  let fixture: ComponentFixture<LoyaltySettingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LoyaltySettingComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LoyaltySettingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
