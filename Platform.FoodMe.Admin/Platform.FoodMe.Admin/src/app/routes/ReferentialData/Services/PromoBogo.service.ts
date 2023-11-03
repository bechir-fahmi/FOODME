import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {BehaviorSubject, Observable} from 'rxjs';

import { environment } from '@env/environment';
import {HelperService} from "./helper.service";
import {CouponVM} from "../Models/Promotion/CouponVM";
import {PromoBogoVM} from "../Models/Promotion/PromoBogoVM";



@Injectable({
  providedIn: 'root'
})
export class PromoBogoservice {
  private helper: HelperService<PromoBogoVM>;
  private messageSource = new BehaviorSubject('default message');
  currentMessage = this.messageSource.asObservable();
  private http: HttpClient;

  constructor( helper: HelperService<PromoBogoVM>,http: HttpClient) {
    this.helper = helper;
    this.http = http;
  }
  public getPromoBogo(page: number, size: number): Observable<PromoBogoVM[]> {
    return this.http.get<PromoBogoVM[]>(`${environment.API}/Buy1Get1/GetAllBuy1Get1?PageNumber=${page}&PageSize=${size}`);
  }

  getPromoBogoID(promoBogoId : number) {

    return this.http.get<PromoBogoVM>(`${environment.API}/Buy1Get1/GetBuy1Get1/id/${promoBogoId}`);
  }

  addPromoBogo(promoBogo: any) {
    return this.http.post<PromoBogoVM>(`${environment.API}/Buy1Get1/AddBuy1Get1`, promoBogo);
  }

  updatePromoBogo(PromoBogoVM:any) {
    return this.http.patch<any>(`${environment.API}/Buy1Get1/UpdateBuy1Get1`,PromoBogoVM);
  }

  public deletePromoBogo(id: number) {
    return this.http.delete<any>(`${environment.API}/Buy1Get1/RemoveBuy1Get1/${id}`)
  }

}
