import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import {OrderRefundsReasonDto} from "../../Models/OrderManagement/OrderRefundsReasonDto";
import {OrderActionReasonDto} from "../../Models/OrderManagement/OrderActionReasonDto";



@Injectable({
  providedIn: 'root'
})
export class OrderRefundsReasonService {

  constructor(private http:HttpClient) { }

  getorderRefundsReason(page: number, size: number)
  {
    return this.http.get<OrderRefundsReasonDto[]>(`${environment.API}/OrderRefundsReason/GetAllOrderRefundsReason?PageNumber=${page}&PageSize=${size}`);
  }

  public postOrderRefundsReason(orderRefundsReasonDto: any) {
    return this.http.post<OrderRefundsReasonDto>(`${environment.API}/OrderRefundsReason/AddOrderRefundsReason`, orderRefundsReasonDto);
  }
  public deleteOrderRefundsReason(id: number) {
    return this.http.delete<any>(`${environment.API}/OrderRefundsReason/RemoveOrderRefundsReason/${id}`)
  }

  public updateOrderRefundsReason(orderRefundsReasonDto: any) {
    return this.http.put<any>(`${environment.API}/OrderRefundsReason/UpdateOrderRefundsReason`, orderRefundsReasonDto)
  }

  getRefundsReason(RefundsDescription:any, status:any, page:number, size:number) {
    let apiUrl = `${environment.API}/OrderRefundsReason/GetAllOrderRefundsReason?`;

    if (RefundsDescription) {
      apiUrl += `RefundsDescription=${RefundsDescription}&`;
    }

    if (status) {
      apiUrl += `Status=${status}&`;
    }
    apiUrl += `PageNumber=${page}&PageSize=${size}`;

    return this.http.get<OrderRefundsReasonDto[]>(apiUrl);
  }
}
