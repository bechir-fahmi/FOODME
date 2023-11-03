import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { CouponVM } from 'app/routes/ReferentialData/Models/Promotion/CouponVM';
import {CustomerCouponVM} from "../../../Models/Promotion/CustomerCouponVM";
import {CouponService} from "../../../Services/Coupon.service";
import {MatSelectChange} from "@angular/material/select";
import {OrderActionReasonDto} from "../../../Models/OrderManagement/OrderActionReasonDto";

@Component({
  selector: 'app-coupon',
  templateUrl: './coupon.component.html',
  styleUrls: ['./coupon.component.scss']
})
export class CouponComponent  implements OnInit {
  showInputFiltre: boolean;
  page: number=0;
  size: number=5;
  listCoupon:any;
  selectedOption: any;
  displayedColumns = ['name', 'code', 'type','category','discount','discountPercentage', 'activationDate', 'expirationDate','dontApplyLoyality','dontApplyOffer','status','actions'];
  dataSource: MatTableDataSource<CustomerCouponVM>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private router: Router,private route: ActivatedRoute, public couponService:CouponService) {
  }
  CustomerCoupons: CustomerCouponVM[] = [];


  ngOnInit() {
    this.loadData()
  }

  editCustomerCoupon(CouponVM:CouponVM)
  {
    this.router.navigate([
      'promotions/customerCoupon/createCustomerCoupon',
      {page :"edit", my_object: JSON.stringify(CouponVM) }
    ]);
  }

  createCustomerCoupon()
  {
    this.router.navigate([
      'promotions/customerCoupon/createCustomerCoupon',
      { page: 'create' },
    ]);
  }

  viewCustomerCoupon(CouponVM:CouponVM)
  {
    this.router.navigate([
      'promotions/customerCoupon/createCustomerCoupon',
      {page :"view", my_object: JSON.stringify(CouponVM) }
    ]);
  }
  loadData() {
    this.couponService.getCoupon(this.page,this.size).subscribe(
      (data: any) => {
        if (data) {
          this.listCoupon = data;
          this.dataSource = new MatTableDataSource(this.listCoupon)
          console.log('dataload')
        }
      }
    )
  }
  deleteCustomerCoupon(id: number){
    this.couponService.deleteCoupon(id).subscribe(
      () => {
       this.loadData()
      });
  }

  onSelectionChange($event: MatSelectChange) {
    let listCoupon:any =[];
    this.listCoupon.forEach((CouponVM: CouponVM) =>{
      if(CouponVM.status==$event.value){
        listCoupon.push(CouponVM)
      }
    })
    this.dataSource = new MatTableDataSource(listCoupon)

  }
}








