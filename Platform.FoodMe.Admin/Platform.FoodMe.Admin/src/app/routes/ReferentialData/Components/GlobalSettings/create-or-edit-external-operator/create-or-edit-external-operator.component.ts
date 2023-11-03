import { Component, Inject, OnInit } from '@angular/core';
import { ExternalOperatorVM } from 'app/routes/ReferentialData/Models/GlobalSettings/ExternalOperatorVM';
import { CreateOrEditPaymentMethodComponent } from '../create-or-edit-payment-method/create-or-edit-payment-method.component';
import { ActivatedRoute, Router } from '@angular/router';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-create-or-edit-external-operator',
  templateUrl: './create-or-edit-external-operator.component.html',
  styleUrls: ['./create-or-edit-external-operator.component.scss']
})
export class CreateOrEditExternalOperatorComponent implements OnInit {
  ExternalOperator: ExternalOperatorVM = new ExternalOperatorVM();
  title? : string;
  action = 0;
  

 constructor( public dialogref: MatDialogRef<CreateOrEditPaymentMethodComponent>,private route: ActivatedRoute,private router: Router,
    @Inject(MAT_DIALOG_DATA) public data?: any){
     
    }
    
  ngOnInit(): void {

    if (this.route.snapshot.url[this.route.snapshot.url.length-2].toString().includes("update"))
    {
     this.title = 'Edit Aggregator'
      // this.ExternalOperatorService.getExternalOperator(Number(this.route.snapshot.url[this.route.snapshot.url.length-1])).subscribe(res=>{this.ExternalOperator=res});
   }
   if (this.route.snapshot.url[this.route.snapshot.url.length-2].toString().includes("view"))
   {
     
    this.title = 'View Aggregator'
    //  this.ExternalOperatorService.getExternalOperator(Number(this.route.snapshot.url[this.route.snapshot.url.length-1])).subscribe(res=>{this.ExternalOperator=res});
  }
  if(this.route.snapshot.url[this.route.snapshot.url.length-1].toString().includes("create")) {
       this.title = 'Add Aggregator'
     }
   }
  

  save(){
    // if(this.action == 1) {
      
    //   this.ExternalOperatorService.updateExternalOperator(this.ExternalOperator).subscribe(
    //     () => {
    //       this.dialogref.close();
    //     }
    //   )
    // }
    // else {
      
    //   this.ExternalOperatorService.addExternalOperator(this.ExternalOperator).subscribe(
    //     () => {
    //       this.dialogref.close();
    //     }
    //   )
    // }
  }

}


