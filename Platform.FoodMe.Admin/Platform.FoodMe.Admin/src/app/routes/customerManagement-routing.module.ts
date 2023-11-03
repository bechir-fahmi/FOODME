import { NgModule } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { RouterModule, Routes } from "@angular/router";
import { CustomerPointComponent } from "./ReferentialData/Components/CustomerManagement/customer-point/customer-point.component";
import { CreateOrEditCustomerPointComponent } from "./ReferentialData/Components/CustomerManagement/create-or-edit-customer-point/create-or-edit-customer-point.component";
import { CouponComponent } from "./ReferentialData/Components/Promotion/coupon/coupon.component";
import { CreateOrEditCouponComponent } from "./ReferentialData/Components/Promotion/create-or-edit-coupon/create-or-edit-coupon.component";
import { CustomerListComponent } from "./ReferentialData/Components/CustomerManagement/customer-list/customer-list.component";
import { LoyaltySettingComponent } from "./ReferentialData/Components/CustomerManagement/loyalty-setting/loyalty-setting.component";
import { CustomerWalletComponent } from "./ReferentialData/Components/CustomerManagement/customer-wallet/customer-wallet.component";
import {
  RestaurantEvaluationComponent
} from "./ReferentialData/Components/CustomerManagement/restaurant-evaluation/restaurant-evaluation.component";
import {
  DriverEvaluationComponent
} from "./ReferentialData/Components/CustomerManagement/driver-evaluation/driver-evaluation.component";
import {
  CustomerCouponComponent
} from "./ReferentialData/Components/Promotion/customer-coupon/customer-coupon.component";



const routes: Routes = [
  { path: 'customerPoint', component: CustomerPointComponent },
  { path: 'customerPoint/createCustomerPoint', component: CreateOrEditCustomerPointComponent },
  { path: 'coupon', component: CustomerCouponComponent },
  { path: 'coupon/createCoupon', component: CreateOrEditCouponComponent },
  { path : 'loyaltySetting', component: LoyaltySettingComponent },
  { path : 'customerWallet', component: CustomerWalletComponent },
  { path: 'restaurantEvaluation', component: RestaurantEvaluationComponent },
  { path: 'driverEvaluation', component: DriverEvaluationComponent },
  { path : 'customerList', component: CustomerListComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  providers: [
    {provide:MatDialogRef , useValue:{} },

    { provide: MAT_DIALOG_DATA, useValue: {} }

 ],
})
export class CustomerManagementRoutingModule { }
