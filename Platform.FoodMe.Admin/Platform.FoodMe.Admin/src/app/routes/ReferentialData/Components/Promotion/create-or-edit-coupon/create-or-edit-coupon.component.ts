import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CouponVM } from 'app/routes/ReferentialData/Models/Promotion/CouponVM';
import { CustomerCouponVM } from 'app/routes/ReferentialData/Models/Promotion/CustomerCouponVM';
import { UserVM } from 'app/routes/ReferentialData/Models/User Data/UserVM';
import { UserService } from 'app/routes/ReferentialData/Services/UserData/User/user.service';

@Component({
  selector: 'app-create-or-edit-coupon',
  templateUrl: './create-or-edit-coupon.component.html',
  styleUrls: ['./create-or-edit-coupon.component.scss']
})
export class CreateOrEditCouponComponent implements OnInit {
  Coupon: CouponVM = new CouponVM();
  title? : string;
  action = 0;
  users:UserVM[]=[];
  coupons:CustomerCouponVM[]=[];

 constructor(private userService:UserService, public dialogref: MatDialogRef<CreateOrEditCouponComponent>,
    @Inject(MAT_DIALOG_DATA) public data?: any){
     
    }
    
  ngOnInit(): void {
this.userService.getAllUsers().subscribe(res=>{this.users=res})
    if(this.action == 1) {
      this.title = 'Edit Coupon'
      this.Coupon = this.data;
    }
    else {
      this.title = 'Add Customer coupon'
    }
  }

  save(){
    // if(this.action == 1) {
      
    //   this.Couponservice.updateCoupon(this.Coupon).subscribe(
    //     () => {
    //       this.dialogref.close();
    //     }
    //   )
    // }
    // else {
      
    //   this.Couponservice.addCoupon(this.Coupon).subscribe(
    //     () => {
    //       this.dialogref.close();
    //     }
    //   )
    // }
  }

}


