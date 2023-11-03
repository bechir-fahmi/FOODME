import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';
import { RoutesRoutingModule } from './routes-routing.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import { LoginComponent } from './sessions/login/login.component';
import { RegisterComponent } from './sessions/register/register.component';
import { Error403Component } from './sessions/403.component';
import { Error404Component } from './sessions/404.component';
import { Error500Component } from './sessions/500.component';
import { MaterialModule } from 'app/material.module';
import { CountryComponent } from './ReferentialData/Components/LocationData/Countries/country/country.component';
import { CreateOrEditCountryModalComponent } from './ReferentialData/Components/LocationData/Countries/create-or-edit-country-modal/create-or-edit-country-modal.component';
import { RegionComponent } from './ReferentialData/Components/LocationData/Regions/region/region.component';
import { AreaComponent } from './ReferentialData/Components/LocationData/Areas/area/area.component';
import { CreateOrEditAreaModalComponent } from './ReferentialData/Components/LocationData/Areas/create-or-edit-area-modal/create-or-edit-area-modal.component';
import { CreateOrEditregionModalComponent } from './ReferentialData/Components/LocationData/Regions/create-or-edit-region-modal/create-or-edit-region-modal.component';
import { CityComponent } from './ReferentialData/Components/LocationData/Cities/city/city.component';
import { CreateOrEditCityModalComponent } from './ReferentialData/Components/LocationData/Cities/create-or-edit-city-modal/create-or-edit-city-modal.component';
import { LanguageComponent } from './ReferentialData/Components/LanguageData/Languages/language/language.component';
import { CreateOrEditLanguageModalComponent } from './ReferentialData/Components/LanguageData/Languages/create-or-edit-language-modal/create-or-edit-language-modal.component';
import { ViewLanguageResourceModalComponent } from './ReferentialData/Components/LanguageSources/view-language-resource-modal/view-language-resource-modal.component';
import { CreateOrEditLanguageResourceModalComponent } from './ReferentialData/Components/LanguageSources/create-or-edit-language-resource-modal/create-or-edit-language-resource-modal.component';
import { RestaurantComponent } from './ReferentialData/Components/RestaurantData/Restaurants/restaurant/restaurant.component';
import { CreateOrEditRestaurantComponent } from './ReferentialData/Components/RestaurantData/Restaurants/create-or-edit-restaurant/create-or-edit-restaurant.component';
import { BrandComponent } from './ReferentialData/Components/RestaurantData/BrandData/Brands/brand/brand.component';
import { CreateOrEditBrandModalComponent } from './ReferentialData/Components/RestaurantData/BrandData/Brands/create-or-edit-brand-modal/create-or-edit-brand-modal.component';
import { BrandGroupsComponent } from './ReferentialData/Components/RestaurantData/BrandData/BrandGroupData/brand-groups/brand-groups.component';
import { NgxMatIntlTelInputComponent } from 'ngx-mat-intl-tel-input';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ReactiveFormsModule } from '@angular/forms';
import { CreateOrEditBrandGroupComponent } from './ReferentialData/Components/RestaurantData/BrandData/BrandGroupData/create-or-edit-brand-group/create-or-edit-brand-group.component';
import { CreateOrEditUserComponent } from './ReferentialData/Components/UserData/User/create-or-edit-user-model/create-or-edit-user-model.component';
import { userComponent } from './ReferentialData/Components/UserData/User/user.component';
import { AddressListComponent } from './ReferentialData/Components/UserData/AddressList/address-list/address-list.component';
import { CreateOrEditAddressListComponent } from './ReferentialData/Components/UserData/AddressList/create-or-edit-address-list/create-or-edit-address-list.component';
import { DeliveryCompanyComponent } from './ReferentialData/Components/RestaurantData/DeliveryCompany/delivery-company/delivery-company.component';
import { CreateOrEditDeliveryCompanyComponent } from './ReferentialData/Components/RestaurantData/DeliveryCompany/create-or-edit-delivery-company/create-or-edit-delivery-company.component';

import { UploadUserPictureComponent } from './ReferentialData/Components/UserData/User/upload-user-picture/upload-user-picture.component';
import { RemoveElementComponent } from '@shared/components/remove-element/remove-element.component';
import { MenuComponent } from './ReferentialData/Components/MenuData/menu/menu.component';
import { CreateOrEditMenuComponent } from './ReferentialData/Components/MenuData/create-or-edit-menu/create-or-edit-menu.component';
import { DxBulletModule, DxCheckBoxModule, DxDataGridModule, DxSelectBoxModule, DxTemplateModule } from 'devextreme-angular';

