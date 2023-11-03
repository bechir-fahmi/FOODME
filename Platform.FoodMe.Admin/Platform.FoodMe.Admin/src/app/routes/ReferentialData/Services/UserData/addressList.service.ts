import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { Observable } from 'rxjs';
import { AddressListVM } from '../../Models/User Data/addressListVM';


@Injectable({
  providedIn: 'root'
})
export class AddressListService {
  private http: HttpClient;

  constructor(http: HttpClient) {
    this.http = http;
  }
  
  getAllAddressLists() {
    return this.http.get<AddressListVM[]>(`${environment.API}/AddressType/GetAllAddressTypes`);
  }

  getAddressList(AddressListId : number) {
    return this.http.get<AddressListVM[]>(`${environment.API}/AddressType/GetAddressType/id/${AddressListId}`);
  }

  addAddressList(AddressList: AddressListVM): Observable<any> {
    const headers = { 'content-type': 'application/json'}
    const body=JSON.stringify(AddressList);
    return this.http.post(`${environment.API}/AddressType/AddAddressType`, body, {'headers':headers});
  }

  updateAddressList(AddressList: AddressListVM) {
    return this.http.put(`${environment.API}/AddressType/UpdateAddressType`, AddressList)
  }

  deleteAddressList(AddressListId: number) {
    return this.http.delete(`${environment.API}/AddressType/RemoveAddressType/${AddressListId}`);
  }
}