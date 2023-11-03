import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { SubscriptionFlowValidation } from '@shared/Models/SubscriptionFlowValidation';
import { Observable } from 'rxjs';
import { RestaurantVM } from '../../Models/RestaurantData/RestaurantVM';


@Injectable({
  providedIn: 'root'
})
export class RestaurantService {
  private http: HttpClient;

  constructor(http: HttpClient) {
    this.http = http;
  }

  getAllRestaurants() {
    return this.http.get<RestaurantVM[]>(`${environment.API}/Restaurant/GetAllRestaurants`);
  }

  getRestaurant(RestaurantId : number) {
    return this.http.get<RestaurantVM>(`${environment.API}/Restaurant/GetRestaurantById/${RestaurantId}`);
  }

  getRestaurantsByBrandId(brandId : number) {
    return this.http.get<RestaurantVM[]>(`${environment.API}/Restaurant/GetRestaurantByBrandId/${brandId}`);
  }

  getRestaurantByFlowValidation(flowValidation : SubscriptionFlowValidation) {
    return this.http.get<RestaurantVM[]>(`${environment.API}/Restaurant/GetRestaurants/SubscriptionFlowValidation/${flowValidation}`);
  }

  addRestaurant(Restaurant: RestaurantVM): Observable<any> {
    const headers = { 'content-type': 'application/json'}
    const body=JSON.stringify(Restaurant);
    return this.http.post(`${environment.API}/Restaurant/AddRestaurant`, body, {'headers':headers});
  }

  updateRestaurant(Restaurant: RestaurantVM) {
    return this.http.put(`${environment.API}/Restaurant/UpdateRestaurant`, Restaurant)
  }

  deleteRestaurant(RestaurantId: number) {
    return this.http.delete(`${environment.API}/Restaurant/RemoveRestaurant/${RestaurantId}`);
  }
}