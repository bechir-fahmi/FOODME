import { MaterialModule } from 'app/material.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BusinessHoursRoutingModule } from './business-hours-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { OpenHoursComponent } from './Components/open-hours/open-hours.component';
import { CalendarsListComponent } from './Components/calendars-list/calendars-list.component';
import { NgbCollapseModule } from '@ng-bootstrap/ng-bootstrap';
import { TreeViewModule } from '@progress/kendo-angular-treeview';
import { ListViewModule } from '@progress/kendo-angular-listview';
import { NgSelectModule } from '@ng-select/ng-select';


@NgModule({
  declarations: [
OpenHoursComponent,
CalendarsListComponent
  ],
  imports: [
    CommonModule,
    FormsModule ,
    BusinessHoursRoutingModule,
    ReactiveFormsModule,
    MaterialModule,
    NgbCollapseModule,
    TreeViewModule,
    ListViewModule,
    NgSelectModule
  ]
})
export class BusinessHoursModule { }
