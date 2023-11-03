import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { RoleVM } from 'app/routes/ReferentialData/Models/User Data/RoleVM';


@Injectable({
  providedIn: 'root'
})
export class RoleService {
  private http: HttpClient;

  constructor(http: HttpClient) {
    this.http = http;
  }

  getAllRoles() {
    return this.http.get<RoleVM[]>(`${environment.API}/Role/GetAllRoles`);
  }
}
