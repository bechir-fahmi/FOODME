import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { Observable } from 'rxjs';
import { MenuTemplateVM } from '../../Models/MenuData/MenuTemplateVM';



@Injectable({
  providedIn: 'root'
})
export class TemplateMenuService {
  private http: HttpClient;

  constructor(http: HttpClient) {
    this.http = http;
  }

  getAllTemplateMenus() {
    return this.http.get<MenuTemplateVM[]>(`${environment.API}/TemplateMenu/GetAllTemplateMenu`);
  }

  getTemplateMenu(TemplateMenuId : number) {
    return this.http.get<MenuTemplateVM>(`${environment.API}/TemplateMenu/GetTemplateMenu/${TemplateMenuId}`);
  }


  addTemplateMenu(TemplateMenu: MenuTemplateVM): Observable<any> {
    const headers = { 'content-type': 'application/json'}
    const body = JSON.stringify({
    
      "nameLabelCode": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "languageResources": [
        {
          "id": 0,
          "code": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
          "value": "string",
          "languageKey": 0
        }
      ],
      "brandId": 1,
      "menuType": 1,
      "menutStatus": 0
    });
    return this.http.post(`${environment.API}/TemplateMenu/AddTemplateMenu`, body, {'headers':headers});
  }

  updateTemplateMenu(TemplateMenu: MenuTemplateVM) {
    const headers = { 'content-type': 'application/json'}
    const body = JSON.stringify(TemplateMenu);
    return this.http.put(`${environment.API}/TemplateMenu/UpdateTemplateMenu`, body, {'headers':headers})
  }

  deleteTemplateMenu(TemplateMenuId: number) {
    return this.http.delete(`${environment.API}/TemplateMenu/RemoveTemplateMenu/${TemplateMenuId}`);
  }
}