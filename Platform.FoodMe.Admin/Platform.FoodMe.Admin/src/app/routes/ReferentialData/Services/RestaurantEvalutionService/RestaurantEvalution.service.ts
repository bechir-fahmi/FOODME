import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import {RestaurantEvalutionDto} from "../../Models/CustomerManagement/RestaurantEvalutionDto";



@Injectable({
  providedIn: 'root'
})
export class RestaurantEvalutionService {

  constructor(private http:HttpClient) { }


  getRestaurantEvalution()
  {
    return this.http.get<RestaurantEvalutionDto[]>(`${environment.API}/RestaurantEvalutions/GetAllRestaurantEvalutions`);
  }

}
