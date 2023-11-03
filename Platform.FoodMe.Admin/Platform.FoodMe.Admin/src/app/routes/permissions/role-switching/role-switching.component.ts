import { Component, OnDestroy, OnInit } from '@angular/core';
import { NgxRolesService, NgxPermissionsService } from 'ngx-permissions';
import { Subject } from 'rxjs';
import { Router } from '@angular/router';
import { takeUntil } from 'rxjs/operators';
import { EnumType } from 'typescript';
import { RoleService } from '@shared/services/RoleService/Role.Service';
import { RoleVM } from 'app/routes/ReferentialData/Models/User Data/RoleVM';
export interface PeriodicElement {
  Name: string;
  CraeteUser: string;
  Status: string;

}

const ELEMENT_DATA: PeriodicElement[] = [
  // { Name: 'Hydrogen', CraeteUser:'user1' , Status: 'H'},
  // { Name: 'Helium', CraeteUser: 'user2', Status: 'He'},
  // { NameRole: 'Lithium', CraeteUser:'user3', Status: 'Li'},
  // { NameRole: 'Beryllium', CraeteUser: 'user4', Status: 'Be'},
  // { NameRole: 'Boron', CraeteUser: 'user5', Status: 'B'},
  // { NameRole: 'Carbon', CraeteUser: 'user6', Status: 'C'},
  // { NameRole: 'Nitrogen', CraeteUser: 'user7', Status: 'N'},
  // { NameRole: 'Oxygen', CraeteUser: 'user8', Status: 'O'},
  // { NameRole: 'Fluorine', CraeteUser: 'user9', Status: 'F'},
  // { NameRole: 'Neon', CraeteUser: 'user10', Status: 'Ne'},
];
@Component({
  selector: 'app-permissions-role-switching',
  templateUrl: './role-switching.component.html',
  styleUrls: ['./role-switching.component.scss'],
})


export class PermissionsRoleSwitchingComponent implements OnInit, OnDestroy {

  displayedColumns: string[] = [ 'Name', 'CreatorUserId','CreationTime', 'Status','Edit'];
  dataSource : Array<RoleVM> = [];
  currentRole!: string;

  currentPermissions!: string[];

  permissionsOfRole: any = {
    ADMIN: ['canAdd', 'canDelete', 'canEdit', 'canRead'],
    MANAGER: ['canAdd', 'canEdit', 'canRead'],
    GUEST: ['canRead'],
  };


  private readonly _destroy$ = new Subject<void>();

  constructor(private rolesSrv: NgxRolesService,
    private permissionsSrv: NgxPermissionsService,
    private router: Router,
    private roleService : RoleService
    ) {}

  ngOnInit() {
    this.currentRole = Object.keys(this.rolesSrv.getRoles())[0];
    this.currentPermissions = Object.keys(this.permissionsSrv.getPermissions());

    this.rolesSrv.roles$.pipe(takeUntil(this._destroy$)).subscribe(roles => {
      console.log(roles);
    });
    this.permissionsSrv.permissions$.pipe(takeUntil(this._destroy$)).subscribe(permissions => {
      console.log(permissions);
    });
    this.getAllRoles();
  }
  onContinue() {
    this.router.navigateByUrl('permissions/route-guard');
}
  ngOnDestroy() {
    this._destroy$.next();
    this._destroy$.complete();
  }

  onPermissionChange() {
    this.currentPermissions = this.permissionsOfRole[this.currentRole];
    this.rolesSrv.flushRolesAndPermissions();
    this.rolesSrv.addRoleWithPermissions(this.currentRole, this.currentPermissions);
  }

  getAllRoles()
  {
    //send request to api and get responce
    this.roleService.getAllRoles().subscribe(res =>{

      this.dataSource = res;
      console.log(this.dataSource);
    });
  }
}
