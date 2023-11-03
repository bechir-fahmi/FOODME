import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { SubscriptionFlowValidation } from '@shared/Models/SubscriptionFlowValidation';
import { BrandGroupVM } from 'app/routes/ReferentialData/Models/RestaurantData/BrandStructure/BrandGroupVM';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class BrandGroupService {
  private http: HttpClient;

  constructor(http: HttpClient) {
    this.http = http;
  }

  getAllBrandGroups() {
    return this.http.get<BrandGroupVM[]>(`${environment.API}/BrandGroup/GetAllBrandGroups`);
  }

  getBrandGroup(brandGroupId : number) {
    return this.http.get<BrandGroupVM>(`${environment.API}/BrandGroup/GetBrandGroups/id/${brandGroupId}`);
  }

  getBrandGroupByFlowValidation(flowValidation : SubscriptionFlowValidation) {
    return this.http.get<BrandGroupVM[]>(`${environment.API}/BrandGroup/GetBrandGroups/SubscriptionFlowValidation/${flowValidation}`);
  }

  addBrandGroup(brandGroup: BrandGroupVM): Observable<any> {
    const headers = { 'content-type': 'application/json'}
    const body=JSON.stringify(brandGroup);
    return this.http.post(`${environment.API}/BrandGroup/AddBrandGroup`, body, {'headers':headers});
  }

  updateBrandGroup(brandGroup: BrandGroupVM) {
    return this.http.put(`${environment.API}/BrandGroup/UpdateBrandGroup`, brandGroup)
  }

  deleteBrandGroup(brandGroupId: number) {
    return this.http.delete(`${environment.API}/BrandGroup/RemoveBrandGroup/${brandGroupId}`);
  }
}
