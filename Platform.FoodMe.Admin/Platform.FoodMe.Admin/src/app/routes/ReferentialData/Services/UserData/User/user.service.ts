import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { SubscriptionFlowValidation } from '@shared/Models/SubscriptionFlowValidation';
import { UserVM } from 'app/routes/ReferentialData/Models/User Data/UserVM';
import { Observable } from 'rxjs';
import { HelperService } from '../../helper.service';


@Injectable({
  providedIn: 'root'
})
export class UserService {
  private http: HttpClient;

  constructor(http: HttpClient, private helper: HelperService<any>) {
    this.http = http;
    this.helper = helper;
  }

  getAllUsers() {
    return this.http.get<UserVM[]>(`${environment.API}/User/GetAllUsers`);
  }

  getUser(UserId : string) {
    return this.http.get<UserVM>(`${environment.API}/User/GetUser/id/${UserId}`);
  }

  getUserByFlowValidation(flowValidation : SubscriptionFlowValidation) {
    return this.http.get<UserVM[]>(`${environment.API}/User/GetUsers/SubscriptionFlowValidation/${flowValidation}`);
  }

  addUser(User: UserVM): Observable<any> {
    //const headers = { 'content-type': 'application/json'}
    //const body=JSON.stringify(User);
    return this.helper.add('/User/AddUser', User);
  }

  updateUser(User: UserVM) {
    return this.http.put(`${environment.API}/User/UpdateUser`, User)
  }

  deleteUser(UserId: number) {
    return this.http.delete(`${environment.API}/User/RemoveUser/${UserId}`);
  }
}