import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { HelperService } from '../helper.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class KitchenTypesService {

  constructor( private helper: HelperService<any>, private http: HttpClient) { 
    this.helper = helper;
    this.http = http;
  }

  getAllKitchens() {
    return this.helper.get('/kitchenType/GetAllKitchens');
  }

 /* getCarhopAddress(CarhopAddressId : number) {
    
    return this.http.get<any>(`${environment.API}/CarhopAddress/GetCarhopAddress/id/${CarhopAddressId}`);
  }

  deleteCarhopAddress(CarhopAddressId: number) {
    return this.helper.delete(`/CarhopAddress/RemoveCarhopAddress/${CarhopAddressId}`);
  }

  addCarhopAddress(CarhopAddress: any) {
    return this.helper.add('/CarhopAddress/AddCarhopAddress', CarhopAddress);
  }

  updateCarhopAddress(CarhopAddress: any) {
  return this.helper.update('/CarhopAddress/UpdateCarhopAddress', CarhopAddress);
  }*/
}
