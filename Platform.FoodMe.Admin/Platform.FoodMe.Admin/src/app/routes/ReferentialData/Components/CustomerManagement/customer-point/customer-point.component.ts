import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomerPointVM } from 'app/routes/ReferentialData/Models/CustomerManagement/CustomerPointVM';

@Component({
  selector: 'app-customer-point',
  templateUrl: './customer-point.component.html',
  styleUrls: ['./customer-point.component.scss']
})
export class CustomerPointComponent  implements OnInit {


  displayedColumns = ['name','orderId','earnedPointBy','points','actions'];
  dataSource: MatTableDataSource<CustomerPointVM>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private router: Router,private route: ActivatedRoute) {
  }
   CustomerPoints: CustomerPointVM[] = [];

  ngOnInit() {
    this.getCustomerPoints();
  }

  editCustomerPoint(CustomerPoint: CustomerPointVM)
  {
    // let navigateToEdit=this.route.snapshot._routerState.url.concat(`/updateCustomerPoint/${CustomerPoint.id}`)
    // this.router.navigate([navigateToEdit]);
   // let navigateToEdit=this.route.snapshot._routerState.url.concat(`/updateCustomerPoint/${CustomerPoint.id}`)
   // this.router.navigate([navigateToEdit]);
  }

  createCustomerPoint()
  {
    // let navigateToCreate=this.route.snapshot._routerState.url.concat("/createCustomerPoint");
    // this.router.navigate([navigateToCreate]);
   // let navigateToCreate=this.route.snapshot._routerState.url.concat("/createCustomerPoint");
   // this.router.navigate([navigateToCreate]);
  }

  viewCustomerPoint(CustomerPoint: CustomerPointVM)
  {
  //  let navigateToView=this.route.snapshot._routerState.url.concat(`/viewCustomerPoint/${CustomerPoint.id}`)
   // this.router.navigate([navigateToView]);
  }

  refresh(): void {
    // this.CustomerPointService.getCustomerPoints().subscribe(res =>{
    //   this.dataSource.data = res;
    // });
  }
  getCustomerPoints()
  {
    this.dataSource = new MatTableDataSource(this.CustomerPoints);

    // this.CustomerPointService.getCustomerPoints().subscribe(res =>{
    //   this.CustomerPoints = res;
    //   this.dataSource = new MatTableDataSource(this.CustomerPoints);
    //   this.dataSource.paginator = this.paginator;
    //   this.dataSource.sort = this.sort;
    // });
  }
  deleteCustomerPoint(id: number){
    // this.CustomerPointService.deleteCustomerPoint(id).subscribe(
    //   () => {
    //     this.refresh();
    //   });
  }

}









