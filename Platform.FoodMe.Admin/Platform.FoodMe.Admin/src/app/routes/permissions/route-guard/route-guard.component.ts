import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgxRolesService, NgxPermissionsService } from 'ngx-permissions';
import {FormControl} from '@angular/forms';
@Component({
  selector: 'app-permissions-route-guard',
  templateUrl: './route-guard.component.html',
  styleUrls: ['./route-guard.component.scss'],
})
export class PermissionsRouteGuardComponent implements OnInit {

  toppings = new FormControl('');
  length:Number=3;
  toppingList: string[] = ['View', 'Update', 'Delete', 'Create'];
  List: string[] = ['user', 'superAdmin', 'Brand'];
  currentRole!: string;

  currentPermissions!: string[];

  permissionsOfRole: any = {
    ADMIN: ['canAdd', 'canDelete', 'canEdit', 'canRead'],
    MANAGER: ['canAdd', 'canEdit', 'canRead'],
    GUEST: ['canRead'],
  };

  constructor(
    private rolesSrv: NgxRolesService,
    private permissionsSrv: NgxPermissionsService,
    private router: Router
  ) {}

  ngOnInit() {
    this.currentRole = Object.keys(this.rolesSrv.getRoles())[0];
    this.currentPermissions = Object.keys(this.permissionsSrv.getPermissions());
  }

  onPermissionChange() {
    this.currentPermissions = this.permissionsOfRole[this.currentRole];
    this.rolesSrv.flushRolesAndPermissions();
    this.rolesSrv.addRoleWithPermissions(this.currentRole, this.currentPermissions);

    this.router.navigateByUrl('/dashboard');
  }
}
