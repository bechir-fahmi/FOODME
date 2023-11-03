import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { Observable } from 'rxjs';
import { ExtraModifierGroupVM } from '../../Models/MenuData/ExtraModifierGroupVM';



@Injectable({
  providedIn: 'root'
})
export class ExtraModifierGroupService {
  private http: HttpClient;

  constructor(http: HttpClient) {
    this.http = http;
  }

  getAllExtraModifierGroups() {
    return this.http.get<ExtraModifierGroupVM[]>(`${environment.API}/ExtraModfierGroup/GetAllExtraModifierGroup`);
  }

  getExtraModifierGroup(ExtraModifierGroupId : number) {
    return this.http.get<ExtraModifierGroupVM>(`${environment.API}/ExtraModifierGroup/GetExtraModifierGroup/id/${ExtraModifierGroupId}`);
  }


  addExtraModifierGroup(ExtraModifierGroup: ExtraModifierGroupVM): Observable<any> {
    const headers = { 'content-type': 'application/json'}
    const body=JSON.stringify(ExtraModifierGroup);
    return this.http.post(`${environment.API}/ExtraModfierGroup/AddExtraModifierGroup`, body, {'headers':headers});
  }

  updateExtraModifierGroup(ExtraModifierGroup: ExtraModifierGroupVM) {
    return this.http.put(`${environment.API}/ExtraModifierGroup/UpdateExtraModifierGroup`, ExtraModifierGroup)
  }

  deleteExtraModifierGroup(ExtraModifierGroupId: number) {
    return this.http.delete(`${environment.API}/ExtraModfierGroup/RemoveExtraModifierGroup/${ExtraModifierGroupId}`);
  }
}