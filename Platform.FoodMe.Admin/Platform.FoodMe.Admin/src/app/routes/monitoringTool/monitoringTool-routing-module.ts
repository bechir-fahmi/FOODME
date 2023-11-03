import { NgModule } from "@angular/core";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { RouterModule, Routes } from "@angular/router";
import { WeCareComponent } from "./we-care/we-care.component";
import { AddNewMonitorComponent } from "./add-new-monitor/add-new-monitor.component";
import { MonitorListComponent } from "./monitor-list/monitor-list.component";
import { CashierComponent } from "./cashier/cashier.component";
import { CustomersComponent } from "./customers/customers.component";

const routes: Routes = [
  { path: 'we-care', component: WeCareComponent },
  { path: 'we-care/add-new-monitor', component: AddNewMonitorComponent },
  { path: 'we-care/monitor-list', component: MonitorListComponent },
  { path: 'cashier', component: CashierComponent },
  { path: 'customers', component: CustomersComponent },


];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  providers: [
    {provide:MatDialogRef , useValue:{} },

    { provide: MAT_DIALOG_DATA, useValue: {} }

 ],
})
export class monitoringToolRoutingModule { }
