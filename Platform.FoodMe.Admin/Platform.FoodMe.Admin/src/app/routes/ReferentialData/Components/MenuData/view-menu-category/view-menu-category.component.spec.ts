import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewMenuCategoryComponent } from './view-menu-category.component';

describe('ViewMenuCategoryComponent', () => {
  let component: ViewMenuCategoryComponent;
  let fixture: ComponentFixture<ViewMenuCategoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewMenuCategoryComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewMenuCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
