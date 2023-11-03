import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { PromoBogoVM } from 'app/routes/ReferentialData/Models/Promotion/PromoBogoVM';
import {PromoBogoservice} from "../../../Services/PromoBogo.service";
import {FormControl, FormGroup} from "@angular/forms";
import { ActivatedRoute, Router } from '@angular/router';
import {OrderActionReasonDto} from "../../../Models/OrderManagement/OrderActionReasonDto";

@Component({
  selector: 'app-create-or-edit-promo-bogo',
  templateUrl: './create-or-edit-promo-bogo.component.html',
  styleUrls: ['./create-or-edit-promo-bogo.component.scss']
})
export class CreateOrEditPromoBogoComponent  implements OnInit {
  page=""
  object: any
  promoBogoVM:PromoBogoVM;
  formValue = new FormGroup({
    name: new FormControl(''),
    activationDate: new FormControl(""),
    expirationDate: new FormControl(""),
    pricingType: new FormControl(0),
    status: new FormControl(false),
  })




 constructor( public dialogref: MatDialogRef<CreateOrEditPromoBogoComponent>,public  promoBogoservice:PromoBogoservice,private route: ActivatedRoute){

    }

  ngOnInit(): void {
    this.page = <string>this.route.snapshot.paramMap.get('page');
    this.object = JSON.parse(<string>this.route.snapshot.paramMap.get('my_object'));
    if(this.object){
      console.log(this.object['activationDate'])
      console.log(this.object['activationDate'].slice(0,10))
      this.formValue.patchValue({
        name: this.object['name'],
        activationDate:this.object['activationDate'].slice(0,10),
        expirationDate:this.object['expirationDate'].slice(0,10),
        pricingType:this.object['pricingType'] ,
        status:this.object['status'] ,
      })
    }
  }


  save(){
    let promoBogo = {
      name: this.formValue.value.name,
      activationDate: this.formValue.value.activationDate,
      expirationDate: this.formValue.value.expirationDate,
      pricingType:Number(this.formValue.value.pricingType),
      status: this.formValue.value.status==false?0:1
    }
    this.promoBogoservice.addPromoBogo(promoBogo)
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
    let promoBogo = {
      name: this.formValue.value.name,
      activationDate: this.formValue.value.activationDate,
      expirationDate: this.formValue.value.expirationDate,
      pricingType:Number(this.formValue.value.pricingType),
      status: this.formValue.value.status==false?0:1
    }
    console.log('test before update', promoBogo);
    this.promoBogoservice.updatePromoBogo(promoBogo)
      .subscribe(res => {
        console.log('test after update', res);
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




