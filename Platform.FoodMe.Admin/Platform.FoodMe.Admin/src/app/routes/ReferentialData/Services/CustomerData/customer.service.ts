import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserVM } from '../../Models/User Data/UserVM';
import { HelperService } from '../helper.service';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private http: HttpClient,private helper: HelperService<UserVM>) { 
    this.http = http;
    this.helper = helper;
  }

  getAllCustomer() {
    return this.helper.get('/user/getAllUsers');
  }

  getAllLoyaltySetting() {
    return this.helper.get('/LoyalitySetting/GetAllLoyalitySettings');
  }
}
