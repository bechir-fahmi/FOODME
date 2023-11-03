import { NgModule } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { RouterModule, Routes } from "@angular/router";
import { BrandComponent } from "./ReferentialData/Components/RestaurantData/BrandData/Brands/brand/brand.component";
import { CreateOrEditBrandModalComponent } from "./ReferentialData/Components/RestaurantData/BrandData/Brands/create-or-edit-brand-modal/create-or-edit-brand-modal.component";
import { CreateOrEditRestaurantComponent } from "./ReferentialData/Components/RestaurantData/Restaurants/create-or-edit-restaurant/create-or-edit-restaurant.component";
import { RestaurantComponent } from "./ReferentialData/Components/RestaurantData/Restaurants/restaurant/restaurant.component";
import { CarhopAddressComponent } from "./ReferentialData/Components/BrandManagement/carhop-address/carhop-address.component";
import { CreateOrEditCarhopAddressComponent } from "./ReferentialData/Components/BrandManagement/create-or-edit-carhop-address/create-or-edit-carhop-address.component";

import { KitchenTypeComponent } from "./ReferentialData/Components/BrandManagement/kitchen-type/kitchen-type.component";
import { CreateOrEditKitchenTypeComponent } from "./ReferentialData/Components/BrandManagement/create-or-edit-kitchen-type/create-or-edit-kitchen-type.component";
import { DeliveryZoneComponent } from "./ReferentialData/Components/BrandManagement/delivery-zone/delivery-zone.component";
import { CreateOrEditDeliveryZoneComponent } from "./ReferentialData/Components/BrandManagement/create-or-edit-delivery-zone/create-or-edit-delivery-zone.component";



const routes: Routes = [
  { path: 'brands', component: BrandComponent },
  { path: 'brands/createBrand', component: CreateOrEditBrandModalComponent },
  { path: 'brands/updateBrand/:id', component: CreateOrEditBrandModalComponent },
  { path: 'brands/viewBrand/:id', component: CreateOrEditBrandModalComponent },
  { path: 'restaurants', component: RestaurantComponent },
  { path: 'restaurants/createRestaurant', component: CreateOrEditRestaurantComponent },
  { path: 'restaurants/updateRestaurant/:id', component: CreateOrEditRestaurantComponent },
  { path: 'restaurants/viewRestaurant/:id', component: CreateOrEditRestaurantComponent },
  { path: 'carhopRestaurants', component: CarhopAddressComponent },
  { path: 'carhopRestaurants/createCarhopAddress', component: CreateOrEditCarhopAddressComponent },
  { path: 'carhopRestaurants/updateCarhopAddress/:id', component: CreateOrEditCarhopAddressComponent },
  { path: 'carhopRestaurants/viewCarhopAddress/:id', component: CreateOrEditCarhopAddressComponent },
  { path: 'kitchenTypes', component: KitchenTypeComponent },
  { path: 'kitchenTypes/createKitchenType', component: CreateOrEditKitchenTypeComponent },
  { path: 'deliveryZone', component: DeliveryZoneComponent },
  { path: 'deliveryZone/createDeliveryZone', component: CreateOrEditDeliveryZoneComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  providers: [
    {provide:MatDialogRef , useValue:{} ,
    
    },


    { provide: MAT_DIALOG_DATA, useValue: {} }

 ],
})
export class BrandManagementRoutingModule { }
