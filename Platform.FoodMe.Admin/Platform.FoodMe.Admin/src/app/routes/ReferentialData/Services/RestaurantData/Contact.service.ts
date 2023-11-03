import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { SubscriptionFlowValidation } from '@shared/Models/SubscriptionFlowValidation';
import { Observable } from 'rxjs';
import { ContactVM } from '../../Models/RestaurantData/ContactVM';



@Injectable({
  providedIn: 'root'
})
export class ContactService {
  private http: HttpClient;

  constructor(http: HttpClient) {
    this.http = http;
  }

  getAllContacts() {
    return this.http.get<ContactVM[]>(`${environment.API}/Contact/GetAllContact`);
  }

  getContact(ContactId : number) {
    return this.http.get<ContactVM>(`${environment.API}/Contact/GetContacts/id/${ContactId}`);
  }

  getContactByFlowValidation(flowValidation : SubscriptionFlowValidation) {
    return this.http.get<ContactVM[]>(`${environment.API}/Contact/GetContacts/SubscriptionFlowValidation/${flowValidation}`);
  }

  addContact(Contact: ContactVM): Observable<any> {
    const headers = { 'content-type': 'application/json'}
    const body=JSON.stringify(Contact);
    return this.http.post(`${environment.API}/Contact/AddContact`, body, {'headers':headers});
  }

  updateContact(Contact: ContactVM) {
    return this.http.put(`${environment.API}/Contact/UpdateContact`, Contact)
  }

  deleteContact(ContactId: number) {
    return this.http.delete(`${environment.API}/Contact/RemoveContact/${ContactId}`);
  }
}