import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import {Observable} from "rxjs";
import {CuisineDto} from "../../Models/CuisineDto";
import {CarhopAddressVM} from "../../Models/BrandManagement/CarhopAddressVM";



@Injectable({
  providedIn: 'root'
})
export class CuisineService {

  constructor(private http:HttpClient) { }


  getCuisine()
  {
    return this.http.get<CuisineDto[]>(`${environment.API}/KitchenType/GetAllkitchens`);
  }
  public getCuisin(page: number, size: number): Observable<CuisineDto[]> {
    return this.http.get<CuisineDto[]>(`${environment.API}/KitchenType/GetAllkitchens?page=${page}&size=${size}`);
  }

  public postCuisine(cuisineDto: any) {
    return this.http.post<CuisineDto>(`${environment.API}/KitchenType/Addkitchen`, cuisineDto);
  }

  public deleteCuisine(id: number) {
    return this.http.delete<any>(`${environment.API}/KitchenType/RemoveBand/${id}`)
  }
}
