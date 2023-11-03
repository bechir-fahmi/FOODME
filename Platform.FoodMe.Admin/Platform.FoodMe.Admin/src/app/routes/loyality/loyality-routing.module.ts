import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoyalityComponent } from './loyality.component';

const routes: Routes = [{ path: '', component: LoyalityComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LoyalityRoutingModule { }
