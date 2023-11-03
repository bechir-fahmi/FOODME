import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoyalityRoutingModule } from './loyality-routing.module';
import { LoyalityComponent } from './loyality.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from 'app/material.module';
import { TreeViewModule } from '@progress/kendo-angular-treeview';
import { ListViewModule } from '@progress/kendo-angular-listview';

@NgModule({
  declarations: [
    LoyalityComponent
  ],
  imports: [
    CommonModule,
    LoyalityRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    MaterialModule,
    TreeViewModule,
    ListViewModule

  ]
})
export class LoyalityModule { }
