import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { Observable } from 'rxjs';
import { MenuVM } from '../../Models/MenuData/MenuVM';


@Injectable({
  providedIn: 'root'
})
export class MenuService {
  private http: HttpClient;

  constructor(http: HttpClient) {
    this.http = http;
  }

  getAllMenus() {
    return this.http.get<MenuVM[]>(`${environment.API}/Menu/GetAllMenu`);
  }

  getMenu(MenuId : number) {
    return this.http.get<MenuVM>(`${environment.API}/Menu/GetMenu/${MenuId}`);
  }


  addMenu(menu: MenuVM): Observable<any> {
    const headers = { 'content-type': 'application/json'}

    
    const body=JSON.stringify( menu );
    return this.http.post(`${environment.API}/Menu/AddMenu`, body, {'headers':headers});
  }

  updateMenu(Menu: MenuVM) {
    return this.http.patch(`${environment.API}/Menu/UpdateMenu`, Menu);
  }

  deleteMenu(MenuId: number) {
    return this.http.delete(`${environment.API}/Menu/RemoveMenu/${MenuId}`);
  }
}