import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {BehaviorSubject, Observable} from 'rxjs';

import { environment } from '@env/environment';
import {HelperService} from "./helper.service";
import {CouponVM} from "../Models/Promotion/CouponVM";



@Injectable({
  providedIn: 'root'
})
export class CouponService {
  private helper: HelperService<CouponVM>;
  private messageSource = new BehaviorSubject('default message');
  currentMessage = this.messageSource.asObservable();
  private http: HttpClient;

  constructor( helper: HelperService<CouponVM>,http: HttpClient) {
    this.helper = helper;
    this.http = http;
  }

  // getCarhopAddresss() {
  //   return this.helper.get('/CarhopAddress/GetAllCarhopAddresss');
  // }
  public getCoupon(page: number, size: number): Observable<CouponVM[]> {
    return this.http.get<CouponVM[]>(`${environment.API}/Coupon/GetAllCoupons?PageNumber=${page}&PageSize=${size}`);
  }

  getCouponID(CouponId : number) {

    return this.http.get<CouponVM>(`${environment.API}/Coupon/GetCoupon/id/${CouponId}`);
  }

  addCoupon(Coupon: any) {
    return this.http.post<CouponVM>(`${environment.API}/Coupon/AddCoupon`, Coupon);
  }

  updateCoupon(Coupon:CouponVM) {
    return this.http.put<any>(`${environment.API}/Coupon/UpdateCoupon`,Coupon);
  }
  public deleteCoupon(id: number) {
    return this.http.delete<any>(`${environment.API}/Coupon/RemoveCoupon/${id}`)
  }

}
