import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { CountryVM } from 'app/routes/ReferentialData/Models/location-data/CountryVM';
import { RoleVM } from 'app/routes/ReferentialData/Models/User Data/RoleVM';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class RoleService {
  private http: HttpClient;

  constructor(http: HttpClient) {
    this.http = http;
  }

  getroles() {
    return this.http.get<RoleVM[]>(`${environment.API}/Role/GetAllRoles`);
  }
  getRole(roleId : string) {
    return this.http.get<RoleVM>(`${environment.API}/Role/GetRole/${roleId}`);
  }

  addrole(role: RoleVM): Observable<any> {
    const headers = { 'content-type': 'application/json'};
    const body=JSON.stringify(role);
    console.log(body);
    return this.http.post(`${environment.API}/Role/AddRole`, body, {headers});
  }

  updaterole(role: RoleVM) {
    return this.http.put(`${environment.API}/Role/UpdateRole`, role);
  }

  deleterole(roleid: string) {
    return this.http.delete(`${environment.API}/Role/RemoveRole/${roleid}`);
  }
}