import { CreateOrEditTemplateMenuComponent } from './ReferentialData/Components/MenuData/create-or-edit-template-menu/create-or-edit-template-menu.component';
import { TemplateMenuComponent } from './ReferentialData/Components/MenuData/template-menu/template-menu.component';
import { MenuCategoryComponent } from './ReferentialData/Components/MenuData/menu-category/menu-category.component';
import { CreateOrEditMenuCategoryComponent } from './ReferentialData/Components/MenuData/create-or-edit-menu-category/create-or-edit-menu-category.component';
import { MenuItemComponent } from './ReferentialData/Components/MenuData/menu-item/menu-item.component';
import { CreateOrEditMenuItemComponent } from './ReferentialData/Components/MenuData/create-or-edit-menu-item/create-or-edit-menu-item.component';
import { IgxAvatarModule, IgxButtonModule, IgxGridModule, IgxIconModule, IgxInputGroupModule, IgxLayoutModule, IgxRippleModule } from 'igniteui-angular';
import { RouterTestingModule } from '@angular/router/testing';
import { ExtraModifierGroupComponent } from './ReferentialData/Components/MenuData/extra-modifier-group/extra-modifier-group.component';
import { CreateOrEditExtraModifierGroupComponent } from './ReferentialData/Components/MenuData/create-or-edit-extra-modifier-group/create-or-edit-extra-modifier-group.component';
import { DiscountReductionComponent } from './ReferentialData/Components/Promotion/discount-reduction/discount-reduction.component';
import { CreateOrEditDiscountReductionComponent } from './ReferentialData/Components/Promotion/create-or-edit-discount-reduction/create-or-edit-discount-reduction.component';
import { DeliveryOfferComponent } from './ReferentialData/Components/Promotion/delivery-offer/delivery-offer.component';
import { CreateOrEditDeliveryOfferComponent } from './ReferentialData/Components/Promotion/create-or-edit-delivery-offer/create-or-edit-delivery-offer.component';
import { PromoBogoComponent } from './ReferentialData/Components/Promotion/promo-bogo/promo-bogo.component';
import { CreateOrEditPromoBogoComponent } from './ReferentialData/Components/Promotion/create-or-edit-promo-bogo/create-or-edit-promo-bogo.component';
import { CustomerCouponComponent } from './ReferentialData/Components/Promotion/customer-coupon/customer-coupon.component';
import { CreateOrEditCustomerCouponComponent } from './ReferentialData/Components/Promotion/create-or-edit-customer-coupon/create-or-edit-customer-coupon.component';
import { CouponComponent } from './ReferentialData/Components/Promotion/coupon/coupon.component';
import { CreateOrEditCouponComponent } from './ReferentialData/Components/Promotion/create-or-edit-coupon/create-or-edit-coupon.component';
import { DiscountBogoComponent } from './ReferentialData/Components/Promotion/discount-bogo/discount-bogo.component';
import { CreateOrEditDiscountBogoComponent } from './ReferentialData/Components/Promotion/create-or-edit-discount-bogo/create-or-edit-discount-bogo.component';
import { WeCareComponent } from './monitoringTool/we-care/we-care.component';
import { CashierComponent } from './monitoringTool/cashier/cashier.component';
import { CustomersComponent } from './monitoringTool/customers/customers.component';
import { MonitorListComponent } from './monitoringTool/monitor-list/monitor-list.component';
import { AddNewMonitorComponent } from './monitoringTool/add-new-monitor/add-new-monitor.component';
import { OrderCardComponent } from './monitoringTool/order-card/order-card.component';
import { PaymentMethodComponent } from './ReferentialData/Components/GlobalSettings/payment-method/payment-method.component';
import { CreateOrEditPaymentMethodComponent } from './ReferentialData/Components/GlobalSettings/create-or-edit-payment-method/create-or-edit-payment-method.component';
import { ExternalOperatorComponent } from './ReferentialData/Components/GlobalSettings/external-operator/external-operator.component';
import { CreateOrEditExternalOperatorComponent } from './ReferentialData/Components/GlobalSettings/create-or-edit-external-operator/create-or-edit-external-operator.component';
import { CarhopAddressComponent } from './ReferentialData/Components/BrandManagement/carhop-address/carhop-address.component';
import { CreateOrEditCarhopAddressComponent } from './ReferentialData/Components/BrandManagement/create-or-edit-carhop-address/create-or-edit-carhop-address.component';
import { CustomerPointComponent } from './ReferentialData/Components/CustomerManagement/customer-point/customer-point.component';
import { CreateOrEditCustomerPointComponent } from './ReferentialData/Components/CustomerManagement/create-or-edit-customer-point/create-or-edit-customer-point.component';
import { KitchenTypeComponent } from './ReferentialData/Components/BrandManagement/kitchen-type/kitchen-type.component';
import { CreateOrEditKitchenTypeComponent } from './ReferentialData/Components/BrandManagement/create-or-edit-kitchen-type/create-or-edit-kitchen-type.component';
import { OrderActionReasonComponent } from './ReferentialData/Components/OrderManagement/order-action-reason/order-action-reason.component';
import { CreateOrEditOrderActionReasonComponent } from './ReferentialData/Components/OrderManagement/create-or-edit-order-action-reason/create-or-edit-order-action-reason.component';
import { DeliveryZoneComponent } from './ReferentialData/Components/BrandManagement/delivery-zone/delivery-zone.component';
import { CreateOrEditDeliveryZoneComponent } from './ReferentialData/Components/BrandManagement/create-or-edit-delivery-zone/create-or-edit-delivery-zone.component';
import { CreateOrEditRoleComponent } from './ReferentialData/Components/UserData/Role/create-or-edit-role/create-or-edit-role.component';
import { RoleComponent } from './ReferentialData/Components/UserData/Role/role.component';



