import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { HelperService } from '../ReferentialData/Services/helper.service';
import { LoyaltyProgram } from './Models/LoyaltyProgram';

@Injectable({
  providedIn: 'root'
})
export class LoyalityService {

  private helper: HelperService<LoyaltyProgram>;
  private messageSource = new BehaviorSubject('default message');
  currentMessage = this.messageSource.asObservable();
  private http: HttpClient;

  constructor(http: HttpClient, helper: HelperService<LoyaltyProgram>) {
    this.helper = helper;
    this.http = http;
   }

   addLoyaltySetting(LoyaltyProgram: LoyaltyProgram) {
    return this.helper.add('/LoyalitySetting/AddLoyalitySetting', LoyaltyProgram);
  }

}
