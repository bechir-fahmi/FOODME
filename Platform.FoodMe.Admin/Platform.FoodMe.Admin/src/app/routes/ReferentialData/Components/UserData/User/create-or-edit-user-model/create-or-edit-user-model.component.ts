import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { UserVM } from 'app/routes/ReferentialData/Models/User Data/UserVM';
import { UserService } from 'app/routes/ReferentialData/Services/UserData/User/user.service';



@Component({
  selector: 'app-create-or-edit-user-model',
  templateUrl: './create-or-edit-user-model.component.html',
  styleUrls: ['./create-or-edit-user-model.component.scss']
})
export class CreateOrEditUserComponent  implements OnInit  {
  
  user: UserVM = new UserVM();
  title? : string;
  view=false;
 

 constructor(private router: Router,private route: ActivatedRoute,private Userservice: UserService, public dialogref: MatDialogRef<CreateOrEditUserComponent>,
    @Inject(MAT_DIALOG_DATA) public data?: any){
     
    }
 
    
  ngOnInit(): void {
   

    if (this.route.snapshot.url[this.route.snapshot.url.length-2].toString().includes("update"))
    {
     this.title = 'Edit User'
      this.Userservice.getUser(this.route.snapshot.url[this.route.snapshot.url.length-1].toString()).subscribe(res=>{this.user=res});
   }
   if (this.route.snapshot.url[this.route.snapshot.url.length-2].toString().includes("view"))
   {
     this.view=true;
    this.title = 'View User'
     this.Userservice.getUser(this.route.snapshot.url[this.route.snapshot.url.length-1].toString()).subscribe(res=>{this.user=res});
  }
 if (this.route.snapshot.url[this.route.snapshot.url.length-1].toString().includes("create")) {
       this.title = 'Add User'
     }
  }

  save(){
    if(this.route.snapshot.url[this.route.snapshot.url.length-2].toString().includes("update")) {
      
      this.Userservice.updateUser(this.user).subscribe(
        () => {
          this.router.navigate(["/userManagement/Users"]);
        }
      )
    }
    else {
      
      this.Userservice.addUser(this.user).subscribe(
        () => {
          this.router.navigate(["/userManagement/Users"]);
        }
      )
    }
  }


}