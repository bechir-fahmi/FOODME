import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { Observable } from 'rxjs';
import {OrderActionReasonDto} from '../../Models/OrderManagement/OrderActionReasonDto';


@Injectable({
  providedIn: 'root'
})
export class OrderActionReasonService {
  constructor(private http:HttpClient) { }

  getOrderAction(page: number, size: number)
  {
    return this.http.get<OrderActionReasonDto[]>(`${environment.API}/OrderActionReason/GetAllOrderActionReason?PageNumber=${page}&PageSize=${size}`);
  }

  public postOrderAction(orderActionReasonDto: any) {
    return this.http.post<OrderActionReasonDto>(`${environment.API}/OrderActionReason/AddOrderActionReason`, orderActionReasonDto);
  }
  public deleteOrderAction(id: number) {
    return this.http.delete<any>(`${environment.API}/OrderActionReason/RemoveOrderActionReason/${id}`)
  }

  public UpdateOrderAction(orderActionReasonDto: any) {
    return this.http.put<any>(`${environment.API}/OrderActionReason/UpdateOrderActionReason`, orderActionReasonDto)
  }

  getActionReason(reasonDescription:any, resonAction:any, status:any, page:number, size:number) {
    let apiUrl = `${environment.API}/OrderActionReason/GetFilteredData?`;

    if (reasonDescription) {
      apiUrl += `ReasonDescription=${reasonDescription}&`;
    }
    if (resonAction) {
      apiUrl += `ReasonAction=${resonAction}&`;
    }
    if (status) {
      apiUrl += `Status=${status}&`;
    }
    apiUrl += `PageNumber=${page}&PageSize=${size}`;

    return this.http.get<OrderActionReasonDto[]>(apiUrl);
  }
}
