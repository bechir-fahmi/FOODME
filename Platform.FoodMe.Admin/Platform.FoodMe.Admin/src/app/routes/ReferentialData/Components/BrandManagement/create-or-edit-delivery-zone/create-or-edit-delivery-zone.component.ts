import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';import { DeliveryZoneVM } from 'app/routes/ReferentialData/Models/BrandManagement/DeliveryZoneVM';
import { DeliveryZoneService } from 'app/routes/ReferentialData/Services/BrandManagement/delivery-zone.service';

@Component({
  selector: 'app-create-or-edit-delivery-zone',
  templateUrl: './create-or-edit-delivery-zone.component.html',
  styleUrls: ['./create-or-edit-delivery-zone.component.scss']
})
export class CreateOrEditDeliveryZoneComponent implements OnInit {
  DeliveryZone: DeliveryZoneVM = new DeliveryZoneVM();
  title? : string;
  action = 0;
  
 constructor( public dialogref: MatDialogRef<CreateOrEditDeliveryZoneComponent>,private route: ActivatedRoute,private router: Router, private deliveryZoneService: DeliveryZoneService,
    @Inject(MAT_DIALOG_DATA) public data?: any){
     
    }
    
  ngOnInit(): void {

    if (this.route.snapshot.url[this.route.snapshot.url.length-2].toString().includes("update"))
    {
     this.title = 'Edit Delivery Zone'
      // this.DeliveryZoneService.getDeliveryZone(Number(this.route.snapshot.url[this.route.snapshot.url.length-1])).subscribe(res=>{this.DeliveryZone=res});
   }
   if (this.route.snapshot.url[this.route.snapshot.url.length-2].toString().includes("view"))
   {
     
    this.title = 'View Delivery Zone'
    //  this.DeliveryZoneService.getDeliveryZone(Number(this.route.snapshot.url[this.route.snapshot.url.length-1])).subscribe(res=>{this.DeliveryZone=res});
  }
  if(this.route.snapshot.url[this.route.snapshot.url.length-1].toString().includes("create")) {
       this.title = 'Add Delivery Zone'
     }
   }
  



  save(){
    /*  if(this.action == 1) {
      
      this.DeliveryZoneService.updateDeliveryZone(this.DeliveryZone).subscribe(
         () => {
           this.dialogref.close();
        }
       )
     }
   else {
      
      this.DeliveryZoneService.addDeliveryZone(this.DeliveryZone).subscribe(
        () => {
          this.dialogref.close();
        }
      )
   }*/
  }
  doSomething(e:any){
    // this.Restaurantservice.getRestaurantsByBrandId(e.id).subscribe(res=>{this.restaurants=res})
    
  }


}




