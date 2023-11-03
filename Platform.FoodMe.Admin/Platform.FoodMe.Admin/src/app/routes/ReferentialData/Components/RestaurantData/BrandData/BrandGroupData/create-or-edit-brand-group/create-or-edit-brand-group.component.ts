import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { BrandGroupVM } from 'app/routes/ReferentialData/Models/RestaurantData/BrandStructure/BrandGroupVM';
import { BrandGroupService } from 'app/routes/ReferentialData/Services/RestaurantData/BrandStructure/BrandGroup.service';


@Component({
  selector: 'app-create-or-edit-brand-group',
  templateUrl: './create-or-edit-brand-group.component.html',
  styleUrls: ['./create-or-edit-brand-group.component.scss']
})
export class CreateOrEditBrandGroupComponent  implements OnInit {
  BrandGroup: BrandGroupVM = new BrandGroupVM();
  title? : string;
 view=false;

 constructor(private BrandGroupservice: BrandGroupService, public dialogref: MatDialogRef<CreateOrEditBrandGroupComponent>,private route: ActivatedRoute,private router: Router
    ,@Inject(MAT_DIALOG_DATA) public data?: any){
      
    }
    
  ngOnInit(): void {
    
   if (this.route.snapshot.url[this.route.snapshot.url.length-2].toString().includes("update"))
   {
    this.title = 'Edit BrandGroup'
     this.BrandGroupservice.getBrandGroup(Number(this.route.snapshot.url[this.route.snapshot.url.length-1])).subscribe(res=>{this.BrandGroup=res});
  }
  if (this.route.snapshot.url[this.route.snapshot.url.length-2].toString().includes("view"))
  {
    this.view=true;
   this.title = 'View BrandGroup'
    this.BrandGroupservice.getBrandGroup(Number(this.route.snapshot.url[this.route.snapshot.url.length-1])).subscribe(res=>{this.BrandGroup=res});
 }
 if(this.route.snapshot.url[this.route.snapshot.url.length-1].toString().includes("create")) {
      this.title = 'Add BrandGroup'
    }
  }

  save(){
    if(this.route.snapshot.url[this.route.snapshot.url.length-2].toString().includes("update")) {
      
      this.BrandGroupservice.updateBrandGroup(this.BrandGroup).subscribe(
        () => {
          this.router.navigate(["/referentialData/restaurantData/brandGroups"]);
        }
      )
    }
    else {
      
      this.BrandGroupservice.addBrandGroup(this.BrandGroup).subscribe(
        () => {
          this.router.navigate(["/referentialData/restaurantData/brandGroups"]);
        }
      )
    }
  }

}