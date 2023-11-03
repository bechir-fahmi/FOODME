import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomerPointVM } from 'app/routes/ReferentialData/Models/CustomerManagement/CustomerPointVM';
import { TypeOfGettingPoints } from 'app/routes/ReferentialData/Models/CustomerManagement/TypeOfGettingPoints';

@Component({
  selector: 'app-create-or-edit-customer-point',
  templateUrl: './create-or-edit-customer-point.component.html',
  styleUrls: ['./create-or-edit-customer-point.component.scss']
})
export class CreateOrEditCustomerPointComponent implements OnInit {
  CustomerPoint: CustomerPointVM = new CustomerPointVM();
  title? : string;
  action = 0;
  typeOfGettingPoints:any[]=[];

 constructor( public dialogref: MatDialogRef<CreateOrEditCustomerPointComponent>,private route: ActivatedRoute,private router: Router,
    @Inject(MAT_DIALOG_DATA) public data?: any){
      const typeOfGettingPoints = Object.keys(TypeOfGettingPoints)
      .filter((v) => isNaN(Number(v)))
      .map((name) => {
        return {
          id: TypeOfGettingPoints[name as keyof typeof TypeOfGettingPoints],
          name,
        };
      });
      this.typeOfGettingPoints=typeOfGettingPoints;
    }
    
  ngOnInit(): void {

    if (this.route.snapshot.url[this.route.snapshot.url.length-2].toString().includes("update"))
    {
     this.title = 'Edit Customer Points'
      // this.CustomerPointService.getCustomerPoint(Number(this.route.snapshot.url[this.route.snapshot.url.length-1])).subscribe(res=>{this.CustomerPoint=res});
   }
   if (this.route.snapshot.url[this.route.snapshot.url.length-2].toString().includes("view"))
   {
     
    this.title = 'View Customer Points'
    //  this.CustomerPointService.getCustomerPoint(Number(this.route.snapshot.url[this.route.snapshot.url.length-1])).subscribe(res=>{this.CustomerPoint=res});
  }
  if(this.route.snapshot.url[this.route.snapshot.url.length-1].toString().includes("create")) {
       this.title = 'Add Customer Points'
     }
   }
  

  save(){
    // if(this.action == 1) {
      
    //   this.CustomerPointService.updateCustomerPoint(this.CustomerPoint).subscribe(
    //     () => {
    //       this.dialogref.close();
    //     }
    //   )
    // }
    // else {
      
    //   this.CustomerPointService.addCustomerPoint(this.CustomerPoint).subscribe(
    //     () => {
    //       this.dialogref.close();
    //     }
    //   )
    // }
  }

}
