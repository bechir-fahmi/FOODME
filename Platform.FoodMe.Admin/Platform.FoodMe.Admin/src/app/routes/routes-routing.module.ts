import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { environment } from '@env/environment';

import { AdminLayoutComponent } from '@theme/admin-layout/admin-layout.component';
import { AuthLayoutComponent } from '@theme/auth-layout/auth-layout.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { LoginComponent } from './sessions/login/login.component';
import { RegisterComponent } from './sessions/register/register.component';
import { Error403Component } from './sessions/403.component';
import { Error404Component } from './sessions/404.component';
import { Error500Component } from './sessions/500.component';
import { AuthGuard } from '@core/authentication';

const routes: Routes = [
  {
    path: '',
    component: AdminLayoutComponent,
    canActivate: [AuthGuard],
    canActivateChild: [AuthGuard],
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { path: 'dashboard', component: DashboardComponent },

      { path: '403', component: Error403Component },
      { path: '404', component: Error404Component },
      { path: '500', component: Error500Component },
      {
          path: 'order-management',
          loadChildren: () => import('./orderManagement-routing.module').then(m => m.orderManagementRoutingModule),
        },
      {
        path: 'order-management',
        loadChildren: () => import('./orderManagement-routing.module').then(m => m.orderManagementRoutingModule),
      },
      {
          path: 'monitoring-tool',
          loadChildren: () => import('./monitoringTool/monitoringTool-routing-module').then(m => m.monitoringToolRoutingModule),
        },
        {
          path: 'referentialData',
          loadChildren: () => import('./ReferentialData/ReferentialData-routing.module').then(m => m.ReferentialDataRoutingModule),
        },
        {
          path: 'userManagement',
          loadChildren: () => import('./userManagement-routing.module').then(m => m.UserManagementRoutingModule),
        },
        {
          path: 'brandManagement',
          loadChildren: () => import('./brandManagement-routing.module').then(m => m.BrandManagementRoutingModule),
        },
        {
          path: 'menuManagement',
          loadChildren: () => import('./menuManagement-routing.module').then(m => m.MenuManagementRoutingModule),
        },
        {
          path: 'promotions',
          loadChildren: () => import('./promotion-routing.module').then(m => m.PromotionRoutingModule),
        },
        {
          path: 'globalSettings',
          loadChildren: () => import('./GlobalSettings-routing.module').then(m => m.GlobalSettingsRoutingModule),
        },
        {
          path: 'customerManagement',
          loadChildren: () => import('./customerManagement-routing.module').then(m => m.CustomerManagementRoutingModule),
        },
      {
        path: 'profile',
        loadChildren: () => import('./profile/profile.module').then(m => m.ProfileModule),
      },
      {
        path: 'permissions',
        loadChildren: () =>
          import('./permissions/permissions.module').then(m => m.PermissionsModule),
      },
      { path: 'businessHours', loadChildren: () => import('./business-hours/business-hours.module').then(m => m.BusinessHoursModule) },
      { path: 'loyality', loadChildren: () => import('./loyality/loyality.module').then(m => m.LoyalityModule) },


    ],
  },
  {
    path: 'auth',
    component: AuthLayoutComponent,
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
    ],
  },
  { path: '**', redirectTo: 'dashboard' },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, {
      useHash: environment.useHash,
    }),
  ],
  exports: [RouterModule],
})
export class RoutesRoutingModule {}
