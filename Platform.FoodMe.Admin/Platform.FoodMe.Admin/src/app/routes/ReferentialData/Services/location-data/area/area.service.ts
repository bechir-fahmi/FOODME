import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AreaVM } from '../../../Models/location-data/AreaVM';
import { HelperService } from '../../helper.service';


@Injectable({
  providedIn: 'root'
})
export class AreaService {
  private http: HttpClient;
  private helper: HelperService<AreaVM>;

  constructor(http: HttpClient, helper: HelperService<AreaVM>) {
    this.http = http;
    this.helper = helper;
  }

  getAreas() {
    return this.helper.get('/Area/GetAllAreas');
  }

  deleteArea(areaid: number) {
    return this.helper.delete(`/Area/RemoveArea/${areaid}`);
  }

  addArea(area: any) {
    return this.helper.add('/Area/AddArea', area);
  }

  updateArea(area: AreaVM) {
  return this.helper.update('/Area/UpdateArea', area);
  }
}
