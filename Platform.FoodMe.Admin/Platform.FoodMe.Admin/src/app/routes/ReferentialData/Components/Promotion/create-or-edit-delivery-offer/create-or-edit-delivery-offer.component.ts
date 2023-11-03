import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DeliveryOfferVM } from 'app/routes/ReferentialData/Models/Promotion/DeliveryOfferVM';
import {FormControl, FormGroup} from "@angular/forms";
import {DeliveryOfferservice} from "../../../Services/DeliveryOffer.service";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-create-or-edit-delivery-offer',
  templateUrl: './create-or-edit-delivery-offer.component.html',
  styleUrls: ['./create-or-edit-delivery-offer.component.scss']
})
export class CreateOrEditDeliveryOfferComponent implements OnInit {
  page=""
  object: any
  DeliveryOfferVM: DeliveryOfferVM;
  formValue = new FormGroup({
    name: new FormControl(''),
    activationDate: new FormControl(''),
    expirationDate: new FormControl(''),
    pricingType: new FormControl(0),
    deliveryAmount: new FormControl(0),
    status: new FormControl(false),
  })



 constructor( public dialogref: MatDialogRef<CreateOrEditDeliveryOfferComponent>,public  deliveryOfferservice:DeliveryOfferservice,private route: ActivatedRoute,
    @Inject(MAT_DIALOG_DATA) public data?: any){

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
        deliveryAmount:this.object['deliveryAmount'] ,
        status:this.object['status'] ,
      })
    }
  }


  save() {
    let DeliveryOffer = {
      name: this.formValue.value.name,
      activationDate: this.formValue.value.activationDate,
      expirationDate: this.formValue.value.expirationDate,
      pricingType:Number(this.formValue.value.pricingType),
      deliveryAmount:Number(this.formValue.value.deliveryAmount),
      status: this.formValue.value.status==false?0:1
    }
    this.deliveryOfferservice.addDeliveryOffer(DeliveryOffer)
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

  update() {

  }
}



