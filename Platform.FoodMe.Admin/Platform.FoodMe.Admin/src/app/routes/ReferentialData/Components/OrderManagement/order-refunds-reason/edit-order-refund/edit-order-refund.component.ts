import { Component } from '@angular/core';
import {OrderRefundsReasonDto} from "../../../../Models/OrderManagement/OrderRefundsReasonDto";
import {MatDialogRef} from "@angular/material/dialog";
import {OrderRefundsReasonService} from "../../../../Services/OrderRefundsReasonService/orderRefundsReason.service";
import {FormControl, FormGroup} from "@angular/forms";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-edit-order-refund',
  templateUrl: './edit-order-refund.component.html',
  styleUrls: ['./edit-order-refund.component.scss']
})
export class EditOrderRefundComponent {
  isChecked = 0;
  showUpdate!: boolean;
  page=""
  object: any
  id: number;
  orderRefundsReasonDto:OrderRefundsReasonDto;
  formValue = new FormGroup({
    refundsDescription: new FormControl(''),
    reasonStatus: new FormControl(0),
  })
  constructor(
    public dialogRef: MatDialogRef<EditOrderRefundComponent>,
    public orderRefundsReasonService: OrderRefundsReasonService,
    private route: ActivatedRoute) { }
  ngOnInit(): void {
    this.page = <string>this.route.snapshot.paramMap.get('page');
    this.object = JSON.parse(<string>this.route.snapshot.paramMap.get('my_object'));
    if(this.object){
      this.id=this.object['id'];
      this.formValue.patchValue({
        refundsDescription: this.object['refundsDescription'],
        reasonStatus:this.object['status'] ,
      })
    }
  }
  changeAttributeValue() {
    this.isChecked == 0 ? this.isChecked=1 : this.isChecked=0
  }
  showUpdateButton(){
    this.showUpdate=true
  }
  editOrderRefunds(row: OrderRefundsReasonDto) {
    this.orderRefundsReasonDto = row;

    this.orderRefundsReasonDto.id = row.id;
    this.formValue.patchValue({

      refundsDescription:row.refundsDescription,
      reasonStatus: row.status,
    });
  }

  update() {
    let orderActionRefund = {
      id: this.id,
      reasonDescription: this.formValue.value.refundsDescription,
      status: this.isChecked
    }
    this.orderRefundsReasonService.updateOrderRefundsReason(orderActionRefund)
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
    let orderRefundsReasonDto = {
      refundsDescription: this.formValue.value.refundsDescription,
      status: this.isChecked
    }
    this.orderRefundsReasonService.postOrderRefundsReason(orderRefundsReasonDto)
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
