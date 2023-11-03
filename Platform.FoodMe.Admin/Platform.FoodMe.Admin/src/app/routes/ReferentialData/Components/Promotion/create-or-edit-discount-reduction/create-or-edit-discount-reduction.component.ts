import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DiscountReductionVM } from 'app/routes/ReferentialData/Models/Promotion/DiscountReductionVM';
import { DiscountType } from 'app/routes/ReferentialData/Models/Promotion/DiscountType';
import { OrderMode } from 'app/routes/ReferentialData/Models/Promotion/OrderMode';
import { PromoType } from 'app/routes/ReferentialData/Models/Promotion/PromoType';
import {couponType, discountType} from "../create-or-edit-customer-coupon/create-or-edit-customer-coupon.component";
import {FormControl, FormGroup} from "@angular/forms";
import {CouponService} from "../../../Services/Coupon.service";
import {DiscountReductionservice} from "../../../Services/DiscountReduction.service";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-create-or-edit-discount-reduction',
  templateUrl: './create-or-edit-discount-reduction.component.html',
  styleUrls: ['./create-or-edit-discount-reduction.component.scss']
})
export class CreateOrEditDiscountReductionComponent implements OnInit {
  DiscountReduction: DiscountReductionVM;
  page=""
  object: any
  discountTypes: discountType[] = [];
  formValue = new FormGroup({
    name: new FormControl(''),
    activationDate: new FormControl(''),
    expirationDate: new FormControl(''),
    pricingType: new FormControl(0),
    discount: new FormControl(0),
    sunday: new FormControl(false),
    monday: new FormControl(false),
    tuesday: new FormControl(false),
    wednesday: new FormControl(false),
    thursday: new FormControl(false),
    saturday: new FormControl(false),
    friday: new FormControl(false),
    status: new FormControl(false),

  })

  constructor(public dialogref: MatDialogRef<CreateOrEditDiscountReductionComponent>, public discountReductionService: DiscountReductionservice,private route: ActivatedRoute,
              @Inject(MAT_DIALOG_DATA) public data?: any) {
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
        activationDate:this.object['activationDate'].slice(0,10),
        expirationDate:this.object['expirationDate'].slice(0,10),
        pricingType:this.object['pricingType'] ,
        status:this.object['status'] ,
      })
    }
  }
  update() {

  }

  save() {
    let discountReduction = {
      name: this.formValue.value.name,
      activationDate: this.formValue.value.activationDate,
      expirationDate: this.formValue.value.expirationDate,
      pricingType: Number(this.formValue.value.pricingType),
      discount: Number(this.formValue.value.discount),
      status: this.formValue.value.status == false ? 0 : 1,
      sunday: this.formValue.value.sunday,
      monday: this.formValue.value.monday,
      tuesday: this.formValue.value.tuesday,
      wednesday: this.formValue.value.wednesday,
      thursday: this.formValue.value.thursday,
      friday: this.formValue.value.friday,
      saturday: this.formValue.value.saturday,


    }
    this.discountReductionService.addDiscountReduction(discountReduction)
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



