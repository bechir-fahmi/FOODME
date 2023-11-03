import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

import { DragDropModule } from '@angular/cdk/drag-drop';

import { MaterialModule } from '../material.module';
import { MaterialExtensionsModule } from '../material-extensions.module';

import { FormlyModule } from '@ngx-formly/core';
import { FormlyMaterialModule } from '@ngx-formly/material';
import { NgProgressModule } from 'ngx-progressbar';
import { NgProgressHttpModule } from 'ngx-progressbar/http';
import { NgProgressRouterModule } from 'ngx-progressbar/router';
import { NgxPermissionsModule } from 'ngx-permissions';
import { ToastrModule } from 'ngx-toastr';
import { TranslateModule } from '@ngx-translate/core';

import { BreadcrumbComponent } from './components/breadcrumb/breadcrumb.component';
import { PageHeaderComponent } from './components/page-header/page-header.component';
import { ErrorCodeComponent } from './components/error-code/error-code.component';
import { DisableControlDirective } from './directives/disable-control.directive';
import { SafeUrlPipe } from './pipes/safe-url.pipe';
import { ToObservablePipe } from './pipes/to-observable.pipe';
import { AfcoTicketOrderComponent } from './components/afco-ticket-order/afco-ticket-order.component';
import { AfcoDataPickerComponent } from './components/afco-data-picker/afco-data-picker.component';
import { CompanyPipe } from './pipes/company.pipe';
import { RestaurantNamePipe } from './pipes/restaurant-name.pipe';
import { LabelMenuTemplatePipe } from './pipes/label-menu-template.pipe';
import { LabelBrandPipe } from './pipes/label-brand.pipe';
import { LabelMenuPipe } from './pipes/label-menu.pipe';
import { LabelCategoryPipe } from './pipes/label-category.pipe';
import { LabelBrande } from './pipes/lablebrand';
import { image } from './pipes/image';
import { LableDescription } from './pipes/labledescription';
import { LabelGetkitchen } from './pipes/lablekitchen';
import { imageKitchen } from './pipes/imagekitchen';
import { lableArea } from './pipes/lablearea';
import { adressRestaurant } from './pipes/adressrestaurant';




const MODULES: any[] = [
  CommonModule,
  RouterModule,
  ReactiveFormsModule,
  FormsModule,
  DragDropModule,
  MaterialModule,
  MaterialExtensionsModule,
  FormlyModule,
  FormlyMaterialModule,
  NgProgressModule,
  NgProgressRouterModule,
  NgProgressHttpModule,
  NgxPermissionsModule,
  ToastrModule,
  TranslateModule

];
const COMPONENTS: any[] = [BreadcrumbComponent, PageHeaderComponent, ErrorCodeComponent , AfcoTicketOrderComponent, AfcoDataPickerComponent] ;
const COMPONENTS_DYNAMIC: any[] = [];
const DIRECTIVES: any[] = [DisableControlDirective];
const PIPES: any[] = [SafeUrlPipe, ToObservablePipe,CompanyPipe, RestaurantNamePipe,LabelBrande,image,LableDescription,LabelGetkitchen,imageKitchen,lableArea,adressRestaurant];

@NgModule({
  imports: [...MODULES],
  exports: [...MODULES, ...COMPONENTS, ...DIRECTIVES, ...PIPES ],
  declarations: [...COMPONENTS, ...COMPONENTS_DYNAMIC, ...DIRECTIVES, ...PIPES ],
})
export class SharedModule {}