import { LoyaltySettingComponent } from './ReferentialData/Components/CustomerManagement/loyalty-setting/loyalty-setting.component';
import { CustomerWalletComponent } from './ReferentialData/Components/CustomerManagement/customer-wallet/customer-wallet.component';
import { AddFundWalletComponent } from './ReferentialData/Components/CustomerManagement/customer-wallet/add-fund-wallet/add-fund-wallet.component';
import { CustomerListComponent } from './ReferentialData/Components/CustomerManagement/customer-list/customer-list.component';
import { ConfirmDialoComponent } from './monitoringTool/cashier/confirm-dialo/confirm-dialo.component';
import { ViewMenuComponent } from './ReferentialData/Components/MenuData/view-menu/view-menu.component';
import { ViewTemplateMenuComponent } from './ReferentialData/Components/MenuData/view-template-menu/view-template-menu.component';
import { ViewMenuItemComponent } from './ReferentialData/Components/MenuData/view-menu-item/view-menu-item.component';
import { ViewMenuCategoryComponent } from './ReferentialData/Components/MenuData/view-menu-category/view-menu-category.component';
import { ViewExtraModifierGroupComponent } from './ReferentialData/Components/MenuData/view-extra-modifier-group/view-extra-modifier-group.component';
import { OrdersComponent } from './orderManagement/orders/orders.component';
import { NgbCollapseModule } from '@ng-bootstrap/ng-bootstrap';
import { CardOrderComponent } from './orderManagement/orders/card-order/card-order.component';
import { TrackingOrderComponent } from './orderManagement/orders/tracking-order/tracking-order.component';
import { OrderCancellationReasonComponent } from './ReferentialData/Components/OrderManagement/order-cancellation-reason/order-cancellation-reason.component';
import { EditCancellationReasonComponent } from './ReferentialData/Components/OrderManagement/order-cancellation-reason/edit-cancellation-reason/edit-cancellation-reason.component';
import { OrderRefundsReasonComponent } from './ReferentialData/Components/OrderManagement/order-refunds-reason/order-refunds-reason.component';
import { EditOrderRefundComponent } from './ReferentialData/Components/OrderManagement/order-refunds-reason/edit-order-refund/edit-order-refund.component';
import { RestaurantEvaluationComponent } from './ReferentialData/Components/CustomerManagement/restaurant-evaluation/restaurant-evaluation.component';
import { DriverEvaluationComponent } from './ReferentialData/Components/CustomerManagement/driver-evaluation/driver-evaluation.component';
// search module

const COMPONENTS: any[] = [
  DashboardComponent,
  LoginComponent,
  RegisterComponent,
  Error403Component,
  Error404Component,
  Error500Component,
  CountryComponent,
  CreateOrEditCountryModalComponent,
  RegionComponent,
  CreateOrEditregionModalComponent,
  AreaComponent,
  CreateOrEditAreaModalComponent,
  CityComponent,
  // eslint-disable-next-line max-len
  CreateOrEditCityModalComponent, LanguageComponent, CreateOrEditLanguageModalComponent,CreateOrEditCountryModalComponent, CreateOrEditBrandGroupComponent,BrandGroupsComponent, CreateOrEditUserComponent,userComponent, CreateOrEditAddressListComponent,AddressListComponent, CreateOrEditDeliveryCompanyComponent,DeliveryCompanyComponent,CreateOrEditRoleComponent,RoleComponent
];
const COMPONENTS_DYNAMIC: any[] = [];

