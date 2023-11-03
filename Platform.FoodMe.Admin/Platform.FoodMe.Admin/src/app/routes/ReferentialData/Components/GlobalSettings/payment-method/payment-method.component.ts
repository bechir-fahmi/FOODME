import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { PaymentTypeVM } from 'app/routes/ReferentialData/Models/GlobalSettings/PaymentMethodVM';
import { PaymentTypeService } from 'app/routes/ReferentialData/Services/GlobalSettings/PaymentMethod.service';

@Component({
  selector: 'app-payment-method',
  templateUrl: './payment-method.component.html',
  styleUrls: ['./payment-method.component.scss']
})
export class PaymentMethodComponent  implements OnInit {


  displayedColumns = ['name','actions'];
  dataSource: MatTableDataSource<PaymentTypeVM>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private paymentTypeService:PaymentTypeService,private router: Router,private route: ActivatedRoute) {
  }
   PaymentTypes: PaymentTypeVM[] = [];

  ngOnInit() {
    this.getPaymentTypes();
  }

  editPaymentType(PaymentType: PaymentTypeVM)
  {
  //  let navigateToEdit=this.route.snapshot._routerState.url.concat(`/updatePaymentType/${PaymentType.id}`)
   // this.router.navigate([navigateToEdit]);
  }

  createPaymentType()
  {
    // let navigateToCreate=this.route.snapshot._routerState.url.concat("/createPaymentType");
    // this.router.navigate([navigateToCreate]);
  }

  refresh(): void {
    this.paymentTypeService.getPaymentTypes().subscribe(res =>{
      this.dataSource.data = res;
    });
  }
  getPaymentTypes()
  {

    this.paymentTypeService.getPaymentTypes().subscribe(res =>{
      this.PaymentTypes = res;
      this.dataSource = new MatTableDataSource(this.PaymentTypes);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }
  deletePaymentType(id: number){
    this.paymentTypeService.deletePaymentType(id).subscribe(
      () => {
        this.refresh();
      });
  }

}







