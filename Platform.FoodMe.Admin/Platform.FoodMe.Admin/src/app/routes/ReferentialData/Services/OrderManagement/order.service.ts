import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { OrderVM } from '../../Models/Promotion/OrderVM';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(private http: HttpClient) {
    this.http = http;
  }

  getAllOrder() {
    return this.http.get<OrderVM[]>(`${environment.API}/Order/GetAllOrder`);
  }

  geFilterOrders(order:any) {
    const body=JSON.stringify(order);
    return this.http.get<OrderVM[]>(`${environment.API}/Order/GetFilteredOrders`,order);
  }


}
