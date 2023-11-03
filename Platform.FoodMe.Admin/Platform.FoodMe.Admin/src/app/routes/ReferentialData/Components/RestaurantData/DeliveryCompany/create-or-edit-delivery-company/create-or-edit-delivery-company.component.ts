import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { DeliveryCompanyVM } from 'app/routes/ReferentialData/Models/RestaurantData/DeliveryCompanyVM';
import { DeliveryCompanyService } from 'app/routes/ReferentialData/Services/RestaurantData/DeliveryCompany.service';

@Component({
  selector: 'app-create-or-edit-delivery-company',
  templateUrl: './create-or-edit-delivery-company.component.html',
  styleUrls: ['./create-or-edit-delivery-company.component.scss']
})
export class CreateOrEditDeliveryCompanyComponent implements OnInit {
  DeliveryCompany: DeliveryCompanyVM = new DeliveryCompanyVM();
  title? : string;


 constructor(private DeliveryCompanyservice: DeliveryCompanyService, public dialogref: MatDialogRef<CreateOrEditDeliveryCompanyComponent>,private route: ActivatedRoute,private router: Router
    ,@Inject(MAT_DIALOG_DATA) public data?: any){
      
    }
    
  ngOnInit(): void {
    
   if (this.route.snapshot.url[this.route.snapshot.url.length-2].toString().includes("update"))
   {
    this.title = 'Edit DeliveryCompany'
     this.DeliveryCompanyservice.getDeliveryCompany(Number(this.route.snapshot.url[this.route.snapshot.url.length-1])).subscribe(res=>{this.DeliveryCompany=res});
  }
  if (this.route.snapshot.url[this.route.snapshot.url.length-2].toString().includes("view"))
  {
    
   this.title = 'View DeliveryCompany'
    this.DeliveryCompanyservice.getDeliveryCompany(Number(this.route.snapshot.url[this.route.snapshot.url.length-1])).subscribe(res=>{this.DeliveryCompany=res});
 }
 if(this.route.snapshot.url[this.route.snapshot.url.length-1].toString().includes("create")) {
      this.title = 'Add DeliveryCompany'
    }
  }

  save(){
    if(this.route.snapshot.url[this.route.snapshot.url.length-2].toString().includes("update")) {
      
      this.DeliveryCompanyservice.updateDeliveryCompany(this.DeliveryCompany).subscribe(
        () => {
          this.router.navigate(["/referentialData/restaurantData/DeliveryCompanys"]);
        }
      )
    }
    else {
      
      this.DeliveryCompanyservice.addDeliveryCompany(this.DeliveryCompany).subscribe(
        () => {
          this.router.navigate(["/referentialData/restaurantData/DeliveryCompanys"]);
        }
      )
    }
  }

}
