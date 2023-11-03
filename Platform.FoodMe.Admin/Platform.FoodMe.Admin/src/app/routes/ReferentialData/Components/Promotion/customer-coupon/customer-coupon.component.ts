import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomerCouponVM } from 'app/routes/ReferentialData/Models/Promotion/CustomerCouponVM';
import {CouponVM} from "../../../Models/Promotion/CouponVM";

@Component({
  selector: 'app-customer-coupon',
  templateUrl: './customer-coupon.component.html',
  styleUrls: ['./customer-coupon.component.scss']
})
export class CustomerCouponComponent implements OnInit {


  displayedColumns = ['userName', 'couponName','actions'];
  dataSource: MatTableDataSource<CouponVM>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private router: Router,private route: ActivatedRoute) {
  }
  Coupons: CouponVM[] = [];

  ngOnInit() {
    this.getCoupons();
  }

  editCoupon(Coupon: CouponVM)
  {
   //   let navigateToEdit=this.route.snapshot._routerState.url.concat(`/customerCoupon/update/${Coupon.id}`)
 //   this.router.navigate([navigateToEdit]);
  }

  createCoupon()
  {
  //  let navigateToCreate=this.route.snapshot._routerState.url.concat("/createCoupon");
   // this.router.navigate([navigateToCreate]);
  }

  viewCoupon(Coupon: CouponVM)
  {
   // let navigateToView=this.route.snapshot._routerState.url.concat(`/viewMenu/${Coupon.id}`)
    //this.router.navigate([navigateToView]);
  }







  refresh(): void {
    // this.Couponservice.getAllCoupons().subscribe(res =>{
    //   this.dataSource.data = res;
    // });
  }
  getCoupons()
  {
    this.dataSource = new MatTableDataSource(this.Coupons);
    //send request to api and get responce
    // this.Couponservice.getAllCoupons().subscribe(res =>{
    //   this.Coupons = res;
    //   this.dataSource = new MatTableDataSource(this.Coupons);
    //   this.dataSource.paginator = this.paginator;
    //   this.dataSource.sort = this.sort;
    // });
  }
  deleteCoupon(id: number){
    // this.Couponservice.deleteCoupon(id).subscribe(
    //   () => {
    //     this.refresh();
    //   });
  }

}






