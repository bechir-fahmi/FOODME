import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Token, User, Response } from './interface';
import { Menu } from '@core';
import { map } from 'rxjs/operators';
import { environment } from '@env/environment';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  constructor(protected http: HttpClient) {}

  createCompany(username: string, password: string, rememberMe = false) {
    return this.http.post<Response>(`${environment.API}/BrandGroup/AddBrandGroup`, { EmailOrUserName : username, password, rememberMe });
  }
}
