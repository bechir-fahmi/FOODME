import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RegionVM } from 'app/routes/ReferentialData/Models/location-data/RegionVM';
import { HelperService } from '../../helper.service';


@Injectable({
  providedIn: 'root'
})
export class RegionService {
  private http: HttpClient;
  private helper: HelperService<RegionVM>;

  constructor(http: HttpClient, helper: HelperService<RegionVM>) { 
    this.http = http;
    this.helper = helper;
  }

  getRegions() {
    return this.helper.get('/Region/GetAllRegions');
  }

  deleteRegion(regionid: number) {
    return this.helper.delete(`/Region/RemoveRegion/${regionid}`);
  }

  addRegion(region: RegionVM) {
    return this.helper.add('/Region/AddRegion', region);
  }

  updateRegion(region: RegionVM) {
  return this.helper.update('/Region/UpdateRegion', region);
  }
}