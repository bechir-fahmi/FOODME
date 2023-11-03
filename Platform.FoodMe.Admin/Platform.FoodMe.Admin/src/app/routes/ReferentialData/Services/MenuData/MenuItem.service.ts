import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { Observable } from 'rxjs';
import { MenuItemVM } from '../../Models/MenuData/MenuItemVM';



@Injectable({
  providedIn: 'root'
})
export class MenuItemService {
  private http: HttpClient;

  constructor(http: HttpClient) {
    this.http = http;
  }

  getAllMenuItems() {
    return this.http.get<MenuItemVM[]>(`${environment.API}/MenuItem/GetAllMenuItem`);
  }

  getMenuItem(MenuItemId : number) {
    return this.http.get<MenuItemVM[]>(`${environment.API}/MenuItem/GetMenuItem/id/${MenuItemId}`);
  }


  addMenuItem(MenuItem: MenuItemVM): Observable<any> {
    const headers = { 'content-type': 'application/json'}
    const body=JSON.stringify(MenuItem);
    return this.http.post(`${environment.API}/MenuItem/AddMenuItem`, body, {'headers':headers});
  }

  updateMenuItem(MenuItem: MenuItemVM) {
    return this.http.put(`${environment.API}/MenuItem/UpdateMenuItem`, MenuItem)
  }

  deleteMenuItem(MenuItemId: number) {
    return this.http.delete(`${environment.API}/MenuItem/RemoveMenuItem/${MenuItemId}/version/`);
  }
}