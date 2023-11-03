import { HttpClient } from '@angular/common/http';
import { Pipe, PipeTransform } from '@angular/core';
import {environment} from "@env/environment";
import {Observable} from "rxjs";

@Pipe({
  name: 'restaurantName'
})
export class RestaurantNamePipe implements PipeTransform {
  constructor(private http: HttpClient) {}
  transform(ep: any): Observable<any> {
    return this.http.get(`${environment.API}/Restaurant/GetRestaurantById/` + ep);
  }
}

