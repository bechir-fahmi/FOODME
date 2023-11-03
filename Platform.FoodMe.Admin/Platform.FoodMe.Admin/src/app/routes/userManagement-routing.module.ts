import { NgModule } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { RouterModule, Routes } from '@angular/router';
import { CreateOrEditRoleComponent } from './ReferentialData/Components/UserData/Role/create-or-edit-role/create-or-edit-role.component';

import { RoleComponent } from './ReferentialData/Components/UserData/Role/role.component';
import { CreateOrEditUserComponent } from './ReferentialData/Components/UserData/User/create-or-edit-user-model/create-or-edit-user-model.component';
import { userComponent } from './ReferentialData/Components/UserData/User/user.component';




const routes: Routes = [
  { path: 'Users', component: userComponent },
  { path: 'Users/createUser', component: CreateOrEditUserComponent },
  { path: 'Users/viewUser/:id', component: CreateOrEditUserComponent },
  { path: 'Users/updateUser/:id', component: CreateOrEditUserComponent },
  { path: 'roles', component: RoleComponent },
  { path: 'roles/createRole', component: CreateOrEditRoleComponent },
  { path: 'roles/viewRole/:id', component: CreateOrEditRoleComponent },
  { path: 'roles/updateRole/:id', component: CreateOrEditRoleComponent },
  
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  providers: [
    {provide:MatDialogRef , useValue:{} },

    { provide: MAT_DIALOG_DATA, useValue: {} }
    
 ],
})
export class UserManagementRoutingModule { }