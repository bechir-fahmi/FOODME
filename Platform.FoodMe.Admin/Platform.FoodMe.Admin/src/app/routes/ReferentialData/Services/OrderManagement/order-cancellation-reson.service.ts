import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import {OrderCancellationReasonDto} from "../../Models/OrderManagement/OrderCancellationReasonDto";


@Injectable({
  providedIn: 'root'
})
export class OrderCancellationResonService {

  constructor(private http:HttpClient) { }

  getOrderCancellation(page: number, size: number)
  {
    return this.http.get<OrderCancellationReasonDto[]>(`${environment.API}/OrderCancelReason/GetAllOrderCancelReason?PageNumber=${page}&PageSize=${size}`);
  }

  public postOrderCancellation(orderCancellationReasonDto: any) {
    return this.http.post<OrderCancellationReasonDto>(`${environment.API}/OrderCancelReason/AddOrderCancelReason`, orderCancellationReasonDto);
  }
  public deleteOrderCancellation(id: number) {
    return this.http.delete<any>(`${environment.API}/OrderCancelReason/RemoveOrderCancelReason/${id}`)
  }
  public UpdateOrderCancellation(orderCancellationReasonDto: any) {
    return this.http.put<any>(`${environment.API}/OrderCancelReason/UpdateOrderCancelReason`, orderCancellationReasonDto)
  }

}
