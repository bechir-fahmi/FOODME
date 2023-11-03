import { NgModule } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { RouterModule, Routes } from "@angular/router";
import { LanguageComponent } from "./Components/LanguageData/Languages/language/language.component";
import { AreaComponent } from "./Components/LocationData/Areas/area/area.component";
import { CreateOrEditAreaModalComponent } from "./Components/LocationData/Areas/create-or-edit-area-modal/create-or-edit-area-modal.component";
import { CityComponent } from "./Components/LocationData/Cities/city/city.component";
import { CreateOrEditCityModalComponent } from "./Components/LocationData/Cities/create-or-edit-city-modal/create-or-edit-city-modal.component";
import { CountryComponent } from "./Components/LocationData/Countries/country/country.component";
import { CreateOrEditCountryModalComponent } from "./Components/LocationData/Countries/create-or-edit-country-modal/create-or-edit-country-modal.component";
import { CreateOrEditregionModalComponent } from "./Components/LocationData/Regions/create-or-edit-region-modal/create-or-edit-region-modal.component";
import { RegionComponent } from "./Components/LocationData/Regions/region/region.component";
import { MenuComponent } from "./Components/MenuData/menu/menu.component";
import { BrandGroupsComponent } from "./Components/RestaurantData/BrandData/BrandGroupData/brand-groups/brand-groups.component";
import { CreateOrEditBrandGroupComponent } from "./Components/RestaurantData/BrandData/BrandGroupData/create-or-edit-brand-group/create-or-edit-brand-group.component";
import { CreateOrEditDeliveryCompanyComponent } from "./Components/RestaurantData/DeliveryCompany/create-or-edit-delivery-company/create-or-edit-delivery-company.component";
import { DeliveryCompanyComponent } from "./Components/RestaurantData/DeliveryCompany/delivery-company/delivery-company.component";
import { AddressListComponent } from "./Components/UserData/AddressList/address-list/address-list.component";


const routes: Routes = [
  { path: 'locationData/countries', component: CountryComponent },
  { path: 'locationData/countries/createCountry', component: CreateOrEditCountryModalComponent },
  { path: 'locationData/areas/createArea', component: CreateOrEditAreaModalComponent },
  { path: 'locationData/cities/createCity', component: CreateOrEditCityModalComponent },
  { path: 'locationData/regions/createRegion', component: CreateOrEditregionModalComponent },
  { path: 'userData/addressLists', component: AddressListComponent },
  { path: 'restaurantData/brandGroups', component: BrandGroupsComponent },
  { path: 'restaurantData/brandGroups/createBrandGroup', component: CreateOrEditBrandGroupComponent },
  { path: 'restaurantData/brandGroups/viewBrandGroup/:id', component: CreateOrEditBrandGroupComponent },
  { path: 'restaurantData/brandGroups/updateBrandGroup/:id', component: CreateOrEditBrandGroupComponent },
  { path: 'restaurantData/deliveryCompanies', component: DeliveryCompanyComponent },
  { path: 'restaurantData/deliveryCompanies/createDeliveryCompany', component: CreateOrEditDeliveryCompanyComponent },
  { path: 'languageData/languages', component: LanguageComponent },
  { path: 'locationData/cities', component: CityComponent },
  { path: 'locationData/regions', component: RegionComponent },
  { path: 'locationData/areas', component: AreaComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  providers: [
    {provide:MatDialogRef , useValue:{} },

    { provide: MAT_DIALOG_DATA, useValue: {} }
    
 ],
})
export class ReferentialDataRoutingModule { }