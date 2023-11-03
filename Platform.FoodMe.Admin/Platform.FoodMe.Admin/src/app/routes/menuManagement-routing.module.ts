import { NgModule } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { RouterModule, Routes } from "@angular/router";
import { CreateOrEditExtraModifierGroupComponent } from "./ReferentialData/Components/MenuData/create-or-edit-extra-modifier-group/create-or-edit-extra-modifier-group.component";
import { CreateOrEditMenuCategoryComponent } from "./ReferentialData/Components/MenuData/create-or-edit-menu-category/create-or-edit-menu-category.component";
import { CreateOrEditMenuItemComponent } from "./ReferentialData/Components/MenuData/create-or-edit-menu-item/create-or-edit-menu-item.component";
import { CreateOrEditMenuComponent } from "./ReferentialData/Components/MenuData/create-or-edit-menu/create-or-edit-menu.component";
import { CreateOrEditTemplateMenuComponent } from "./ReferentialData/Components/MenuData/create-or-edit-template-menu/create-or-edit-template-menu.component";
import { ExtraModifierGroupComponent } from "./ReferentialData/Components/MenuData/extra-modifier-group/extra-modifier-group.component";
import { MenuCategoryComponent } from "./ReferentialData/Components/MenuData/menu-category/menu-category.component";
import { MenuItemComponent } from "./ReferentialData/Components/MenuData/menu-item/menu-item.component";
import { MenuComponent } from "./ReferentialData/Components/MenuData/menu/menu.component";
import { TemplateMenuComponent } from "./ReferentialData/Components/MenuData/template-menu/template-menu.component";
import { ViewMenuComponent } from "./ReferentialData/Components/MenuData/view-menu/view-menu.component";
import { ViewTemplateMenuComponent } from "./ReferentialData/Components/MenuData/view-template-menu/view-template-menu.component";
import { ViewMenuCategoryComponent } from "./ReferentialData/Components/MenuData/view-menu-category/view-menu-category.component";
import { ViewMenuItemComponent } from "./ReferentialData/Components/MenuData/view-menu-item/view-menu-item.component";
import { ViewExtraModifierGroupComponent } from "./ReferentialData/Components/MenuData/view-extra-modifier-group/view-extra-modifier-group.component";



const routes: Routes = [
  { path: 'menu', component: MenuComponent },
  { path: 'menu/createMenu', component: CreateOrEditMenuComponent },
  { path: 'menu/viewMenu', component: ViewMenuComponent },
  { path: 'menuTemplate', component: TemplateMenuComponent },
  { path: 'menuTemplate/createMenuTemplate', component: CreateOrEditTemplateMenuComponent },
  { path: 'menuTemplate/viewMenuTemplate', component: ViewTemplateMenuComponent },
  { path: 'menuCategory', component: MenuCategoryComponent },
  { path: 'menuCategory/createMenuCategory', component: CreateOrEditMenuCategoryComponent },
  { path: 'menuCategory/viewMenuCategory', component: ViewMenuCategoryComponent },
  { path: 'menuItem', component: MenuItemComponent },
  { path: 'menuItem/createMenuItem', component: CreateOrEditMenuItemComponent },
  { path: 'menuItem/viewMenuItem', component: ViewMenuItemComponent },
  { path: 'modifierGroup', component: ExtraModifierGroupComponent },
  { path: 'modifierGroup/createModifierGroup', component: CreateOrEditExtraModifierGroupComponent },
  { path: 'modifierGroup/viewModifierGroup', component: ViewExtraModifierGroupComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  providers: [
    {provide:MatDialogRef , useValue:{} },

    { provide: MAT_DIALOG_DATA, useValue: {} }
    
 ],
})
export class MenuManagementRoutingModule { }