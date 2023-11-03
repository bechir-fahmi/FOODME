import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewExtraModifierGroupComponent } from './view-extra-modifier-group.component';

describe('ViewExtraModifierGroupComponent', () => {
  let component: ViewExtraModifierGroupComponent;
  let fixture: ComponentFixture<ViewExtraModifierGroupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewExtraModifierGroupComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewExtraModifierGroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
