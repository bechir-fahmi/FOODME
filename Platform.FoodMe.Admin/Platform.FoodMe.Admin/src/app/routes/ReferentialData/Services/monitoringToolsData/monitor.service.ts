import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { OrderFilterVM } from '../../Models/OrderManagement/OrderFilterVM';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MonitorService {

  constructor(private http: HttpClient) {
    this.http = http;
  }

  getAllOrderFiltre() {
    return this.http.get<any[]>(`${environment.API}/OrderFilter/GetAllOrderFilter`);
  }

  addOrderFilter(order: OrderFilterVM): Observable<OrderFilterVM> {
    const headers = { 'content-type': 'application/json'}
    const body=JSON.stringify(order);
    return this.http.post<OrderFilterVM>(`${environment.API}/OrderFilter/AddOrderFilter`, body, {'headers':headers});
  }

  deleteOrderFilter(orderFilterId: number) {
    return this.http.delete(`${environment.API}/OrderFilter/RemoveOrderFilter/${orderFilterId}`);
  }
}
