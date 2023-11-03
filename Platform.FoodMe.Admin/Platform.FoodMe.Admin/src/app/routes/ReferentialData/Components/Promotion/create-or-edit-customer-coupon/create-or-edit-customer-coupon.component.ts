import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CouponType } from 'app/routes/ReferentialData/Models/Promotion/CouponType';
import { CustomerCouponVM } from 'app/routes/ReferentialData/Models/Promotion/CustomerCouponVM';
import { CustomerVM } from 'app/routes/ReferentialData/Models/Promotion/CustomerVM';
import { DiscountType } from 'app/routes/ReferentialData/Models/Promotion/DiscountType';
import { RestaurantVM } from 'app/routes/ReferentialData/Models/RestaurantData/RestaurantVM';
import { RestaurantService } from 'app/routes/ReferentialData/Services/RestaurantData/Restaurant.service';
import {FormControl, FormGroup} from "@angular/forms";
import {CouponVM} from "../../../Models/Promotion/CouponVM";
import {CouponService} from "../../../Services/Coupon.service";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-create-or-edit-customer-coupon',
  templateUrl: './create-or-edit-customer-coupon.component.html',
  styleUrls: ['./create-or-edit-customer-coupon.component.scss']
})
export class CreateOrEditCustomerCouponComponent implements OnInit {
  CouponVM: CouponVM;
  title?: string;
  action = 0;
  page=""
  object: any
  type: couponType[] = [];
  discountTypes: discountType[] = [];
  formValue = new FormGroup({
    name: new FormControl(''),
    code: new FormControl(''),
    type: new FormControl(''),
    activationDate: new FormControl(''),
    expirationDate: new FormControl(''),
    discountType: new FormControl(''),
    category: new FormControl(0),
    discountPercentage: new FormControl(0),
    dontApplyLoyality: new FormControl(false),
    dontApplyOffer: new FormControl(false),
    sunday: new FormControl(false),
    monday: new FormControl(false),
    tuesday: new FormControl(false),
    wednesday: new FormControl(false),
    thursday: new FormControl(false),
    saturday: new FormControl(false),
    friday: new FormControl(false),
    active: new FormControl(false),

  })

  constructor(public dialogref: MatDialogRef<CreateOrEditCustomerCouponComponent>, public couponService: CouponService,private route: ActivatedRoute,
              @Inject(MAT_DIALOG_DATA) public data?: any) {
    const couponTypes = Object.keys(CouponType)
      .filter((v) => isNaN(Number(v)))
      .map((name) => {
        return {
          id: CouponType[name as keyof typeof CouponType],
          name,
        };
      });
    this.type = couponTypes;
    const discountTypes = Object.keys(DiscountType)
      .filter((v) => isNaN(Number(v)))
      .map((name) => {
        return {
          id: DiscountType[name as keyof typeof DiscountType],
          name,
        };
      });
    this.discountTypes = discountTypes;



  }

  ngOnInit(): void {
    this.page = <string>this.route.snapshot.paramMap.get('page');
    this.object = JSON.parse(<string>this.route.snapshot.paramMap.get('my_object'));
    if(this.object){
      this.formValue.patchValue({
        name: this.object['name'],
        type:this.object['type'] ,
        activationDate:this.object['activationDate'].slice(0,10),
        expirationDate:this.object['expirationDate'].slice(0,10),
        discountPercentage:this.object['discountPercentage'] ,
        active:this.object['status'] ,
        discountType:this.object['discount'] ,
        dontApplyLoyality:this.object['dontApplyLoyality'] ,
        dontApplyOffer:this.object['dontApplyOffer'] ,
        sunday:this.object['sunday'] ,
        monday:this.object['monday'] ,
        tuesday:this.object['tuesday'] ,
        wednesday:this.object['wednesday'] ,
        thursday:this.object['thursday'] ,
        friday:this.object['friday'] ,
        saturday:this.object['saturday'] ,
      })
    }



  }

  createCoupon() {
    let coupon= {
      name: this.formValue.value.name,
      code: this.formValue.value.code,
      type: this.formValue.value.type,
      activationDate: this.formValue.value.activationDate,
      expirationDate: this.formValue.value.expirationDate,
      discount: this.formValue.value.discountType,
      discountPercentage:Number(this.formValue.value.discountPercentage),
      category:Number(this.formValue.value.category),
      status: this.formValue.value.active==false?0:1,
      dontApplyLoyality: this.formValue.value.dontApplyLoyality,
      dontApplyOffer: this.formValue.value.dontApplyOffer,
      sunday: this.formValue.value.sunday,
      monday: this.formValue.value.monday,
      tuesday: this.formValue.value.tuesday,
      wednesday: this.formValue.value.wednesday,
      thursday: this.formValue.value.thursday,
      friday: this.formValue.value.friday,
      saturday: this.formValue.value.saturday,


    }
    this.couponService.addCoupon(coupon)
      .subscribe(res => {

          let ref = document.getElementById('cancel')
          ref?.click();
          this.formValue.reset();
        },
        err => {
          console.error(err);
        }
      )
  }
  update(){
    this.couponService.updateCoupon(this.CouponVM)
      .subscribe(res => {
          let ref = document.getElementById('cancel')
          ref?.click();
          this.formValue.reset();
        },
        err => {
          console.error(err);
        }
      )
  }

}


export interface couponType {
  id:number;
  name : String;
  }

  export interface discountType {
    id:number;
    name : String;
    }
