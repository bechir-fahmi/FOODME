import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { RestaurantDto } from '@shared/Models/RestaurantData/RestaurantDto';

@Injectable({
  providedIn: 'root'
})
export class RestourantService {

  constructor(private http:HttpClient) { }
  getrestaurant()
  {
    return this.http.get<RestaurantDto[]>(`${environment.API}/Restaurant/GetAllRestaurant`);
  }

}
