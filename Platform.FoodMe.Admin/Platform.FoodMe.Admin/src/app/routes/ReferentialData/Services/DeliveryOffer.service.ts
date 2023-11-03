import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {BehaviorSubject, Observable} from 'rxjs';
import { environment } from '@env/environment';
import {HelperService} from "./helper.service";
import {DeliveryOfferVM} from "../Models/Promotion/DeliveryOfferVM";



@Injectable({
  providedIn: 'root'
})
export class DeliveryOfferservice {
  private helper: HelperService<DeliveryOfferVM>;
  private messageSource = new BehaviorSubject('default message');
  currentMessage = this.messageSource.asObservable();
  private http: HttpClient;

  constructor( helper: HelperService<DeliveryOfferVM>,http: HttpClient) {
    this.helper = helper;
    this.http = http;
  }

  public getDeliveryOffer(page: number, size: number): Observable<DeliveryOfferVM[]> {
    return this.http.get<DeliveryOfferVM[]>(`${environment.API}/DeliveryOffer/GetAllDeliveryOffer?PageNumber=${page}&PageSize=${size}`);
  }

  getDeliveryOfferID(CouponId : number) {

    return this.http.get<DeliveryOfferVM>(`${environment.API}/DeliveryOffer/GetDeliveryOffer/id/${CouponId}`);
  }

  addDeliveryOffer(Coupon: any) {
    return this.http.post<DeliveryOfferVM>(`${environment.API}/DeliveryOffer/AddDeliveryOffer`, Coupon);
  }

  updateDeliveryOffer(promoBogo:any) {
  return this.helper.update('/DeliveryOffer/UpdateDeliveryOffer',promoBogo);
  }

  public deleteDeliveryOffer(id: number) {
    return this.http.delete<any>(`${environment.API}/DeliveryOffer/RemoveDeliveryOffer/${id}`)
  }

}
