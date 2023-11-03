import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CityVM } from '../../../Models/location-data/CityVM';
import { HelperService } from '../../helper.service';


@Injectable({
  providedIn: 'root'
})
export class CityService {
  private http: HttpClient;
  private helper: HelperService<CityVM>;

  constructor(http: HttpClient, helper: HelperService<CityVM>) { 
    this.http = http;
    this.helper = helper;
  }

  getCities() {
    return this.helper.get('/City/GetAllCities');
  }

  deleteCity(cityid: number) {
    return this.helper.delete(`/City/RemoveCity/${cityid}`);
  }

  addCity(city: CityVM) {
    return this.helper.add('/City/AddCity', city);
  }

  updateCity(city: CityVM) {
  return this.helper.update('/City/UpdateCity', city);
  }
}