import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { DiscountBogoVM } from 'app/routes/ReferentialData/Models/Promotion/DiscountBogoVM';
import { PromoType } from 'app/routes/ReferentialData/Models/Promotion/PromoType';
import { RestaurantVM } from 'app/routes/ReferentialData/Models/RestaurantData/RestaurantVM';
import { RestaurantService } from 'app/routes/ReferentialData/Services/RestaurantData/Restaurant.service';

@Component({
  selector: 'app-create-or-edit-discount-bogo',
  templateUrl: './create-or-edit-discount-bogo.component.html',
  styleUrls: ['./create-or-edit-discount-bogo.component.scss']
})
export class CreateOrEditDiscountBogoComponent implements OnInit {
  DiscountBogo: DiscountBogoVM = new DiscountBogoVM();
  title? : string;
  action = 0;
  discountBogos:DiscountBogoVM[]=[];
  restaurants:RestaurantVM[]=[];
  
  

 constructor(private restaurantService:RestaurantService, public dialogref: MatDialogRef<CreateOrEditDiscountBogoComponent>,
    @Inject(MAT_DIALOG_DATA) public data?: any){
    
     
    }
    
  ngOnInit(): void {
this.restaurantService.getAllRestaurants().subscribe(res=>{this.restaurants=res})
    if(this.action == 1) {
      this.title = 'Edit DiscountBogo'
      this.DiscountBogo = this.data;
    }
    else {
      this.title = 'Add  DiscountBogo'
    }
  }

  save(){
    // if(this.action == 1) {
      
    //   this.DiscountBogoservice.updateDiscountBogo(this.DiscountBogo).subscribe(
    //     () => {
    //       this.dialogref.close();
    //     }
    //   )
    // }
    // else {
      
    //   this.DiscountBogoservice.addDiscountBogo(this.DiscountBogo).subscribe(
    //     () => {
    //       this.dialogref.close();
    //     }
    //   )
    // }
  }

}



