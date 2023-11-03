import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OpenHoursComponent } from './Components/open-hours/open-hours.component';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CalendarsListComponent } from './Components/calendars-list/calendars-list.component';

const routes: Routes = [
  { path: 'open-hours', component: OpenHoursComponent },
  { path: 'calendars-list', component: CalendarsListComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  providers: [
    {provide:MatDialogRef , useValue:{} },

    { provide: MAT_DIALOG_DATA, useValue: {} }

 ],
  exports: [RouterModule]
})
export class BusinessHoursRoutingModule { }
