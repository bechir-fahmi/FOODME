import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import {BrandVM} from 'app/routes/ReferentialData/Models/RestaurantData/BrandStructure/BrandVM';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class BrandService {
  private http: HttpClient;

  constructor(http: HttpClient) {
    this.http = http;
  }

  getAllBrands() {
    return this.http.get<BrandVM[]>(`${environment.API}/Brand/GetAllBrands`);
  }

  getBrand(brandId : number) {
    return this.http.get<any>(`${environment.API}/Brand/GetBrand/id/${brandId}`);
  }

  addBrand(brand: BrandVM): Observable<any> {
    const headers = { 'content-type': 'application/json'}
    const body=JSON.stringify(brand);
    return this.http.post(`${environment.API}/Brand/AddBrand`, body, {'headers':headers});
  }

  updateBrand(brand: BrandVM) {
    return this.http.put(`${environment.API}/Brand/UpdateBrand`, brand)
  }

  deleteBrand(brandId: number) {
    return this.http.delete(`${environment.API}/Brand/RemoveBrand/${brandId}`);
  }
}
