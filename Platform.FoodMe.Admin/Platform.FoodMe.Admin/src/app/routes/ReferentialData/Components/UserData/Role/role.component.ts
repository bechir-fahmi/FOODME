import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import{RoleService } from 'app/routes/ReferentialData/Services/UserData/Role/role.service';
import { RoleVM } from 'app/routes/ReferentialData/Models/User Data/RoleVM';

@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.scss']
})
export class RoleComponent implements OnInit {
 

  displayedColumns =  [ 'name','status', 'creatorUserId','creationTime' ,'actions'];
  dataSource: MatTableDataSource<RoleVM>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  // eslint-disable-next-line max-len
  constructor(private RoleService:RoleService,private router: Router,private route: ActivatedRoute) {
  }
   Roles: RoleVM[] = [];

  ngOnInit() {
    this.getRoles();
  }

  editRole(Role: RoleVM)
  {
   // const navigateToEdit=this.route.snapshot._routerState.url.concat(`/updateRole/${Role.id}`);
    // this.router.navigate([navigateToEdit]);
  }

  createRole()
  {
   // const navigateToCreate=this.route.snapshot._routerState.url.concat('/createRole');
   // this.router.navigate([navigateToCreate]);
  }

  viewRole(Role: RoleVM)
  {
    //const navigateToView=this.route.snapshot._routerState.url.concat(`/viewRole/${Role.id}`);
    //this.router.navigate([navigateToView]);
  }

  refresh(): void {
    this.RoleService.getroles().subscribe(res =>{
      this.dataSource.data = res;
    });
  }
  getRoles()
  {
    
    this.RoleService.getroles().subscribe(res =>{
      this.Roles = res;
      this.dataSource = new MatTableDataSource(this.Roles);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }
  deleteRole(id: string){
    this.RoleService.deleterole(id).subscribe(
      () => {
        this.refresh();
      });
  }

}







