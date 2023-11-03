import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { HelperService } from '../ReferentialData/Services/helper.service';
import { BehaviorSubject } from 'rxjs';
import {  WorkingTime } from './Models/WorkingTime';

@Injectable({
  providedIn: 'root'
})
export class BusinessHoursService {


  private helper: HelperService<WorkingTime>;
  private messageSource = new BehaviorSubject('default message');
  currentMessage = this.messageSource.asObservable();
  private http: HttpClient;

  constructor(http: HttpClient, helper: HelperService<WorkingTime>) {
    this.helper = helper;
    this.http = http;
   }

   getAllWorkingTime() {
    return this.helper.get('/WorkingTime/GetAllWorkingTime');
  }

   getAllRestaurants() {
    return this.helper.get('/Restaurant/GetAllRestaurants');
  }

  getWorkingTime(Id : number) {
    return this.http.get<WorkingTime>(`${environment.API}/WorkingTime/GetWorkingTime/id/${Id}`);
  }

  deleteCarhopAddress(WorkingHourId: number) {
    return this.helper.delete(`/WorkingTime/RemoveCarhopAddress/${WorkingHourId}`);
  }

  addWorkingTime(WorkingHours: WorkingTime) {
    return this.helper.add('/WorkingTime/AddWorkingTime', WorkingHours);
  }

  updateWorkingTime(WorkingHours: WorkingTime) {
  return this.helper.update('/WorkingTime/UpdateWorkingTime', WorkingHours);
  }

}
