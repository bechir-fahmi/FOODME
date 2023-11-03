import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {BehaviorSubject, Observable} from 'rxjs';
import { HelperService } from '../helper.service';
import { environment } from '@env/environment';
import { CarhopAddressVM } from '../../Models/BrandManagement/CarhopAddressVM';
import {OrderActionReasonDto} from "../../Models/OrderManagement/OrderActionReasonDto";


@Injectable({
  providedIn: 'root'
})
export class CarhopAddressService {
  private helper: HelperService<CarhopAddressVM>;
  private messageSource = new BehaviorSubject('default message');
  currentMessage = this.messageSource.asObservable();
  private http: HttpClient;

  constructor( helper: HelperService<CarhopAddressVM>,http: HttpClient) {
    this.helper = helper;
    this.http = http;
  }

  // getCarhopAddresss() {
  //   return this.helper.get('/CarhopAddress/GetAllCarhopAddresss');
  // }
  public getCarhopAddresss(page: number, size: number): Observable<CarhopAddressVM[]> {
    return this.http.get<CarhopAddressVM[]>(`${environment.API}/CarhopAddress/GetAllCarhopAddress?PageNumber=${page}&PageSize=${size}`);
  }

  getCarhopAddress(CarhopAddressId : number) {

    return this.http.get<CarhopAddressVM>(`${environment.API}/CarhopAddress/GetCarhopAddress/id/${CarhopAddressId}`);
  }

  deleteCarhopAddress(CarhopAddressId: number) {
    return this.helper.delete(`/CarhopAddress/RemoveCarhopAddress/${CarhopAddressId}`);
  }

  addCarhopAddress(CarhopAddress: any) {
    return this.http.post<CarhopAddressVM>(`${environment.API}/CarhopAddress/AddCarhopAddress`, CarhopAddress);
  }

  updateCarhopAddress(CarhopAddress: CarhopAddressVM) {
  return this.helper.update('/CarhopAddress/UpdateCarhopAddress', CarhopAddress);
  }



}
