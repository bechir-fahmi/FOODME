import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import {OrderActionReasonDto} from "../../../Models/OrderManagement/OrderActionReasonDto";
import {FormControl, FormGroup} from "@angular/forms";
import {OrderActionReasonService} from "../../../Services/OrderManagement/OrderActionReason.service";


@Component({
  selector: 'app-create-or-edit-order-action-reason',
  templateUrl: './create-or-edit-order-action-reason.component.html',
  styleUrls: ['./create-or-edit-order-action-reason.component.scss']
})
export class CreateOrEditOrderActionReasonComponent implements OnInit {
  isChecked = 0;
  showUpdate!: boolean;
  size: number=5;
  page=""
  object: any
  id: number;
  reasonAction: any;
  orderActionReasonDto:OrderActionReasonDto;
  formValue = new FormGroup({
    reasonDescription: new FormControl(''),
    reasonAction: new FormControl(0),
    reasonStatus: new FormControl(0),
  })
  constructor(
    public dialogRef: MatDialogRef<CreateOrEditOrderActionReasonComponent>,private route: ActivatedRoute,
    public orderActionService: OrderActionReasonService) { }

  ngOnInit(): void {
    this.page = <string>this.route.snapshot.paramMap.get('page');
    this.object = JSON.parse(<string>this.route.snapshot.paramMap.get('my_object'));

    if(this.object){
      this.id=this.object['id'];
      this.reasonAction=this.object['reasonAction'];
      this.formValue.patchValue({

        reasonDescription: this.object['reasonDescription'],
        reasonAction:this.object['reasonAction'],
        reasonStatus:this.object['status'] ,
      })
    }

  }
  onNoClick(): void {
    this.dialogRef.close();
  }
  changeAttributeValue() {
    this.isChecked == 0 ? this.isChecked=1 : this.isChecked=0
  }
  showUpdateButton(){
    this.showUpdate=true
  }
  editOrderAction(row: OrderActionReasonDto) {
    this.orderActionReasonDto = row;

    this.orderActionReasonDto.id = row.id;
    this.formValue.patchValue({

      reasonAction:row.reasonAction,
      reasonDescription: row.reasonDescription,
      reasonStatus: row.status,
    });
  }

  update() {
    let orderActionReasonDto = {
      id: this.id,
      reasonAction: Number(this.formValue.value.reasonAction),
      reasonDescription: this.formValue.value.reasonDescription,
      status: this.isChecked
    }
    this.orderActionService.UpdateOrderAction(orderActionReasonDto)
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
    let orderActionReasonDto = {
      reasonAction: Number(this.formValue.value.reasonAction),
      reasonDescription: this.formValue.value.reasonDescription,
      status: this.isChecked
    }
    this.orderActionService.postOrderAction(orderActionReasonDto)
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
