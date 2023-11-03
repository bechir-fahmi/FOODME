import { NgModule } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { RouterModule, Routes } from "@angular/router";
import { CreateOrEditDeliveryOfferComponent } from "./ReferentialData/Components/Promotion/create-or-edit-delivery-offer/create-or-edit-delivery-offer.component";
import { CreateOrEditDiscountReductionComponent } from "./ReferentialData/Components/Promotion/create-or-edit-discount-reduction/create-or-edit-discount-reduction.component";
import { DeliveryOfferComponent } from "./ReferentialData/Components/Promotion/delivery-offer/delivery-offer.component";
import { DiscountReductionComponent } from "./ReferentialData/Components/Promotion/discount-reduction/discount-reduction.component";
import { PromoBogoComponent } from "./ReferentialData/Components/Promotion/promo-bogo/promo-bogo.component";
import { CreateOrEditPromoBogoComponent } from "./ReferentialData/Components/Promotion/create-or-edit-promo-bogo/create-or-edit-promo-bogo.component";
import { CustomerCouponComponent } from "./ReferentialData/Components/Promotion/customer-coupon/customer-coupon.component";
import { CreateOrEditCustomerCouponComponent } from "./ReferentialData/Components/Promotion/create-or-edit-customer-coupon/create-or-edit-customer-coupon.component";

import { DiscountBogoComponent } from "./ReferentialData/Components/Promotion/discount-bogo/discount-bogo.component";
import { CreateOrEditDiscountBogoComponent } from "./ReferentialData/Components/Promotion/create-or-edit-discount-bogo/create-or-edit-discount-bogo.component";
import {CouponComponent} from "./ReferentialData/Components/Promotion/coupon/coupon.component";

const routes: Routes = [
    { path: 'deliveryOffer', component: DeliveryOfferComponent },
    { path: 'deliveryOffer/createDeliveryOffer', component: CreateOrEditDeliveryOfferComponent },
    { path: 'discountReduction', component: DiscountReductionComponent },
    { path: 'discountReduction/createDiscountReduction', component: CreateOrEditDiscountReductionComponent },
    { path: 'promoBogo', component: PromoBogoComponent },
    { path: 'promoBogo/createPromoBogo', component: CreateOrEditPromoBogoComponent },
    { path: 'customerCoupon', component: CouponComponent },
    { path: 'customerCoupon/createCustomerCoupon', component: CreateOrEditCustomerCouponComponent },
    { path: 'customerCoupon/update/:id', component: CreateOrEditCustomerCouponComponent },
    { path: 'discountBogo', component: DiscountBogoComponent },
    { path: 'discountBogo/createDiscountBogo', component: CreateOrEditDiscountBogoComponent },
  ];

  @NgModule({
    imports: [RouterModule.forChild(routes)],
    providers: [
      {provide:MatDialogRef , useValue:{} },

      { provide: MAT_DIALOG_DATA, useValue: {} }

   ],
  })
  export class PromotionRoutingModule { }
