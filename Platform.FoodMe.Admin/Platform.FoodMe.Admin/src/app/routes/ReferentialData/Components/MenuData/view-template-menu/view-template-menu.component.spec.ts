import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewTemplateMenuComponent } from './view-template-menu.component';

describe('ViewTemplateMenuComponent', () => {
  let component: ViewTemplateMenuComponent;
  let fixture: ComponentFixture<ViewTemplateMenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewTemplateMenuComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewTemplateMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