@NgModule({
  imports: [SharedModule, RoutesRoutingModule, MaterialModule, ReactiveFormsModule,
    NgxMatIntlTelInputComponent,
    BrowserAnimationsModule,
    MatFormFieldModule,
    MatInputModule,
    NgbCollapseModule,
    MatFormFieldModule,
    DxDataGridModule,
    DxTemplateModule,
    DxBulletModule,
    DxCheckBoxModule,
    DxSelectBoxModule,
    IgxRippleModule,
    IgxGridModule,
    IgxIconModule,
    IgxLayoutModule,
    IgxAvatarModule,
    IgxInputGroupModule,
    IgxButtonModule,
    RouterTestingModule],

  // eslint-disable-next-line max-len
  declarations: [...COMPONENTS, ...COMPONENTS_DYNAMIC, RegionComponent, AreaComponent, CreateOrEditAreaModalComponent,CountryComponent,CityComponent, CreateOrEditAreaModalComponent, LanguageComponent, CreateOrEditLanguageModalComponent, ViewLanguageResourceModalComponent, CreateOrEditLanguageResourceModalComponent,CreateOrEditCountryModalComponent, RestaurantComponent, CreateOrEditRestaurantComponent, BrandComponent, CreateOrEditBrandModalComponent, BrandGroupsComponent, CreateOrEditBrandModalComponent,CreateOrEditUserComponent,userComponent, AddressListComponent,CreateOrEditAddressListComponent, DeliveryCompanyComponent,CreateOrEditDeliveryCompanyComponent, UploadUserPictureComponent,RemoveElementComponent, MenuComponent, CreateOrEditMenuComponent, TemplateMenuComponent, CreateOrEditTemplateMenuComponent, MenuCategoryComponent, CreateOrEditMenuCategoryComponent, MenuItemComponent, CreateOrEditMenuItemComponent, ExtraModifierGroupComponent, CreateOrEditExtraModifierGroupComponent, DiscountReductionComponent, CreateOrEditDiscountReductionComponent, DeliveryOfferComponent, CreateOrEditDeliveryOfferComponent, PromoBogoComponent, CreateOrEditPromoBogoComponent, CustomerCouponComponent, CreateOrEditCustomerCouponComponent, CouponComponent, CreateOrEditCouponComponent, DiscountBogoComponent, CreateOrEditDiscountBogoComponent,WeCareComponent, PaymentMethodComponent, CreateOrEditPaymentMethodComponent, ExternalOperatorComponent, CreateOrEditExternalOperatorComponent, CarhopAddressComponent, CreateOrEditCarhopAddressComponent, CustomerPointComponent, CreateOrEditCustomerPointComponent, KitchenTypeComponent, CreateOrEditKitchenTypeComponent, OrderActionReasonComponent, CreateOrEditOrderActionReasonComponent, DeliveryZoneComponent, CreateOrEditDeliveryZoneComponent,CreateOrEditRoleComponent,RoleComponent,ConfirmDialoComponent, CashierComponent, CustomersComponent,
    MonitorListComponent, AddNewMonitorComponent, OrderCardComponent,PaymentMethodComponent, CreateOrEditPaymentMethodComponent, ExternalOperatorComponent, CreateOrEditExternalOperatorComponent, CarhopAddressComponent, CreateOrEditCarhopAddressComponent, CustomerPointComponent, CreateOrEditCustomerPointComponent,LoyaltySettingComponent,
    CustomerWalletComponent, AddFundWalletComponent, CustomerListComponent, ConfirmDialoComponent, ViewMenuComponent, ViewTemplateMenuComponent, ViewMenuItemComponent, ViewMenuCategoryComponent, ViewExtraModifierGroupComponent, OrdersComponent, CardOrderComponent, TrackingOrderComponent,, OrderCancellationReasonComponent, EditCancellationReasonComponent, OrderRefundsReasonComponent, EditOrderRefundComponent, RestaurantEvaluationComponent, DriverEvaluationComponent],



})
export class RoutesModule {}
