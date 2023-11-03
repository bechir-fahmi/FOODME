import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { PaymentTypeVM } from 'app/routes/ReferentialData/Models/GlobalSettings/PaymentMethodVM';
import { PaymentTypeService } from 'app/routes/ReferentialData/Services/GlobalSettings/PaymentMethod.service';

@Component({
  selector: 'app-create-or-edit-payment-method',
  templateUrl: './create-or-edit-payment-method.component.html',
  styleUrls: ['./create-or-edit-payment-method.component.scss']
})
export class CreateOrEditPaymentMethodComponent implements OnInit {
  PaymentType: PaymentTypeVM = new PaymentTypeVM();
  title? : string;
  action = 0;
  

 constructor(private paymentTypeService:PaymentTypeService, public dialogref: MatDialogRef<CreateOrEditPaymentMethodComponent>,private route: ActivatedRoute,private router: Router,
    @Inject(MAT_DIALOG_DATA) public data?: any){
     
    }
    
  ngOnInit(): void {

    if (this.route.snapshot.url[this.route.snapshot.url.length-2].toString().includes("update"))
    {
     this.title = 'Edit Payment Method'
      // this.paymentTypeService.getPaymentType(Number(this.route.snapshot.url[this.route.snapshot.url.length-1])).subscribe(res=>{this.PaymentType=res});
   }
   if (this.route.snapshot.url[this.route.snapshot.url.length-2].toString().includes("view"))
   {
     
    this.title = 'View Payment Method'
    //  this.paymentTypeService.getPaymentType(Number(this.route.snapshot.url[this.route.snapshot.url.length-1])).subscribe(res=>{this.PaymentType=res});
  }
  if(this.route.snapshot.url[this.route.snapshot.url.length-1].toString().includes("create")) {
       this.title = 'Add Payment Method'
     }
   }
  

  save(){
    if(this.action == 1) {
      
      this.paymentTypeService.updatePaymentType(this.PaymentType).subscribe(
        () => {
          this.dialogref.close();
        }
      )
    }
    else {
      
      this.paymentTypeService.addPaymentType(this.PaymentType).subscribe(
        () => {
          this.dialogref.close();
        }
      )
    }
  }

}

