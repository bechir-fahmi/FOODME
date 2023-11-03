import { NgModule } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { RouterModule, Routes } from "@angular/router";
import { PaymentMethodComponent } from "./ReferentialData/Components/GlobalSettings/payment-method/payment-method.component";
import { CreateOrEditPaymentMethodComponent } from "./ReferentialData/Components/GlobalSettings/create-or-edit-payment-method/create-or-edit-payment-method.component";
import { DeliveryCompanyComponent } from "./ReferentialData/Components/RestaurantData/DeliveryCompany/delivery-company/delivery-company.component";
import { CreateOrEditDeliveryCompanyComponent } from "./ReferentialData/Components/RestaurantData/DeliveryCompany/create-or-edit-delivery-company/create-or-edit-delivery-company.component";
import { ExternalOperatorComponent } from "./ReferentialData/Components/GlobalSettings/external-operator/external-operator.component";
import { CreateOrEditExternalOperatorComponent } from "./ReferentialData/Components/GlobalSettings/create-or-edit-external-operator/create-or-edit-external-operator.component";




const routes: Routes = [
  { path: 'paymentMethod', component: PaymentMethodComponent },
  { path: 'paymentMethod/createPaymentType', component: CreateOrEditPaymentMethodComponent },
  { path: 'paymentMethod/viewPaymentType/:id', component: CreateOrEditPaymentMethodComponent },
  { path: 'paymentMethod/updatePaymentType/:id', component: CreateOrEditPaymentMethodComponent },
  { path: 'deliveryCompany', component: DeliveryCompanyComponent },
  { path: 'deliveryCompany/createDeliveryCompany', component: CreateOrEditDeliveryCompanyComponent },
  { path: 'deliveryCompany/viewDeliveryCompany/:id', component: CreateOrEditDeliveryCompanyComponent },
  { path: 'deliveryCompany/updateDeliveryCompany/:id', component: CreateOrEditDeliveryCompanyComponent },
  { path: 'externalOperator', component: ExternalOperatorComponent },
  { path: 'externalOperator/createExternalOperator', component: CreateOrEditExternalOperatorComponent },
  { path: 'externalOperator/viewExternalOperator/:id', component: CreateOrEditExternalOperatorComponent },
  { path: 'externalOperator/updateExternalOperator/:id', component: CreateOrEditExternalOperatorComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  providers: [
    {provide:MatDialogRef , useValue:{} },

    { provide: MAT_DIALOG_DATA, useValue: {} }
    
 ],
})
export class GlobalSettingsRoutingModule { }