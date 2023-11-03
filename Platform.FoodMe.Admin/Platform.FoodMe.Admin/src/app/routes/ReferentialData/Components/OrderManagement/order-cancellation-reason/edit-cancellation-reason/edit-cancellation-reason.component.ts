import { Component } from '@angular/core';
import {OrderCancellationReasonDto} from "../../../../Models/OrderManagement/OrderCancellationReasonDto";
import {MatDialogRef} from "@angular/material/dialog";
import {OrderCancellationResonService} from "../../../../Services/OrderManagement/order-cancellation-reson.service";
import {FormControl, FormGroup} from "@angular/forms";
import {PromoBogoVM} from "../../../../Models/Promotion/PromoBogoVM";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-edit-cancellation-reason',
  templateUrl: './edit-cancellation-reason.component.html',
  styleUrls: ['./edit-cancellation-reason.component.scss']
})
export class EditCancellationReasonComponent {
  orderCancellationReasonDto:OrderCancellationReasonDto;
  object: any;
  page="";
  id: number;
  promoBogoVM:PromoBogoVM;
  showUpdate!: boolean;
  formValue = new FormGroup({
    reaSDESCRIPTION: new FormControl(''),
    descriptionEnName: new FormControl(''),
    reaSCONCEPTID: new FormControl(0),
    reaSID: new FormControl(0),
  })
  constructor(
    public dialogRef: MatDialogRef<EditCancellationReasonComponent>,
    public orderCancellationResonService: OrderCancellationResonService,private route: ActivatedRoute) { }
  ngOnInit(): void {
    this.page = <string>this.route.snapshot.paramMap.get('page');
    this.object = JSON.parse(<string>this.route.snapshot.paramMap.get('my_object'));
    if(this.object){
      this.id=this.object['id'];
      this.formValue.patchValue({
        reaSDESCRIPTION: this.object['reaS_DESCRIPTION'],
        descriptionEnName: this.object['descriptionEnName'],
        reaSCONCEPTID: this.object['reaS_CONCEPTID'],
        reaSID:this.object['reaS_ID'] ,

      })
    }
  }
  showUpdateButton(){
    this.showUpdate=true
  }
  update() {
    let CancellationReson = {
      id: this.id,
      reaS_DESCRIPTION: this.formValue.value.reaSDESCRIPTION,
      reaS_ID: this.formValue.value.reaSID,
      reaS_CONCEPTID: this.formValue.value.reaSCONCEPTID,
      descriptionEnName: this.formValue.value.descriptionEnName,

    }
    this.orderCancellationResonService.UpdateOrderCancellation(CancellationReson)
      .subscribe(res => {
          let ref = document.getElementById('cancel');
          ref?.click();
          this.formValue.reset();
        },
        err => {
        }
      );
  }

  save() {
    let CancellationResonDto = {
      reaS_DESCRIPTION: this.formValue.value.reaSDESCRIPTION,
      descriptionEnName: this.formValue.value.descriptionEnName,
      reaS_CONCEPTID:Number(this.formValue.value.reaSCONCEPTID) ,
      reaS_ID:Number (this.formValue.value.reaSID),

    }
    this.orderCancellationResonService.postOrderCancellation(CancellationResonDto)
      .subscribe(res => {
          console.log("CancellationResonDtoo est ", CancellationResonDto)
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

