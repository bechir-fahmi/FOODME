import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { SubscriptionFlowValidation } from '@shared/Models/SubscriptionFlowValidation';
import { Observable } from 'rxjs';
import { DeliveryCompanyVM } from '../../Models/RestaurantData/DeliveryCompanyVM';


@Injectable({
  providedIn: 'root'
})
export class DeliveryCompanyService {
  private http: HttpClient;

  constructor(http: HttpClient) {
    this.http = http;
  }

  getAllDeliveryCompanies() {
    return this.http.get<DeliveryCompanyVM[]>(`${environment.API}/DeliveryCompany/GetAllDeliveryCompany`);
  }

  getDeliveryCompany(DeliveryCompanyId : number) {
    
    return this.http.get<DeliveryCompanyVM>(`${environment.API}/DeliveryCompany/GetDeliveryCompany/id/${DeliveryCompanyId}`);
  }

  // getDeliveryCompany(DeliveryCompanyId : number) {
  //   return this.http.get<DeliveryCompanyVM[]>(`${environment.API}/DeliveryCompany/GetDeliveryCompanys/id/${DeliveryCompanyId}`);
  // }

  // getDeliveryCompanyByFlowValidation(flowValidation : SubscriptionFlowValidation) {
  //   return this.http.get<DeliveryCompanyVM[]>(`${environment.API}/DeliveryCompany/GetDeliveryCompanys/SubscriptionFlowValidation/${flowValidation}`);
  // }

  addDeliveryCompany(DeliveryCompany: DeliveryCompanyVM): Observable<any> {
    const headers = { 'content-type': 'application/json'}
    const body=JSON.stringify(DeliveryCompany);
    return this.http.post(`${environment.API}/CarhopAddress/AddCarhopAddress`, body, {'headers':headers});
  }

  updateDeliveryCompany(DeliveryCompany: DeliveryCompanyVM) {
    return this.http.put(`${environment.API}/CarhopAddress/UpdateCarhopAddress`, DeliveryCompany)
  }

  deleteDeliveryCompany(DeliveryCompanyId: number) {
    return this.http.delete(`${environment.API}/CarhopAddress/RemoveCarhopAddress/${DeliveryCompanyId}`);
  }
}