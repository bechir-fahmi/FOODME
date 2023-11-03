import { NgModule } from "@angular/core";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { RouterModule, Routes } from "@angular/router";
import { OrderActionReasonComponent } from "./ReferentialData/Components/OrderManagement/order-action-reason/order-action-reason.component";
import { CreateOrEditOrderActionReasonComponent } from "./ReferentialData/Components/OrderManagement/create-or-edit-order-action-reason/create-or-edit-order-action-reason.component";
import {
  OrderCancellationReasonComponent
} from "./ReferentialData/Components/OrderManagement/order-cancellation-reason/order-cancellation-reason.component";
import {
  OrderRefundsReasonComponent
} from "./ReferentialData/Components/OrderManagement/order-refunds-reason/order-refunds-reason.component";
import {
  EditCancellationReasonComponent
} from "./ReferentialData/Components/OrderManagement/order-cancellation-reason/edit-cancellation-reason/edit-cancellation-reason.component";
import {
  EditOrderRefundComponent
} from "./ReferentialData/Components/OrderManagement/order-refunds-reason/edit-order-refund/edit-order-refund.component";









const routes: Routes = [
    { path: 'order-action-reason', component: OrderActionReasonComponent },
   { path: 'order-cancellation-reason', component: OrderCancellationReasonComponent },
   { path: 'order-cancellation-reason/edit-cancellation-reason', component: EditCancellationReasonComponent },
   { path: 'order-cancellation-reason/update/:id', component: EditCancellationReasonComponent },
   { path: 'order-cancellation-reason/view/:id', component: EditCancellationReasonComponent },
   { path: 'order-refunds-reason', component: OrderRefundsReasonComponent },
  { path: 'order-refunds-reason/edit-order-refund', component: EditOrderRefundComponent },
  { path: 'order-refunds-reason/update/:id', component: EditOrderRefundComponent },
  { path: 'order-refunds-reason/view/:id', component: EditOrderRefundComponent },
  { path: 'order-action-reason/create-or-edit-order-action-reason', component: CreateOrEditOrderActionReasonComponent },




];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  providers: [
    {provide:MatDialogRef , useValue:{} },

    { provide: MAT_DIALOG_DATA, useValue: {} }

 ],
})
export class orderManagementRoutingModule { }
