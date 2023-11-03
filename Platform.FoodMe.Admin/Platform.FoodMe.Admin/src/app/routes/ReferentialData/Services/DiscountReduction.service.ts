import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {BehaviorSubject, Observable} from 'rxjs';

import { environment } from '@env/environment';
import {HelperService} from "./helper.service";
import {CouponVM} from "../Models/Promotion/CouponVM";
import {DiscountReductionVM} from "../Models/Promotion/DiscountReductionVM";



@Injectable({
  providedIn: 'root'
})
export class DiscountReductionservice {
  private helper: HelperService<DiscountReductionVM>;
  private messageSource = new BehaviorSubject('default message');
  currentMessage = this.messageSource.asObservable();
  private http: HttpClient;

  constructor( helper: HelperService<DiscountReductionVM>,http: HttpClient) {
    this.helper = helper;
    this.http = http;
  }
  public getDiscountReduction(page: number, size: number): Observable<DiscountReductionVM[]> {
    return this.http.get<DiscountReductionVM[]>(`${environment.API}/DiscountReduction/GetAllDiscountReduction?PageNumber=${page}&PageSize=${size}`);
  }

  getDiscountReductionID(discountReductionVMId : number) {

    return this.http.get<DiscountReductionVM>(`${environment.API}/DiscountReduction/GetDiscountReduction/id/${discountReductionVMId}`);
  }

  addDiscountReduction(discountReduction: any) {
    return this.http.post<DiscountReductionVM>(`${environment.API}/DiscountReduction/AddDiscountReduction`, discountReduction);
  }

  updateDiscountReduction(discountReduction:any) {
  return this.helper.update('/DiscountReduction/UpdateDiscountReduction',discountReduction);
  }

  public deleteDiscountReduction(id: number) {
    return this.http.delete<any>(`${environment.API}/DiscountReduction/RemoveDiscountReduction/${id}`)
  }

}
