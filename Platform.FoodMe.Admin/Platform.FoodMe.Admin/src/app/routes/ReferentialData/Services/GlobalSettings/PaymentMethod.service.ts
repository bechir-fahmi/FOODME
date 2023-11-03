import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { HelperService } from '../helper.service';
import { PaymentTypeVM } from '../../Models/GlobalSettings/PaymentMethodVM';
import { environment } from '@env/environment';


@Injectable({
  providedIn: 'root'
})
export class PaymentTypeService {
  private helper: HelperService<PaymentTypeVM>;
  private messageSource = new BehaviorSubject('default message');
  currentMessage = this.messageSource.asObservable();
  private http: HttpClient;

  constructor( helper: HelperService<PaymentTypeVM>,http: HttpClient) { 
    this.helper = helper;
    this.http = http;
  }

  getPaymentTypes() {
    return this.helper.get('/PaymentType/GetAllPaymentTypes');
  }

  getPaymentType(PaymentTypeId : number) {
    
    return this.http.get<PaymentTypeVM>(`${environment.API}/PaymentType/GetPaymentType/id/${PaymentTypeId}`);
  }

  deletePaymentType(PaymentTypeId: number) {
    return this.helper.delete(`/PaymentType/RemoveLanguage/${PaymentTypeId}`);
  }

  addPaymentType(PaymentType: PaymentTypeVM) {
    return this.helper.add('/PaymentType/AddPaymentType', PaymentType);
  }

  updatePaymentType(PaymentType: PaymentTypeVM) {
  return this.helper.update('/PaymentType/AddPaymentType', PaymentType);
  }



}