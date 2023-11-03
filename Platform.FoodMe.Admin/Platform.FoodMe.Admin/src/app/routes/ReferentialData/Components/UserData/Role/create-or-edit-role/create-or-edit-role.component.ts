import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Status } from '@shared/Models/LocationData/CountryDto';
import { RoleVM } from 'app/routes/ReferentialData/Models/User Data/RoleVM';
import { UserVM } from 'app/routes/ReferentialData/Models/User Data/UserVM';
import { RoleService } from 'app/routes/ReferentialData/Services/UserData/Role/role.service';
import { UserService } from 'app/routes/ReferentialData/Services/UserData/User/user.service';

@Component({
  selector: 'app-create-or-edit-role',
  templateUrl: './create-or-edit-role.component.html',
  styleUrls: ['./create-or-edit-role.component.scss']
})
export class CreateOrEditRoleComponent implements OnInit {
  Role: RoleVM = new RoleVM();
  title? : string;
  action = 0;
  statuses:any[]=[];
  users:UserVM[]=[];
  

 constructor( public dialogref: MatDialogRef<CreateOrEditRoleComponent>,private userService:UserService,private roleService:RoleService,private route: ActivatedRoute,private router: Router,
    @Inject(MAT_DIALOG_DATA) public data?: any){
        const statuses = Object.keys(Status)
        .filter((v) => isNaN(Number(v)))
        .map((name) => {
          return {
            id: Status[name as keyof typeof Status],
            name,
          };
        });
        this.statuses=statuses;
     
    }
    
  ngOnInit(): void {
this.userService.getAllUsers().subscribe(res=>{this.users=res;});
if (this.route.snapshot.url[this.route.snapshot.url.length-2].toString().includes('update'))
{
 this.title = 'Edit Role';
  // this.roleService.getRole(this.route.snapshot.url[this.route.snapshot.url.length-1].toString()).subscribe(res=>{this.Role=res;});
}
if (this.route.snapshot.url[this.route.snapshot.url.length-2].toString().includes('view'))
{

this.title = 'View Role';
//  this.roleService.getRole(this.route.snapshot.url[this.route.snapshot.url.length-1].toString()).subscribe(res=>{this.Role=res;});
}
if(this.route.snapshot.url[this.route.snapshot.url.length-1].toString().includes('create')) {
   this.title = 'Add Role';
 }
}


  save(){
    if(this.route.snapshot.url[this.route.snapshot.url.length-2].toString().includes('update')) {
      
      this.roleService.updaterole(this.Role).subscribe(
        () => {
          // this.router.navigate(["/referentialData/restaurantData/brandGroups"]);
        }
      );
    }
    else {
      
      this.roleService.addrole(this.Role).subscribe(
        () => {
          // this.router.navigate(["/referentialData/restaurantData/brandGroups"]);
        }
      );
    }
  }

}