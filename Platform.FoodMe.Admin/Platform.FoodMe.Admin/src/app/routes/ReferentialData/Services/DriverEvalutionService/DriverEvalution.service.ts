import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import {DriverEvalutionDto} from "../../Models/CustomerManagement/DriverEvalutionDto";



@Injectable({
  providedIn: 'root'
})
export class DriverEvalutionService {

  constructor(private http:HttpClient) { }


  getDriverEvalution()
  {
    return this.http.get<DriverEvalutionDto[]>(`${environment.API}/DriverEvalutions/GetAllDrivertEvalutions`);
  }

}
